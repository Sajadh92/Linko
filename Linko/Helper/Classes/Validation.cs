
using System.Linq;

namespace Linko.Helper
{
    public static class Validation
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsPasswordStrength(this string pass)
        {
            if (string.IsNullOrWhiteSpace(pass) ||
                pass.Length < 8 ||
                pass.Any(char.IsUpper).ToString().Length == 0 ||
                pass.Any(char.IsLower).ToString().Length == 0 ||
                pass.Any(char.IsDigit).ToString().Length == 0 ||
                pass.Any(char.IsLetter).ToString().Length == 0)
                return false;

            return true;
        }
    }
}
