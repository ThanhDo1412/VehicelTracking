using System;
using System.Collections.Generic;
using System.Text;
using VT.Data.CommonData;

namespace VT.Data.Helper
{
    public class CustomException : Exception
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public CustomException(ErrorCode error)
        {
            ErrorCode = error.ToString();
            ErrorMessage = error.GetDisplayAttribute().Name;
        }

        public CustomException(ErrorCode error, params object[] values)
        {
            ErrorCode = error.ToString();
            ErrorMessage = string.Format(error.GetDisplayAttribute().Name, values);
        }
    }
}
