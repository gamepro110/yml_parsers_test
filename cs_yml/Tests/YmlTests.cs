using NUnit.Framework;

using System;
using System.Collections.Generic;

using yml_lib;

namespace Tests
{
    public class YmlTests
    {
        string yml;
        Person p;
        ISerializationHandler serializer;

        [SetUp]
        public void Setup()
        {
            serializer = new YmlHandler();
            yml = @"id: 56485
name: Jack Ripper
salary: 420.3
tasks:
- 20
- 51
- 674
- 5615";

            p = new Person();
            p.name = "kevin ashton";
            p.id = 1685656485;
            p.salary = 6.95;
            p.tasks = new List<int> { 5615, 452, 54 };
        }

        [Test]
        public void SerializationTest()
        {
            string output = serializer.Serialize(p);
            Assert.AreNotEqual(string.Empty, output);
        }

        [Test]
        public void DeserializationTest()
        {
            Person p2 = serializer.Deserialize<Person>(yml);
            Assert.NotNull(p2);
            Assert.True(p2.id == 56485);
            Assert.True(p2.name.ToLower() == "jack ripper");
            Assert.True(p2.salary == 420.3);

            Assert.Contains(20, p2.tasks);
            Assert.Contains(51, p2.tasks);
            Assert.Contains(674, p2.tasks);
            Assert.Contains(5615, p2.tasks);
        }

    }
}