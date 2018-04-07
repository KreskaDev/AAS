using System;
using NHibernate.Proxy;

namespace AAS.Persistance
{
    //TODO-extrakr Entity should inhert from language extensions Record class
    public abstract class Entity<TKey>
        where TKey
            : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual bool Equals(Entity<TKey> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var thisType = (this is INHibernateProxy) ? GetType().BaseType : GetType();
            var otherType = (other is INHibernateProxy) ? other.GetType().BaseType : other.GetType();

            if (thisType != otherType)
            {
                return false;
            }

            return !Id.Equals(default(TKey)) && !other.Id.Equals(default(TKey)) && other.Id.Equals(Id);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return Equals(obj as Entity<TKey>);
        }


        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
