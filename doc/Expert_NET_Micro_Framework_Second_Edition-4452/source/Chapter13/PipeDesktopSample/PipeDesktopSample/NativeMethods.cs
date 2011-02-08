using System;
using System.Runtime.InteropServices;

namespace PipeDesktopSample
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateNamedPipe(string lpName, uint dwOpenMode, uint dwPipeMode, uint nMaxInstances, uint nOutBufferSize, uint nInBufferSize, uint nDefaultTimeOut, IntPtr pipeSecurityDescriptor);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ConnectNamedPipe(IntPtr hNamedPipe, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DisconnectNamedPipe(IntPtr hNamedPipe);

        public const int ERROR_BROKEN_PIPE = 0x6d;
        public const int ERROR_HANDLE_EOF = 0x26;
        public const int ERROR_INVALID_HANDLE = 6;
        public const int ERROR_INVALID_PARAMETER = 0x57;
        public const int ERROR_IO_PENDING = 0x3e5;
        public const int ERROR_NO_DATA = 0xe8;
        public const int ERROR_OPERATION_ABORTED = 0x3e3;
        public const int ERROR_PIPE_BUSY = 0xe7;
        public const int ERROR_PIPE_CONNECTED = 0x217;
        public const int ERROR_PIPE_LISTENING = 0x218;
        public const int ERROR_PIPE_NOT_CONNECTED = 0xe9;
        public const uint PIPE_ACCESS_DUPLEX = 3;
        public const uint PIPE_ACCESS_INBOUND = 1;
        public const uint PIPE_ACCESS_OUTBOUND = 2;
        public const uint PIPE_CLIENT_END = 0;
        public const uint PIPE_NOWAIT = 1;
        public const uint PIPE_READMODE_BYTE = 0;
        public const uint PIPE_READMODE_MESSAGE = 2;
        public const uint PIPE_SERVER_END = 1;
        public const uint PIPE_TYPE_BYTE = 0;
        public const uint PIPE_TYPE_MESSAGE = 4;
        public const uint PIPE_UNLIMITED_INSTANCES = 0xff;
        public const uint PIPE_WAIT = 0;
    }
}
