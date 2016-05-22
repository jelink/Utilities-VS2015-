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

using System.Data.Common;
using Utilities.ORM.Manager.QueryProvider.BaseClasses;
using Utilities.ORM.Manager.QueryProvider.Interfaces;

namespace Utilities.ORM.Parameters
{
    /// <summary>
    /// Parameter class handling strings, checking for inequality
    /// </summary>
    public class StringNotEqualParameter : ParameterBase<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Value">Value of the parameter</param>
        /// <param name="ID">Name of the parameter</param>
        /// <param name="ParameterStarter">
        /// What the database expects as the parameter starting string ("@" for SQL Server, ":" for
        /// Oracle, etc.)
        /// </param>
        /// <param name="Length">Max length allowed for the string</param>
        /// <param name="FieldName">Field name</param>
        public StringNotEqualParameter(string Value, string ID, int Length, string FieldName = "", string ParameterStarter = "@")
            : base(ID, Value, System.Data.ParameterDirection.Input, ParameterStarter)
        {
            this.Length = Length;
            this.FieldName = string.IsNullOrEmpty(FieldName) ? ID : FieldName;
        }

        /// <summary>
        /// Field name
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Max length of the string
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Adds the parameter to the SQLHelper
        /// </summary>
        /// <param name="Helper">SQLHelper to add the parameter to</param>
        public override void AddParameter(DbCommand Helper)
        {
            Helper.AddParameter(ID, Value);
        }

        /// <summary>
        /// Creates a copy of the parameter
        /// </summary>
        /// <param name="Suffix">Suffix to add to the parameter (for batching purposes)</param>
        /// <returns>A copy of the parameter</returns>
        public override IParameter CreateCopy(string Suffix)
        {
            return new StringNotEqualParameter(Value, ID + Suffix, Length, FieldName, ParameterStarter);
        }

        /// <summary>
        /// Outputs the param as a string
        /// </summary>
        /// <returns>The param as a string</returns>
        public override string ToString()
        {
            return FieldName + "<>" + ParameterStarter + ID;
        }
    }
}