namespace Allors.Core.Meta.Meta
{
    public sealed class MetaManyToManyRoleType : IMetaToManyRoleType
    {
        internal MetaManyToManyRoleType(MetaObjectType objectType, string singularName, string pluralName, string name)
        {
            this.ObjectType = objectType;
            this.SingularName = singularName;
            this.PluralName = pluralName;
            this.Name = name;
        }

        IMetaAssociationType IMetaRoleType.AssociationType => this.AssociationType;

        IMetaCompositeAssociationType IMetaCompositeRoleType.AssociationType => this.AssociationType;

        public MetaManyToManyAssociationType AssociationType { get; internal set; } = null!;

        public MetaObjectType ObjectType { get; }

        public string SingularName { get; }

        public string PluralName { get; }

        public string Name { get; }

        public bool IsOne => false;

        public bool IsMany => true;

        void IMetaRoleType.Deconstruct(out IMetaAssociationType associationType, out IMetaRoleType roleType)
        {
            associationType = this.AssociationType;
            roleType = this;
        }

        public void Deconstruct(out MetaManyToManyAssociationType associationType, out MetaManyToManyRoleType roleType)
        {
            associationType = this.AssociationType;
            roleType = this;
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal string SingularNameForAssociationType(MetaObjectType metaObjectType)
        {
            return $"{metaObjectType.Name}Where{this.SingularName}";
        }

        internal string PluralNameForAssociationType(MetaObjectType metaObjectType)
        {
            return $"{this.ObjectType.Meta.Pluralize(metaObjectType.Name)}Where{this.SingularName}";
        }
    }
}
