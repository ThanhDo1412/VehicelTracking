using System;
using System.Net.Mail;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;

namespace VehicleTracking.Data.Model
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            try
            {
                var mailAddress = new MailAddress(Email);
            }
            catch (FormatException)
            {
                throw new EmailFormatException(ErrorCode.E004);
            }

            if (Password.Length < 5)
            {
                throw new PasswordFormatException(ErrorCode.E005);
            }
        }
    }
}
