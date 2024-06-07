namespace Allors.Core.Meta.Tests;

using System;
using System.Linq;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using Xunit;

public class DerivationOverrideTests
{
    [Fact]
    public void Derivation()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = meta.AddUnit(domain, Guid.NewGuid(), "String");
        var @dateTime = meta.AddUnit(domain, Guid.NewGuid(), "DateTime");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        var firstName = meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        var lastName = meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");
        var fullName = meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FullName");
        meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, dateTime, "DerivedAt");
        meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "Greeting");

        var population = new MetaPopulation(meta)
        {
            DerivationById =
            {
                ["FullName"] = new FullNameDerivation(firstName, lastName),
                ["Greeting"] = new GreetingDerivation(fullName),
            },
        };

        var john = population.Build(person);
        john["FirstName"] = "John";
        john["LastName"] = "Doe";

        population.Derive();

        Assert.Equal("Hello John Doe!", john["Greeting"]);
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

    private class GreetingDerivation(IMetaRoleType fullName) : IMetaDerivation
    {
        public void Derive(MetaChangeSet changeSet)
        {
            var fullNames = changeSet.ChangedRoles(fullName);

            if (!fullNames.Any())
            {
                return;
            }

            var people = fullNames.Select(v => v.Key).Distinct();

            foreach (IMetaObject person in people)
            {
                person["Greeting"] = $"Hello {person["FullName"]}!";
            }
        }
    }
}
