using System;

namespace CodeConcepts.EventEngine.Contracts.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException(string message) : base(message)
        {
        }
    }
}