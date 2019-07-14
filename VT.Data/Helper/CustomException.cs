using System;
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
    }
}
