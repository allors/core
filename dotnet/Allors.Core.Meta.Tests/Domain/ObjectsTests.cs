﻿namespace Allors.Core.Meta.Tests.Domain;

using System.Linq;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Xunit;

public class ObjectsTests
{
    [Fact]
    public void Filter()
    {
        var meta = new MetaMeta();
        var person = meta.AddClass("Person");
        meta.AddUnit<string>(person, "FirstName");
        meta.AddUnit<string>(person, "LastName");

        var population = new MetaPopulation(meta);

        IMetaObject NewPerson(string firstName, string lastName)
        {
            return population.Build(person, v =>
            {
                v["FirstName"] = firstName;
                v["LastName"] = lastName;
            });
        }

        var jane = NewPerson("Jane", "Doe");
        var john = NewPerson("John", "Doe");
        var jenny = NewPerson("Jenny", "Doe");

        var lastNameDoe = population.Objects.Where(v => (string)v["LastName"]! == "Doe").ToArray();

        Assert.Equal(3, lastNameDoe.Length);
        Assert.Contains(jane, lastNameDoe);
        Assert.Contains(john, lastNameDoe);
        Assert.Contains(jenny, lastNameDoe);

        var lessThanFourLetterFirstNames = population.Objects.Where(v => ((string)v["FirstName"]!).Length < 4).ToArray();

        Assert.Empty(lessThanFourLetterFirstNames);

        var fourLetterFirstNames = population.Objects.Where(v => ((string)v["FirstName"]!).Length == 4).ToArray();

        Assert.Equal(2, fourLetterFirstNames.Length);
        Assert.Contains(jane, fourLetterFirstNames);
        Assert.Contains(john, fourLetterFirstNames);

        var fiveLetterFirstNames = population.Objects.Where(v => ((string)v["FirstName"]!).Length == 5).ToArray();
        Assert.Single(fiveLetterFirstNames);
        Assert.Contains(jenny, fiveLetterFirstNames);
    }
}
