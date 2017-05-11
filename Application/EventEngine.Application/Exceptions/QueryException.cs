using System;

namespace CodeConcepts.EventEngine.Application.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException(string message) : base(message)
        {
        }
    }
}