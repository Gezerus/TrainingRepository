using System;

namespace SimpleORM
{
    /// <summary>
    /// The exception that generate when object model do not match the database table
    /// </summary>
    [Serializable]
    public class MappingException : Exception
    {

        public MappingException() : base() 
        { }
        public MappingException(string message) : base(message)
        {}

        public MappingException(string message, System.Exception inner) : base(message, inner)
        { }


        protected MappingException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) 
        { }
    }
}
