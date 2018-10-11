using System.Net.Mail;

namespace Logic.Helpers
{
    public class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var parsed = new MailAddress(email);
                return parsed.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}