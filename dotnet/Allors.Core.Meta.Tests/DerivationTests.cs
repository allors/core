namespace Allors.Core.Meta.Tests;

using System;
using System.Linq;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using Xunit;

public class DerivationTests
{
    [Fact]
    public void Derivation()
    {
        var metaMeta = new MetaMeta();
        var domain = metaMeta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = metaMeta.AddUnit(domain, Guid.NewGuid(), "String");
        var @dateTime = metaMeta.AddUnit(domain, Guid.NewGuid(), "DateTime");
        var person = metaMeta.AddClass(domain, Guid.NewGuid(), "Person");
        var firstName = metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        var lastName = metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");
        var fullName = metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FullName");
        metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @dateTime, "DerivedAt");

        var meta = new Meta(metaMeta)
        {
            DerivationById =
            {
                ["FullName"] = new FullNameDerivation(firstName, lastName),
            },
        };

        var john = meta.Build(person);
        john[firstName] = "John";
        john[lastName] = "Doe";

        meta.Derive();

        Assert.Equal("John Doe", john[fullName]);

        meta.DerivationById["FullName"] = new GreetingDerivation(meta.DerivationById["FullName"], firstName, lastName);

        var jane = meta.Build(person);
        jane[firstName] = "Jane";
        jane[lastName] = "Doe";

        meta.Derive();

        Assert.Equal("Jane Doe Chained", jane[fullName]);
    }

    private class FullNameDerivation(IMetaRoleType firstName, IMetaRoleType lastName) : IMetaDerivation
    {
        public void Derive(MetaChangeSet changeSet)
        {
            var firstNames = changeSet.ChangedRoles(firstName);
            var lastNames = changeSet.ChangedRoles(lastName);

            if (!firstNames.Any() && !lastNames.Any())
            {
                return;
            }

            var people = firstNames.Union(lastNames).Select(v => v.Key).Distinct();

            foreach (IMetaObject person in people)
            {
                // Dummy updates ...
#pragma warning disable S1656 // Variables should not be self-assigned
                person["FirstName"] = person["FirstName"];
                person["LastName"] = person["LastName"];
#pragma warning restore S1656 // Variables should not be self-assigned

                person["DerivedAt"] = DateTime.Now;

                person["FullName"] = $"{person["FirstName"]} {person["LastName"]}";
            }
        }
    }

    private class GreetingDerivation(IMetaDerivation derivation, IMetaRoleType firstName, IMetaRoleType lastName) : IMetaDerivation
    {
        public void Derive(MetaChangeSet changeSet)
        {
            derivation.Derive(changeSet);

            var firstNames = changeSet.ChangedRoles(firstName);
            var lastNames = changeSet.ChangedRoles(lastName);

            if (!firstNames.Any() && !lastNames.Any())
            {
                return;
            }

            var people = firstNames.Union(lastNames).Select(v => v.Key).Distinct();

            foreach (IMetaObject person in people)
            {
                person["FullName"] = $"{person["FullName"]} Chained";
            }
        }
    }
}
