using System;

namespace Policy.Application.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException(string message) : base(message)
        {
        }
    }
}