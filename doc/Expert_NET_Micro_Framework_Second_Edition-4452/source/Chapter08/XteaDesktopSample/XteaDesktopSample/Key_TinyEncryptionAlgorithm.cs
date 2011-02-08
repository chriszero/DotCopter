using System;

namespace Microsoft.SPOT.Cryptography
{
    public sealed class Key_TinyEncryptionAlgorithm
    {
        public const int c_SizeOfKey = 16; //bytes
        private const int c_SizeOfBlock = 8; //bytes
        private const uint c_RoundCount = 32; //number of round cycles
        private const uint c_Delta = 0x9e3779b9;

        private readonly uint[] subKeys;

        public Key_TinyEncryptionAlgorithm(byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            System.Diagnostics.Debug.Assert(BitConverter.IsLittleEndian); //be sure to have little endian like on MF

            this.subKeys = CreateSubKeys(key);
        }

        private static uint[] CreateSubKeys(byte[] key)
        {
            //if key is less than keySize it is extended to keySize and padded with zeros
            byte[] paddedKey;
            if (key.Length < c_SizeOfKey)
            {
                paddedKey = new byte[c_SizeOfKey];
                Array.Copy(key, paddedKey, Math.Min(c_SizeOfKey, key.Length));
            }
            else
                paddedKey = key;

            //convert byte array to DWord array
            uint[] subKeys = new uint[4];
            for (int i = 0; i < c_SizeOfKey / 4; i++)
                subKeys[i] = BitConverter.ToUInt32(paddedKey, i * 4);
            return subKeys;
        }

        public byte[] Encrypt(byte[] data, int offset, int count, byte[] IV)
        {
            return Modify(true, data, offset, count, IV);
        }

        public byte[] Decrypt(byte[] data, int offset, int count, byte[] IV)
        {
            return Modify(false, data, offset, count, IV);
        }

        private byte[] Modify(bool encrypt, byte[] data, int offset, int count, byte[] IV)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            int length = data.Length;
            if (count < c_SizeOfBlock) //two blocks required
                throw new ArgumentOutOfRangeException("count", "At least 8 bytes required!");
            if (offset < 0 || offset + count > length)
                throw new ArgumentOutOfRangeException("offset");

            //convert initialization vector to block
            //if IV is less than blockSize it is extended to blockSize and padded with zeros
            byte[] paddedIV;
            if (IV == null || IV.Length < c_SizeOfBlock)
            {
                paddedIV = new byte[c_SizeOfBlock];
                Array.Copy(IV, paddedIV, Math.Min(c_SizeOfBlock, IV.Length));
            }
            else
                paddedIV = IV;
            UInt64 previousCipherBlock = BitConverter.ToUInt64(paddedIV, 0);

            byte[] output = new byte[count];
            int fullBlockCount = (int)Math.Floor(count / (double)c_SizeOfBlock);
            int totalBlockCount = (int)Math.Ceiling(count / (double)c_SizeOfBlock);
            int remainingByteCount = count % c_SizeOfBlock;
            if (remainingByteCount > 0)
                fullBlockCount--; //handle last full block and last block separately
            int blockNo;
            for (blockNo = 0; blockNo < fullBlockCount; blockNo++)
            {
                if (encrypt)
                {
                    UInt64 plainBlock = BitConverter.ToUInt64(data, offset + blockNo * c_SizeOfBlock);
                    plainBlock ^= previousCipherBlock; //cipher block chaining
                    UInt64 cipherBlock = EncryptBlock(plainBlock);

                    byte[] cipherBlockBytes = BitConverter.GetBytes(cipherBlock);
                    Array.Copy(cipherBlockBytes, 0, output, blockNo * c_SizeOfBlock, c_SizeOfBlock);

                    previousCipherBlock = cipherBlock;
                }
                else
                {
                    UInt64 cipherBlock = BitConverter.ToUInt64(data, offset + blockNo * c_SizeOfBlock);
                    UInt64 plainBlock = DecryptBlock(cipherBlock);
                    plainBlock ^= previousCipherBlock; //cipher block chaining

                    byte[] plainBlockBytes = BitConverter.GetBytes(plainBlock);
                    Array.Copy(plainBlockBytes, 0, output, blockNo * c_SizeOfBlock, c_SizeOfBlock);

                    previousCipherBlock = cipherBlock;
                }
            }

            //cipher byte stealing
            if (remainingByteCount > 0)
            {
                if (encrypt)
                {
                    //last full block
                    UInt64 lastPlainFullBlock = BitConverter.ToUInt64(data, offset + blockNo * c_SizeOfBlock);
                    //cipher block chaining
                    lastPlainFullBlock ^= previousCipherBlock;
                    UInt64 lastCipherFullBlock = EncryptBlock(lastPlainFullBlock);

                    //last part block
                    blockNo++;
                    byte[] paddedBlockBytes = new byte[c_SizeOfBlock];
                    Array.Copy(data, offset + blockNo * c_SizeOfBlock, paddedBlockBytes, 0, remainingByteCount);
                    UInt64 lastPlainPartBlock = BitConverter.ToUInt64(paddedBlockBytes, 0);
                    //cipher block chaining
                    lastPlainPartBlock ^= lastCipherFullBlock;
                    //steal bytes blockSize - remainingByteCount
                    UInt64 mask = (0xFFFFFFFFFFFFFFFF << remainingByteCount * 8);
                    lastPlainPartBlock |= lastCipherFullBlock & mask;
                    UInt64 lastCipherPartBlock = EncryptBlock(lastPlainPartBlock);

                    //copy blocks to cipher output
                    //block order is swapped
                    byte[] cipherBlockBytes;
                    //copy extended last part block 
                    cipherBlockBytes = BitConverter.GetBytes(lastCipherPartBlock);
                    for (int i = 0; i < c_SizeOfBlock; ++i)
                        output[(totalBlockCount - 2) * c_SizeOfBlock + i] = cipherBlockBytes[i];
                    //copy truncated last full block
                    cipherBlockBytes = BitConverter.GetBytes(lastCipherFullBlock);
                    for (int i = 0; i < remainingByteCount; ++i)
                        output[(totalBlockCount - 1) * c_SizeOfBlock + i] = cipherBlockBytes[i];
                }
                else
                {
                    //last full block
                    UInt64 lastCipherFullBlock = BitConverter.ToUInt64(data, offset + blockNo * c_SizeOfBlock);
                    UInt64 lastPlainFullBlock = DecryptBlock(lastCipherFullBlock);

                    //last part block
                    blockNo++;
                    byte[] paddedBlockBytes = new byte[c_SizeOfBlock];
                    for (int i = 0; i < remainingByteCount; i++)
                        paddedBlockBytes[i] = data[offset + blockNo * c_SizeOfBlock + i];
                    UInt64 lastCipherPartBlock = BitConverter.ToUInt64(paddedBlockBytes, 0);
                    //steal bytes blockSize - remainingByteCount
                    UInt64 mask = (0xFFFFFFFFFFFFFFFF << remainingByteCount * 8);
                    lastCipherPartBlock |= lastPlainFullBlock & mask;
                    UInt64 lastPlainPartBlock = DecryptBlock(lastCipherPartBlock);
                    //cipher block chaining
                    lastPlainPartBlock ^= previousCipherBlock;

                    //cipher block chaining
                    lastPlainFullBlock ^= lastCipherPartBlock;

                    //copy blocks to cipher output
                    //block order is swapped
                    byte[] plainBlockBytes;
                    //copy extended last part block 
                    plainBlockBytes = BitConverter.GetBytes(lastPlainPartBlock);
                    Array.Copy(plainBlockBytes, 0, output, (totalBlockCount - 2) * c_SizeOfBlock, plainBlockBytes.Length);
                    //copy truncated last full block
                    plainBlockBytes = BitConverter.GetBytes(lastPlainFullBlock);
                    for (int i = 0; i < remainingByteCount; ++i)
                        output[(totalBlockCount - 1) * c_SizeOfBlock + i] = plainBlockBytes[i];
                }
            }
            return output;
        }

        private UInt64 EncryptBlock(UInt64 block)
        {
            uint y = (uint)block;
            uint z = (uint)(block >> 32);

            uint sum = 0;
            uint n = c_RoundCount;
            while (n-- > 0)
            {
                y += ((z << 4 ^ z >> 5) + z) ^ (sum + this.subKeys[sum & 3]);
                sum += c_Delta;
                z += ((y << 4 ^ y >> 5) + y) ^ (sum + this.subKeys[sum >> 11 & 3]);
            }

            return (UInt64)y | (UInt64)z << 32;
        }

        private UInt64 DecryptBlock(UInt64 block)
        {
            uint y = (uint)block;
            uint z = (uint)(block >> 32);

            uint sum = unchecked(c_Delta * c_RoundCount);
            uint n = c_RoundCount;
            while (n-- > 0)
            {
                //y += ((z << 4 ^ z >> 5) + z) ^ (sum + this.binKey[sum & 3]);
                z -= ((y << 4 ^ y >> 5) + y) ^ (sum + this.subKeys[sum >> 11 & 3]);
                sum -= c_Delta;
                //z += ((y << 4 ^ y >> 5) + y) ^ (sum + this.binKey[sum >> 11 & 3]);
                y -= ((z << 4 ^ z >> 5) + z) ^ (sum + this.subKeys[sum & 3]);
            }
            return (UInt64)y | (UInt64)z << 32;
        }
    }
}
