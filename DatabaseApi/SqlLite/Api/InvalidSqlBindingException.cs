using System;

namespace DatabaseApi.SqlLite.Api
{
    internal class InvalidSqlBindingException : Exception
    {
        public InvalidSqlBindingException(string errorMessage) : base(errorMessage)
        {

        }
    }
}