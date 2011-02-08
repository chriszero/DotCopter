using System;
using System.Reflection;
using System.IO;
using System.Text;

namespace Microsoft.SPOT
{
    internal static class BinaryFormatter
    {
        private enum FieldTypeId
        {
            Boolean, Byte, SByte, Char, UInt16, Int16, UInt32, Int32, UInt64, Int64,
            Enum, Single, Double, String, DateTime, TimeSpan, Class, Struct, NonSerializableFieldType
        };

        private static int[] byteSizes = new int[] {
            1, 1, 1, 2, 2, 2, 4, 4, 8, 8,
            -1, 4, 8, -1, 8, 8, -1, -1};

        public static void SerializeObject(MemoryStream stream, object obj, Type type)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj.GetType() != type)
                throw new ArgumentException("Object must be of parameter type and cannot be a subclass of it.");
            if (type == null)
                throw new ArgumentNullException("type");
            if (!IsTypeSerializable(type))
                return;
            foreach (FieldInfo fieldInfo in GetFieldInfos(type))
            {
                if (fieldInfo.IsNotSerialized)
                    continue;
                try
                {
                    SerializeField(stream, obj, fieldInfo);
                }
                catch (Exception ex)
                {
                    throw new NotSupportedException(
                        string.Format("Cannot serialize field {0}.{1} of field type {2}.\n" + ex.Message,
                                      type.FullName, fieldInfo.Name, fieldInfo.FieldType), ex);
                }
            }
        }

        public static object DeSerializeObject(MemoryStream stream, Type type)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (type == null)
                throw new ArgumentNullException("type");
            if (!IsTypeSerializable(type))
                return null;
            object obj;
            //Mit dem Compact Framework können keine Klassen ohne paramterlosen Konstruktor erzeugt werden
#if PocketPC || Smartphone || WindowsCE
            try
            {
                obj = Activator.CreateInstance(type);
            }
            catch (MissingMethodException ex)
            {
                throw new ArgumentException("The specified type must have a parameterless constructor.", ex);
            }
#else
            obj = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);
#endif
            foreach (FieldInfo fieldInfo in GetFieldInfos(type))
            {
                if (fieldInfo.IsNotSerialized)
                    continue;
                try
                {
                    DeSerializeField(stream, obj, fieldInfo);
                }
                catch (Exception ex)
                {
                    throw new NotSupportedException(
                        string.Format("Cannot deserialize field {0}.{1} of field type {2}.\n" + ex.Message,
                                      type.FullName, fieldInfo.Name, fieldInfo.FieldType), ex);
                }
            }
            return obj;
        }

        private static void SerializeField(MemoryStream stream, object obj, FieldInfo fieldInfo)
        {
            object value = fieldInfo.GetValue(obj);
            ulong binValue;
            SerializationHintsAttribute attribute = GetSerializationHintsAttribute(fieldInfo);
            FieldTypeId id = GetFieldTypeId(fieldInfo, attribute);
            switch (id)
            {
                case FieldTypeId.Boolean:
                    binValue = ((bool)value) ? (ulong)1 : (ulong)0;
                    break;
                case FieldTypeId.Byte:
                    binValue = (byte)value;
                    break;
                case FieldTypeId.SByte:
                    binValue = (ulong)(sbyte)value;
                    break;
                case FieldTypeId.Char:
                    binValue = (char)value;
                    break;
                case FieldTypeId.UInt16:
                    binValue = (ushort)value;
                    break;
                case FieldTypeId.Int16:
                    binValue = (ulong)(short)value;
                    break;
                case FieldTypeId.UInt32:
                    binValue = (uint)value;
                    break;
                case FieldTypeId.Int32:
                    binValue = (ulong)(int)value;
                    break;
                case FieldTypeId.UInt64:
                    binValue = (ulong)value;
                    break;
                case FieldTypeId.Int64:
                    binValue = (ulong)(long)value;
                    break;
                case FieldTypeId.Enum:
                    Type underlayingType = Enum.GetUnderlyingType(fieldInfo.FieldType);
                    switch (Type.GetTypeCode(underlayingType))
                    {
                        case TypeCode.Byte:
                            binValue = (byte)value;
                            break;
                        case TypeCode.SByte:
                            binValue = (ulong)(sbyte)value;
                            break;
                        case TypeCode.Int16:
                            binValue = (ulong)(short)value;
                            break;
                        case TypeCode.UInt16:
                            binValue = (ushort)value;
                            break;
                        case TypeCode.Int32:
                            binValue = (ulong)(int)value;
                            break;
                        case TypeCode.UInt32:
                            binValue = (uint)value;
                            break;
                        case TypeCode.Int64:
                            binValue = (ulong)(long)value;
                            break;
                        case TypeCode.UInt64:
                            binValue = (ulong)value;
                            break;
                        default:
                            throw new NotSupportedException("Invalid enum.");
                    }
                    break;
                case FieldTypeId.Single:
                    binValue = BitConverter.ToUInt32(BitConverter.GetBytes((float)value), 0);
                    break;
                case FieldTypeId.Double:
                    binValue = BitConverter.ToUInt64(BitConverter.GetBytes((double)value), 0);
                    break;
                case FieldTypeId.DateTime:
                    binValue = (ulong)((DateTime)value).Ticks;
                    break;
                case FieldTypeId.TimeSpan:
                    binValue = (ulong)((TimeSpan)value).Ticks;
                    break;
                case FieldTypeId.String:
                    if (value != null)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes((string)value);
                        WriteCompressedUnsigned(stream, (uint)bytes.Length);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                    else
                        WriteCompressedUnsigned(stream, uint.MaxValue);
                    return;
                case FieldTypeId.Struct:
                    SerializeObject(stream, value, fieldInfo.FieldType);
                    return;
                case FieldTypeId.Class:
                    if (value == null)
                        throw new NotSupportedException("Fields which are classes must not be null.");
                    SerializeObject(stream, value, fieldInfo.FieldType);
                    return;
                case FieldTypeId.NonSerializableFieldType:
                    return;
                default :
                    throw new NotSupportedException();
            }
            int byteCount = GetByteSize(fieldInfo, id, attribute);
            WriteUnsignedLong(stream, binValue, byteCount);
        }

        private static void DeSerializeField(MemoryStream stream, object obj, FieldInfo fieldInfo)
        {
            SerializationHintsAttribute attribute = GetSerializationHintsAttribute(fieldInfo);
            FieldTypeId id = GetFieldTypeId(fieldInfo, attribute);
            if (id == FieldTypeId.NonSerializableFieldType)
                return;
            int byteCount = GetByteSize(fieldInfo, id, attribute);
            ulong binValue;
            if (id != FieldTypeId.String && id != FieldTypeId.Class && id != FieldTypeId.Struct)
                binValue = ReadUnsignedLong(stream, byteCount);
            else
                binValue = 0;
            object value = null;
            switch (id)
            {
                case FieldTypeId.Boolean:
                    value = binValue != 0;
                    break;
                case FieldTypeId.Byte:
                    value = (byte)binValue;
                    break;
                case FieldTypeId.SByte:
                    value = (sbyte)binValue;
                    break;
                case FieldTypeId.Char:
                    value = (char)binValue;
                    break;
                case FieldTypeId.UInt16:
                    value = (ushort)binValue;
                    break;
                case FieldTypeId.Int16:
                    value = (short)binValue;
                    break;
                case FieldTypeId.UInt32:
                    value = (uint)binValue;
                    break;
                case FieldTypeId.Int32:
                    value = (int)binValue;
                    break;
                case FieldTypeId.UInt64:
                    value = (ulong)binValue;
                    break;
                case FieldTypeId.Int64:
                    value = (long)binValue;
                    break;
                case FieldTypeId.Enum:
                    Type underlayingType = Enum.GetUnderlyingType(fieldInfo.FieldType);
                    switch (Type.GetTypeCode(underlayingType))
                    {
                        case TypeCode.Byte:
                            value = (byte)binValue;
                            break;
                        case TypeCode.SByte:
                            value = (sbyte)binValue;
                            break;
                        case TypeCode.Int16:
                            value = (short)binValue;
                            break;
                        case TypeCode.UInt16:
                            value = (ushort)binValue;
                            break;
                        case TypeCode.Int32:
                            value = (int)binValue;
                            break;
                        case TypeCode.UInt32:
                            value = (uint)binValue;
                            break;
                        case TypeCode.Int64:
                            value = (long)binValue;
                            break;
                        case TypeCode.UInt64:
                            value = (ulong)binValue;
                            break;
                    }
                    break;
                case FieldTypeId.Single:
                    value = BitConverter.ToSingle(BitConverter.GetBytes((uint)binValue), 0);
                    break;
                case FieldTypeId.Double:
                    value = BitConverter.ToDouble(BitConverter.GetBytes((ulong)binValue), 0);
                    break;
                case FieldTypeId.DateTime:
                    value = new DateTime((long)binValue);
                    break;
                case FieldTypeId.TimeSpan:
                    value = new TimeSpan((long)binValue);
                    break;
                case FieldTypeId.String:
                    uint count = ReadCompressedUnsigned(stream);
                    if (count != uint.MaxValue)
                    {
                        byte[] bytes = new byte[count];
                        stream.Read(bytes, 0, (int)count);
                        value = Encoding.UTF8.GetString(bytes);
                    }
                    else
                        value = null;
                    break;
                case FieldTypeId.Struct:
                case FieldTypeId.Class:
                    value = DeSerializeObject(stream, fieldInfo.FieldType);
                    break;
                default:
                    throw new NotSupportedException();
            }
            fieldInfo.SetValue(obj, value);
        }

        private static FieldInfo[] GetFieldInfos(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        private static SerializationHintsAttribute GetSerializationHintsAttribute(FieldInfo fieldInfo)
        {
            object[] attributes = fieldInfo.GetCustomAttributes(typeof(SerializationHintsAttribute), true);
            if (attributes.Length > 1)
                throw new NotSupportedException("Only one SerializationHintsAttribute per field allowed.");
            SerializationHintsAttribute attribute;
            if (attributes.Length == 1)
                attribute = (SerializationHintsAttribute)attributes[0];
            else
                attribute = null;
            return attribute;
        }

        public static bool IsTypeSerializable(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return (type.Attributes & ~TypeAttributes.Serializable) != 0;
        }

        private static FieldTypeId GetFieldTypeId(FieldInfo fieldInfo, SerializationHintsAttribute attribute)
        {
            Type type = fieldInfo.FieldType;
            if (type.IsPrimitive)
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                        if (attribute != null && attribute.RangeBias != 0)
                            throw new NotSupportedException("RangeBias not allowed for fields of type bool.");
                        if (attribute != null && attribute.Scale != 0)
                            throw new NotSupportedException("Scale not allowed for fields of type bool.");
                        return FieldTypeId.Boolean;
                    case TypeCode.Byte:
                        return FieldTypeId.Byte;
                    case TypeCode.SByte:
                        return FieldTypeId.SByte;
                    case TypeCode.Char:
                        return FieldTypeId.Char;
                    case TypeCode.UInt16:
                        return FieldTypeId.UInt16;
                    case TypeCode.Int16:
                        return FieldTypeId.Int32;
                    case TypeCode.UInt32:
                        return FieldTypeId.UInt32;
                    case TypeCode.Int32:
                        return FieldTypeId.Int32;
                    case TypeCode.UInt64:
                        return FieldTypeId.UInt64;
                    case TypeCode.Int64:
                        return FieldTypeId.Int64;
                    case TypeCode.Single:
                        return FieldTypeId.Single;
                    case TypeCode.Double:
                        return FieldTypeId.Double;
                    default:
                        throw new NotSupportedException("Unsupported primitive.");
                }
            }
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return FieldTypeId.String;
                case TypeCode.DateTime:
                    return FieldTypeId.DateTime;
                default:
                    if (type == typeof(TimeSpan))
                        return FieldTypeId.TimeSpan;
                    if (type.IsEnum)
                        return FieldTypeId.Enum;
                    if (type.IsValueType)
                    {
                        if ((type.Attributes & ~TypeAttributes.Serializable) != 0)
                            return FieldTypeId.Struct;
                        else
                            return FieldTypeId.NonSerializableFieldType;
                    }
                    if (type.IsInterface)
                        throw new NotSupportedException("Fields that are interfaces are not possible to serialize.");
                    if (type.IsClass)
                    {
                        if (type.IsArray)
                            throw new NotSupportedException("Fields that are arrays are not supported.");
                        if ((type.Attributes & ~TypeAttributes.Serializable) != 0)
                        {
                            if (type == typeof(Type))
                                throw new NotSupportedException("Fields of type Type not possible to serialize.");
                            if (type.IsAbstract)
                                throw new NotSupportedException("Fields that are of abstract types are not possible to serialize.");
                            if (!type.IsSealed && (attribute == null || (attribute.Flags & SerializationFlags.FixedType) == 0))
                                throw new NotSupportedException("Fields that are classes must be of a sealed class or must be marked as fixed type with the SerializationHintsAttribute.Flags.");
                            if(attribute == null || (attribute.Flags & SerializationFlags.PointerNeverNull) == 0)
                                throw new NotSupportedException("Fields that are classes cannot be null and must be marked with SerializationFlags.PointerNeverNull.");
                            return FieldTypeId.Class;
                        }
                        return FieldTypeId.NonSerializableFieldType;
                    }
                    throw new NotSupportedException("Unsupported field type.");
            }
        }

        private static int GetByteSize(FieldInfo fieldInfo, FieldTypeId id, SerializationHintsAttribute attribute)
        {
            if (attribute != null && attribute.BitPacked > 0)
            {
                switch (id)
                {
                    case FieldTypeId.Class:
                    case FieldTypeId.Double:
                    case FieldTypeId.Single:
                    case FieldTypeId.Struct:
                    case FieldTypeId.String:
                        throw new NotSupportedException("Cannot specify BitPacket hint for this field.");
                }
                if ((attribute.BitPacked % 8) != 0)
                    throw new NotSupportedException("Number of bits in BitPacked must be dividable by 8 to get complecte bytes.");
                return attribute.BitPacked / 8; 
            }
            if (id == FieldTypeId.Enum)
            {
                Type underlayingType = Enum.GetUnderlyingType(fieldInfo.FieldType);
                switch (Type.GetTypeCode(underlayingType))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                        return 1;
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                        return 2;
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                        return 4;
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        return 8;
                }
            }
            return byteSizes[(int)id];
        }

        private static void WriteUnsignedLong(MemoryStream stream, ulong value, int byteCount)
        {
            if (byteCount < 1 || byteCount > 8)
                throw new ArgumentOutOfRangeException("byteCount");
            if (value > Math.Pow(2, byteCount * 8) - 1)
                throw new ArgumentOutOfRangeException("value", "Value does not fit specified size.");
            for(int i = byteCount - 1; i >= 0; --i)
                stream.WriteByte((byte)(value >> (i * 8)));
        }

        private static ulong ReadUnsignedLong(MemoryStream stream, int byteCount)
        {
            if (byteCount < 1 || byteCount > 8)
                throw new ArgumentOutOfRangeException("byteCount");
            ulong value = 0;
            for (int i = byteCount - 1; i >= 0; --i)
                value |= (ulong)stream.ReadByte() << (i * 8);
            return value;
        }

        private static void WriteCompressedUnsigned(MemoryStream stream, uint value)
        {
            if (value == uint.MaxValue)
                stream.WriteByte(0xff);
            else if (value < 0x80)
                stream.WriteByte((byte)value);
            else if (value < 0x3f00)
                WriteUnsignedLong(stream, 0x8000 | value, 2);
            else if(value < 0x3f000000)
                WriteUnsignedLong(stream, 0xc0000000 | value, 4);
            else
                 throw new ArgumentOutOfRangeException("Max value is 0x3F000000.");
        }

        private static uint ReadCompressedUnsigned(MemoryStream stream)
        {
            uint b = (uint)stream.ReadByte();
            if (b == 0xff)
                return uint.MaxValue;
            uint v; 
            switch (b & 0xc0)
            {
                case 0x80:
                    v = (b & 0xffffff3f) << 8;
                    v |= (uint)stream.ReadByte();
                    return v;
                case 0xc0:
                    v = (b & 0xffffff3f) << 24;
                    v |= (uint)stream.ReadByte() << 16;
                    v |= (uint)stream.ReadByte() << 8;
                    v |= (uint)stream.ReadByte();
                    return v;
                default:
                    return b;
            }
        }
    }
}
