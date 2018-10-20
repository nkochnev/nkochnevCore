using System;

namespace NkochnevCore.Infrastructure.Data
{
    /// <summary>
    ///     Базовый класс для доменных сущностей с Id
    /// </summary>
    public abstract class BaseEntity : BaseDomain
    {
        /// <summary>
        ///     Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }
    }
}