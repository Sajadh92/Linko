
using System.Collections.Generic;

namespace Linko.Helper
{
    public static class Message
    {
        public static readonly string InvalidUsername = "InvalidUsername";
        public static readonly string PasswordNotStrength = "PasswordNotStrength";

        public static readonly Dictionary<string, Dictionary<string, string>> MsgDictionary = new()
        {
            {
                "Ar", new Dictionary<string, string>()
                {
                    { InvalidUsername, "اسم المستخدم غير صحيح" },
                    { PasswordNotStrength, "يرجى ادخال كلمة مرور قوية" },
                }
            },
            {
                "En", new Dictionary<string, string>()
                {
                    { InvalidUsername, "The Username is not correct" },
                    { PasswordNotStrength, "Kindly to enter strong password" },
                }
            }
        };

        public static string Get(string lang, string msgCode)
        {
            return MsgDictionary[lang][msgCode];
        }
    }
}
