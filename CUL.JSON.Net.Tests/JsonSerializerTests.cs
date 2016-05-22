﻿using CUL.Serialization;
using System.Dynamic;
using Utilities.DataTypes;
using Xunit;

namespace CUL.JSON.Net.Tests
{
    public class JsonSerializerTests
    {
        [Fact]
        public void Creation()
        {
            var Serializer = new JsonSerializer();
            Assert.Equal("application/json", Serializer.ContentType);
            Assert.Equal(".json", Serializer.FileType);
            Assert.Equal("JSON.Net", Serializer.Name);
            Assert.Equal(typeof(string), Serializer.ReturnType);
        }

        [Fact]
        public void Deserialize()
        {
            var Serializer = new JsonSerializer();
            dynamic Object = (TestObject)Serializer.Deserialize(typeof(TestObject), "{\"A\":5,\"B\":\"ASDF\"}");
            Assert.Equal(5, Object.A);
            Assert.Equal("ASDF", Object.B);
            Object = (ExpandoObject)Serializer.Deserialize(typeof(ExpandoObject), "{\"A\":5,\"B\":\"ASDF\"}");
            Assert.Equal(5, Object.A);
            Assert.Equal("ASDF", Object.B);
            Object = (Dynamo)Serializer.Deserialize(typeof(Dynamo), "{\"A\":5,\"B\":\"ASDF\"}");
            Assert.Equal(5, Object.A);
            Assert.Equal("ASDF", Object.B);
        }

        [Fact]
        public void NullDeserialize()
        {
            var Serializer = new JsonSerializer();
            Assert.Equal(null, Serializer.Deserialize(typeof(object), null));
            Assert.Equal(null, Serializer.Deserialize(typeof(object), ""));
            Assert.Equal(null, Serializer.Deserialize(null, "ASDF"));
            Assert.Equal(null, Serializer.Deserialize(null, null));
            Assert.Equal(null, Serializer.Deserialize(null, ""));
        }

        [Fact]
        public void NullSerialize()
        {
            var Serializer = new JsonSerializer();
            Assert.Equal("", Serializer.Serialize(typeof(object), null));
            Assert.Equal("", Serializer.Serialize(null, new { A = "" }));
            Assert.Equal("", Serializer.Serialize(null, null));
        }

        [Fact]
        public void Serialize()
        {
            var Serializer = new JsonSerializer();
            dynamic Object = new { A = 5, B = "ASDF" };
            Assert.Equal("{\"A\":5,\"B\":\"ASDF\"}", Serializer.Serialize(Object.GetType(), Object));
            Object = new TestObject { A = 5, B = "ASDF" };
            Assert.Equal("{\"A\":5,\"B\":\"ASDF\"}", Serializer.Serialize(Object.GetType(), Object));
            Object = new ExpandoObject();
            Object.A = 5;
            Object.B = "ASDF";
            Assert.Equal("{\"A\":5,\"B\":\"ASDF\"}", Serializer.Serialize(Object.GetType(), Object));
            Object = new Dynamo();
            Object.A = 5;
            Object.B = "ASDF";
            Assert.Equal("{\"A\":5,\"B\":\"ASDF\"}", Serializer.Serialize(Object.GetType(), Object));
        }

        public class TestObject
        {
            public int A { get; set; }

            public string B { get; set; }
        }
    }
}