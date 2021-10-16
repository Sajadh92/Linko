
using System.Collections.Generic;

namespace Linko.Helper
{
    public static class Message
    {
        public static readonly string InvalidEmail = "InvalidEmail";
        public static readonly string InvalidUsername = "InvalidUsername";
        public static readonly string InvalidPassword = "InvalidPassword";
        public static readonly string InvalidIdentity = "InvalidIdentity";
        public static readonly string InvalidVerificationCode = "InvalidVerificationCode";

        public static readonly string UserNotExist = "UserNotExist";
        public static readonly string UserIsDeleted = "UserIsDeleted";
        public static readonly string UserNotActive = "UserNotActive";
        public static readonly string PasswordNotStrength = "PasswordNotStrength";
        public static readonly string OldPasswordNotCorrect = "OldPasswordNotCorrect";
        public static readonly string VerificationCodeNotCorrect = "VerificationCodeNotCorrect";
        public static readonly string UsernameOrEmailAlreadyExist = "UsernameOrEmailAlreadyExist";
        public static readonly string UsernameOrPasswordNotCorrect = "UsernameOrPasswordNotCorrect";
        public static readonly string EmailOrVerificationCodeNotCorrect = "EmailOrVerificationCodeNotCorrect";

        public static readonly string LoginFaild = "LoginFaild";
        public static readonly string RegisterFaild = "RegisterFaild";
        public static readonly string VerificationFaild = "VerificationFaild";
        public static readonly string ChangePasswordFaild = "ChangePasswordFaild";
        public static readonly string ForgetPasswordFaild = "ForgetPasswordFaild";
        public static readonly string FindUserProfileFaild = "FindUserProfileFaild";
        public static readonly string GetUserAccountsFaild = "GetUserAccountsFaild";
        public static readonly string GetFaild = "GetFaild";
        public static readonly string InsertFaild = "InsertFaild";
        public static readonly string UpdateFaild = "UpdateFaild";
        public static readonly string DeleteFaild = "DeleteFaild";
        public static readonly string UndoDeleteFaild = "UndoDeleteFaild";
        public static readonly string PermanentlyDeleteFaild = "PermanentlyDeleteFaild";
        

        public static readonly Dictionary<string, Dictionary<string, string>> MsgDictionary = new()
        {
            {
                "Ar", new Dictionary<string, string>()
                {
                    { InvalidUsername, "" },
                    { InvalidPassword, "" },
                    { InvalidEmail, "" },
                    { PasswordNotStrength, "" },
                    { UsernameOrPasswordNotCorrect, "" },
                    { LoginFaild, "" },
                    { RegisterFaild, "" },
                    { UserIsDeleted, "" },
                    { UsernameOrEmailAlreadyExist, "" },
                    { UserNotActive, "" },
                    { VerificationFaild, "" },
                    { InvalidVerificationCode, "" },
                    { EmailOrVerificationCodeNotCorrect, "" },
                    { InvalidIdentity, "" },
                    { UserNotExist, "" },
                    { ForgetPasswordFaild, "" },
                    { FindUserProfileFaild, "" },
                    { ChangePasswordFaild, "" },
                    { OldPasswordNotCorrect, "" },
                    { VerificationCodeNotCorrect, "" }
                }
            },
            {
                "En", new Dictionary<string, string>()
                {
                    { InvalidUsername, "" },
                    { InvalidPassword, "" },
                    { InvalidEmail, "" },
                    { PasswordNotStrength, "" },
                    { UsernameOrPasswordNotCorrect, "" },
                    { LoginFaild, "" },
                    { RegisterFaild, "" },
                    { UserIsDeleted, "" },
                    { UsernameOrEmailAlreadyExist, "" },
                    { UserNotActive, "" },
                    { VerificationFaild, "" },
                    { InvalidVerificationCode, "" },
                    { EmailOrVerificationCodeNotCorrect, "" },
                    { InvalidIdentity, "" },
                    { UserNotExist, "" },
                    { ForgetPasswordFaild, "" },
                    { FindUserProfileFaild, "" },
                    { ChangePasswordFaild, "" },
                    { OldPasswordNotCorrect, "" },
                    { VerificationCodeNotCorrect, "" }
                }
            }
        };
    }
}
