using System;

namespace IpLi.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(String message) : base(message)
        {
        }
    }
}