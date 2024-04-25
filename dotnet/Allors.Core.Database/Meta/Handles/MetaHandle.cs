namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A meta handle.
    /// </summary>
    public abstract record MetaHandle(Guid Id)
    {
        /// <inheritdoc/>
        public virtual bool Equals(MetaHandle? other)
        {
            if (other is null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || this.Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
