namespace Allors.Core.Meta.Tests;

using System;
using System.Linq;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class ObjectsTests
{
    [Fact]
    public void Filter()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var firstName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        var lastName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");

        var meta = new Meta(metaMeta);

        IMetaObject NewPerson(string firstName, string lastName)
        {
            return meta.Build(person, v =>
            {
                v[firstName] = firstName;
                v[lastName] = lastName;
            });
        }

        var jane = NewPerson("Jane", "Doe");
        var john = NewPerson("John", "Doe");
        var jenny = NewPerson("Jenny", "Doe");

        var lastNameDoe = meta.Objects.Where(v => (string)v[lastName]! == "Doe").ToArray();

        lastNameDoe.Length.Should().Be(3);
        lastNameDoe.Should().Contain(jane);
        lastNameDoe.Should().Contain(john);
        lastNameDoe.Should().Contain(jenny);

        var lessThanFourLetterFirstNames = meta.Objects.Where(v => ((string)v[firstName]!).Length < 4).ToArray();

        lessThanFourLetterFirstNames.Should().BeEmpty();

        var fourLetterFirstNames = meta.Objects.Where(v => ((string)v[firstName]!).Length == 4).ToArray();

        fourLetterFirstNames.Length.Should().Be(2);
        fourLetterFirstNames.Should().Contain(jane);
        fourLetterFirstNames.Should().Contain(john);

        var fiveLetterFirstNames = meta.Objects.Where(v => ((string)v[firstName]!).Length == 5).ToArray();
        fiveLetterFirstNames.Should().HaveCount(1);
        fiveLetterFirstNames.Should().Contain(jenny);
    }
}
