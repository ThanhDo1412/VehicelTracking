using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class JourneyNotFoundException : CustomException
    {
        public JourneyNotFoundException(ErrorCode error) : base(error)
        {
        }

        public JourneyNotFoundException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}
