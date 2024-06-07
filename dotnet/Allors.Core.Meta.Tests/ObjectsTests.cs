namespace Allors.Core.Meta.Tests;

using System;
using System.Linq;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using Xunit;

public class ObjectsTests
{
    [Fact]
    public void Filter()
    {
        var metaMeta = new MetaMeta();
        var domain = metaMeta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = metaMeta.AddUnit(domain, Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(domain, Guid.NewGuid(), "Person");
        metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");

        var meta = new Meta(metaMeta);

        IMetaObject NewPerson(string firstName, string lastName)
        {
            return meta.Build(person, v =>
            {
                v["FirstName"] = firstName;
                v["LastName"] = lastName;
            });
        }

        var jane = NewPerson("Jane", "Doe");
        var john = NewPerson("John", "Doe");
        var jenny = NewPerson("Jenny", "Doe");

        var lastNameDoe = meta.Objects.Where(v => (string)v["LastName"]! == "Doe").ToArray();

        Assert.Equal(3, lastNameDoe.Length);
        Assert.Contains(jane, lastNameDoe);
        Assert.Contains(john, lastNameDoe);
        Assert.Contains(jenny, lastNameDoe);

        var lessThanFourLetterFirstNames = meta.Objects.Where(v => ((string)v["FirstName"]!).Length < 4).ToArray();

        Assert.Empty(lessThanFourLetterFirstNames);

        var fourLetterFirstNames = meta.Objects.Where(v => ((string)v["FirstName"]!).Length == 4).ToArray();

        Assert.Equal(2, fourLetterFirstNames.Length);
        Assert.Contains(jane, fourLetterFirstNames);
        Assert.Contains(john, fourLetterFirstNames);

        var fiveLetterFirstNames = meta.Objects.Where(v => ((string)v["FirstName"]!).Length == 5).ToArray();
        Assert.Single(fiveLetterFirstNames);
        Assert.Contains(jenny, fiveLetterFirstNames);
    }
}
