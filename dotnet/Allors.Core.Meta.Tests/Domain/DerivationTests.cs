namespace Allors.Core.Meta.Tests.Domain;

using System;
using System.Linq;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Xunit;

public class DerivationTests
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
        meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @dateTime, "DerivedAt");

        var population = new MetaPopulation(meta)
        {
            DerivationById =
            {
                ["FullName"] = new FullNameDerivation(firstName, lastName),
            },
        };

        var john = population.Build(person);
        john[firstName] = "John";
        john[lastName] = "Doe";

        population.Derive();

        Assert.Equal("John Doe", john[fullName]);

        population.DerivationById["FullName"] = new GreetingDerivation(population.DerivationById["FullName"], firstName, lastName);

        var jane = population.Build(person);
        jane[firstName] = "Jane";
        jane[lastName] = "Doe";

        population.Derive();

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
