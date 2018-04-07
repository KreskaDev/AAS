using System;

namespace AAS.Persistance
{
    public class EntityId<TKey>
        : IEquatable<EntityId<TKey>>
        where TKey
        : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        protected EntityId() { }

        protected EntityId(TKey id) { Id = id; }

        public static bool operator ==(EntityId<TKey> left, EntityId<TKey> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityId<TKey> left, EntityId<TKey> right)
        {
            return !Equals(left, right);
        }


        public virtual bool Equals(EntityId<TKey> other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;


            if (!obj.GetType().IsInstanceOfType(this))
                return false;

            var other = obj as EntityId<TKey>;

            if (other == null) return false;

            return this.Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 907 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }

        public static EntityId<TKey> Empty()
        {
            return new EntityId<TKey>(default(TKey));
        }
    }
}