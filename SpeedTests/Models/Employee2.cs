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

using SpeedTests.Configuration;
using Utilities.Random.ContactInfoGenerators;
using Utilities.Random.DefaultClasses;
using Utilities.Random.NameGenerators;

namespace SpeedTests.Models
{
    /// <summary>
    /// Employee test class (multiple sources)
    /// </summary>
    public class Employee2 : Utilities.ORM.ObjectBaseClass<Employee2, long>
    {
        /// <summary>
        /// City generator
        /// </summary>
        [CityGenerator]
        public virtual string City { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [FemaleFirstNameGenerator]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [LastNameGenerator]
        public virtual string LastName { get; set; }

        [IntGenerator]
        public virtual int RandomValue1 { get; set; }

        [FloatGenerator]
        public virtual float RandomValue2 { get; set; }

        /// <summary>
        /// State
        /// </summary>
        [StateGenerator]
        public virtual string State { get; set; }
    }

    /// <summary>
    /// Employee mapping
    /// </summary>
    public class EmployeeMapping2Source1 : Utilities.ORM.BaseClasses.MappingBaseClass<Employee2, Source1Config>
    {
        public EmployeeMapping2Source1()
        {
            ID(x => x.ID).SetAutoIncrement();
            Reference(x => x.DateCreated);
            Reference(x => x.DateModified);
            Reference(x => x.Active);
            Reference(x => x.City);
            Reference(x => x.FirstName);
        }
    }

    /// <summary>
    /// Employee mapping
    /// </summary>
    public class EmployeeMapping2Source2 : Utilities.ORM.BaseClasses.MappingBaseClass<Employee2, Source2Config>
    {
        public EmployeeMapping2Source2()
        {
            ID(x => x.ID).SetAutoIncrement();
            Reference(x => x.LastName);
            Reference(x => x.RandomValue1);
            Reference(x => x.RandomValue2);
            Reference(x => x.State);
        }
    }
}