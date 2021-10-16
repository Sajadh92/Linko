using Linko.Domain;
using System;

namespace Linko.Helper
{
    public static class Key
    {
        public static readonly string SecretKey = "cdAsuIt+MtEDbT5p9I7o3TvBgteF+4l/2sFpWG4Hloi7Tre6Dsw3QBYTY8xbWva8GlKgJzQZdcR3Luqm/bgt5u==";
        public static DateTime DateTimeIQ => DateTime.UtcNow.AddHours(3);
    }

    public static class ClaimInfo
    {
        public static readonly string UserManager = "Linko001";
    }

    public static class Result
    {
        public static ResObj Return(bool success)
        {
            return new() { Success = success };
        }

        public static ResObj Return(bool success, object data)
        {
            return new() { Success = success, Data = data };
        }

        public static ResObj Return(bool success, string msgCode)
        {
            return new() { Success = success, MsgCode = msgCode };
        }

        public static ResObj Return(bool success, string msgCode, object data)
        {
            return new() { Success = success, MsgCode = msgCode, Data = data };
        }
    }
}
