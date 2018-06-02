using System;

namespace Alps.Domain
{
    public class DomainException:Exception
    {
        public DomainException()
            : base()
        {
        }
        public DomainException(string msg)
            : base(msg)
        {
        }
    }
}
