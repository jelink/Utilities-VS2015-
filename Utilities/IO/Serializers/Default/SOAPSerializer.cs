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
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using Utilities.IO.Serializers.BaseClasses;

namespace Utilities.IO.Serializers.Default
{
    /// <summary>
    /// SOAP serializer
    /// </summary>
    public class SOAPSerializer : SerializerBase<string>
    {
        /// <summary>
        /// Content type (MIME type)
        /// </summary>
        public override string ContentType { get { return "application/soap+xml"; } }

        /// <summary>
        /// File type
        /// </summary>
        public override string FileType { get { return ".soap"; } }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return "SOAP"; } }

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Data">Data to deserialize</param>
        /// <returns>The deserialized data</returns>
        public override object Deserialize(Type ObjectType, string Data)
        {
            if (string.IsNullOrEmpty(Data) || ObjectType == null)
                return null;
            using (MemoryStream Stream = new MemoryStream(Encoding.UTF8.GetBytes(Data)))
            {
                var Formatter = new SoapFormatter();
                return Formatter.Deserialize(Stream);
            }
        }

        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Data">Data to serialize</param>
        /// <returns>The serialized data</returns>
        public override string Serialize(Type ObjectType, object Data)
        {
            if (Data == null || ObjectType == null)
                return null;
            using (MemoryStream Stream = new MemoryStream())
            {
                var Serializer = new SoapFormatter();
                Serializer.Serialize(Stream, Data);
                Stream.Flush();
                return Encoding.UTF8.GetString(Stream.GetBuffer(), 0, (int)Stream.Position);
            }
        }
    }
}