﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Utilities.IO.Serializers;

namespace Utilities.IO
{
    /// <summary>
    /// Extension methods dealing with serialization
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class SerializationExtensions
    {
        /// <summary>
        /// Deserializes the data based on the MIME content type specified (defaults to json)
        /// </summary>
        /// <typeparam name="R">Return type expected</typeparam>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Data">Data to deserialize</param>
        /// <param name="ContentType">Content type (MIME type)</param>
        /// <returns>The deserialized object</returns>
        public static R Deserialize<R, T>(this T Data, string ContentType = "application/json")
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(ContentType), "ContentType");
            var TempManager = IoC.Manager.Bootstrapper.Resolve<Manager>();
            if (TempManager == null)
                return default(R);
            return (R)TempManager.Deserialize<T>(Data, typeof(R), ContentType);
        }

        /// <summary>
        /// Deserializes the data based on the content type specified (defaults to json)
        /// </summary>
        /// <typeparam name="R">Return type expected</typeparam>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Data">Data to deserialize</param>
        /// <param name="ContentType">Content type</param>
        /// <returns>The deserialized object</returns>
        public static R Deserialize<R, T>(this T Data, SerializationType ContentType)
        {
            if (ContentType == null)
                ContentType = SerializationType.JSON;
            var TempManager = IoC.Manager.Bootstrapper.Resolve<Manager>();
            if (TempManager == null)
                return default(R);
            return (R)TempManager.Deserialize<T>(Data, typeof(R), ContentType);
        }

        /// <summary>
        /// Serializes the data based on the MIME content type specified (defaults to json)
        /// </summary>
        /// <typeparam name="R">Return type expected</typeparam>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">Object to serialize</param>
        /// <param name="ContentType">Content type (MIME type)</param>
        /// <returns>The serialized object</returns>
        public static R Serialize<R, T>(this T Object, string ContentType = "application/json")
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(ContentType), "ContentType");
            var TempManager = IoC.Manager.Bootstrapper.Resolve<Manager>();
            if (TempManager == null)
                return default(R);
            return TempManager.Serialize<T, R>(Object, ContentType);
        }

        /// <summary>
        /// Serializes the data based on the type specified (defaults to json)
        /// </summary>
        /// <typeparam name="R">Return type expected</typeparam>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">Object to serialize</param>
        /// <param name="ContentType">Content type</param>
        /// <returns>The serialized object</returns>
        public static R Serialize<R, T>(this T Object, SerializationType ContentType)
        {
            if (ContentType == null)
                ContentType = SerializationType.JSON;
            var TempManager = IoC.Manager.Bootstrapper.Resolve<Manager>();
            if (TempManager == null)
                return default(R);
            return TempManager.Serialize<T, R>(Object, ContentType);
        }
    }

    /// <summary>
    /// Serialization enum like class
    /// </summary>
    public class SerializationType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Name</param>
        protected SerializationType(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Binary
        /// </summary>
        public static SerializationType Binary { get { return new SerializationType("application/octet-stream"); } }

        /// <summary>
        /// JSON
        /// </summary>
        public static SerializationType JSON { get { return new SerializationType("application/json"); } }

        /// <summary>
        /// SOAP
        /// </summary>
        public static SerializationType SOAP { get { return new SerializationType("application/soap+xml"); } }

        /// <summary>
        /// XML
        /// </summary>
        public static SerializationType XML { get { return new SerializationType("text/xml"); } }

        private string Name { get; set; }

        /// <summary>
        /// Converts the object to a string implicitly
        /// </summary>
        /// <param name="Object">Object to convert</param>
        /// <returns>The string version of the serialization type</returns>
        public static implicit operator string(SerializationType Object)
        {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            return Object.ToString();
        }

        /// <summary>
        /// Returns the name of the serialization type
        /// </summary>
        /// <returns>Name</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}