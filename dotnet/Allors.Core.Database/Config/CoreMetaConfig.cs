namespace Allors.Core.Database.Config
{
    using System;
    using Allors.Core.Database.Meta;
    using Allors.Embedded.Domain;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class CoreMetaConfig : IMetaConfig
    {
        /// <summary>
        /// Creates a new Core Population.
        /// </summary>
        public CoreMetaConfig()
        {
            this.CoreMetaMetaConfig = new CoreMetaMetaConfig();
            this.EmbeddedPopulation = new EmbeddedPopulation();

            var embeddedMeta = this.CoreMetaMetaConfig.EmbeddedMeta;
            var @interface = embeddedMeta.ObjectTypeByName["Interface"];
            var interfaceId = @interface.RoleTypeByName["Id"];
            var interfaceSingularName = @interface.RoleTypeByName["SingularName"];
            var interfaceAssignedPluralName = @interface.RoleTypeByName["AssignedPluralName"];

            EmbeddedObject NewInterface(Guid id, string singularName, string? assignedPluralName = null)
            {
                var x = interfaceId.GetType();
                var y = interfaceSingularName.GetType();
                var z = interfaceAssignedPluralName.GetType();

                return this.EmbeddedPopulation.Create(@interface, v =>
                {
                    v[interfaceId] = id;
                    v[interfaceSingularName] = singularName;
                    v[interfaceAssignedPluralName] = assignedPluralName;
                });
            }

            var @object = NewInterface(new Guid("1595A154-CEE8-41FC-A88F-CE3EEACA8B57"), "Object");
        }

        /// <summary>
        /// The Meta Population.
        /// </summary>
        public CoreMetaMetaConfig CoreMetaMetaConfig { get; }

        /// <summary>
        /// The embedded population.
        /// </summary>
        public EmbeddedPopulation EmbeddedPopulation { get; set; }

        /// <inheritdoc/>
        public MetaPopulation Build()
        {
            return new MetaPopulation(this.EmbeddedPopulation);
        }
    }
}
