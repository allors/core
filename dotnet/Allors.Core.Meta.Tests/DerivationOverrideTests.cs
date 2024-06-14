namespace Allors.Core.Meta.Tests;

using System;
using System.Linq;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class DerivationOverrideTests
{
    [Fact]
    public void Derivation()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var @dateTime = metaMeta.AddUnit(Guid.NewGuid(), "DateTime");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var firstName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        var lastName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");
        var fullName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FullName");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, dateTime, "DerivedAt");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "Greeting");

        var meta = new Meta(metaMeta)
        {
            DerivationById =
            {
                ["FullName"] = new FullNameDerivation(firstName, lastName),
                ["Greeting"] = new GreetingDerivation(fullName),
            },
        };

        var john = meta.Build(person);
        john["FirstName"] = "John";
        john["LastName"] = "Doe";

        meta.Derive();

        john["Greeting"].Should().Be("Hello John Doe!");
    }

    private sealed class FullNameDerivation(IMetaRoleType firstName, IMetaRoleType lastName) : IMetaDerivation
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

    private sealed class GreetingDerivation(IMetaRoleType fullName) : IMetaDerivation
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
