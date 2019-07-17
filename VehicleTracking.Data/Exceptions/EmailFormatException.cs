using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class EmailFormatException : CustomException
    {
        public EmailFormatException(ErrorCode error) : base(error)
        {
        }

        public EmailFormatException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}
