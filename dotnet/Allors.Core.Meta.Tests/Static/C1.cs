namespace Allors.Core.Meta.Tests.Static;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

public class C1(Meta meta, MetaObjectType objectType)
    : MetaObject(meta, objectType), I1;
