namespace Allors.Core.Database.Engines.Meta
{
    using System.Collections.Frozen;
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Core.Database.Meta.Domain;
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine composite.
    /// </summary>
    public abstract class EnginesComposite(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesObjectType(enginesMeta, metaObject)
    {
        private FrozenSet<EnginesInterface>? supertypes;

        private FrozenSet<EnginesComposite>? supertypesAndSelf;

        /// <summary>
        /// The supertypes.
        /// </summary>
        public IReadOnlySet<EnginesInterface> Supertypes => this.supertypes ??= this.MetaObject[this.M.CompositeSupertypes]
            .Select(v => this.EnginesMeta[(Interface)v])
            .ToFrozenSet();

        /// <summary>
        /// The supertypes and self.
        /// </summary>
        public IReadOnlySet<EnginesComposite> SupertypesAndSelf => this.supertypesAndSelf ??= this.Supertypes
            .Append(this)
            .ToFrozenSet();

        /// <summary>
        /// Determines whether an instance of composite other can be assigned to a variable of the this composite.
        /// </summary>
        public bool IsAssignableFrom(EnginesComposite other)
        {
            return other.SupertypesAndSelf.Contains(this);
        }
    }
}
