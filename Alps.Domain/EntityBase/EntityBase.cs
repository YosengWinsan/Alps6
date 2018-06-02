using System;
using System.ComponentModel.DataAnnotations;
namespace Alps.Domain
{
    public abstract partial class EntityBase
    {
        public Guid ID { get; set; }
        [Timestamp]
        [ScaffoldColumn(false)]
        public byte[] Timestamp { get; set; }

        public EntityBase()
        {
            ID = IdentityGenerator.NewSequentialGuid();
        }
        public bool HasUpdated(byte[] oldTimestamp)
        {
            if (oldTimestamp == null)
                return false;
            return Timestamp == oldTimestamp;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as EntityBase);
        }

        private static bool IsTransient(EntityBase obj)
        {
            return obj != null && Equals(obj.ID, default(Guid));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(EntityBase other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(ID, other.ID))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(ID, default(int)))
                return base.GetHashCode();
            return ID.GetHashCode();
        }

        public static bool operator ==(EntityBase x, EntityBase y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(EntityBase x, EntityBase y)
        {
            return !(x == y);
        }
    }
}
