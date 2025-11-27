using System;

/*
 Summary
 -------
 ServiceException is the custom exception type used by the BusinessLayer to wrap
 technical failures (infrastructure, unexpected runtime issues) with contextual messages.
 Distinguishes these from ValidationException (data/user errors) so callers can decide
 retry vs. user feedback. Supports serialization for potential cross-appdomain or
 logging scenarios.
 */

namespace OruMongoDB.BusinessLayer.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException() { }
        public ServiceException(string message) : base(message) { }
        public ServiceException(string message, Exception inner) : base(message, inner) { }
    }
}