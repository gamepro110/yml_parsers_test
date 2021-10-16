using System;
using System.Collections;
using System.Collections.Generic;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace yml_lib
{
    public record Person
    {
        public int id;
        public string name;
        public double salary;
        public List<int> tasks;
    }

    public interface ISerializationHandler
    {
        public string Serialize<T>(T thing);
        public T Deserialize<T>(string text) where T : class;
    }

    public class YmlHandler : ISerializationHandler
    {
        public string Serialize<T>(T thing)
        {
            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return serializer.Serialize(thing);
        }

        public T Deserialize<T>(string ymlText) where T : class
        {
            IDeserializer Deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            return Deserializer.Deserialize<T>(ymlText);
        }
    }
}
