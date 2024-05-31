namespace Allors.Core.Meta.Tests.Domain;

using System;
using System.Linq;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Xunit;

public class DerivationOverrideTests
{
    [Fact]
    public void Derivation()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit("String");
        var @dateTime = meta.AddUnit("DateTime");
        var person = meta.AddClass("Person");
        var firstName = meta.AddUnit(person, @string, "FirstName");
        var lastName = meta.AddUnit(person, @string, "LastName");
        var fullName = meta.AddUnit(person, @string, "FullName");
        meta.AddUnit(person, dateTime, "DerivedAt");
        meta.AddUnit(person, @string, "Greeting");

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

            foreach (MetaObject person in people)
            {
                // Dummy updates ...
                person["FirstName"] = person["FirstName"];
                person["LastName"] = person["LastName"];

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

            foreach (MetaObject person in people)
            {
                person["Greeting"] = $"Hello {person["FullName"]}!";
            }
        }
    }
}
