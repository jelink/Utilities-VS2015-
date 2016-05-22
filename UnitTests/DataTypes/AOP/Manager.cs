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
using System.Collections.Generic;
using UnitTests.Fixtures;
using Utilities.DataTypes;
using Utilities.DataTypes.AOP.Interfaces;
using Utilities.DataTypes.CodeGen;
using Utilities.ORM.Manager.Mapper.Interfaces;
using Xunit;

namespace UnitTests.DataTypes.AOP
{
    public interface IAOPTestInterface
    {
        string A { get; set; }

        int B { get; set; }

        float C { get; set; }

        List<string> D { get; set; }

        void TestMethod1();

        int TestMethod2();
    }

    public abstract class AOPAbstractTestClass
    {
        protected AOPAbstractTestClass()
        {
        }

        public abstract string A { get; set; }

        public abstract int B { get; set; }

        public abstract float C { get; set; }

        public abstract List<string> D { get; set; }

        public virtual string E { get; set; }

        public virtual int F { get; set; }

        public virtual float G { get; set; }

        public virtual List<string> H { get; set; }

        public abstract void TestMethod1();

        public abstract int TestMethod2();
    }

    public class AOPTestClass
    {
        public virtual string A { get; set; }

        public virtual int B { get; set; }

        public virtual float C { get; set; }

        public virtual List<string> D { get; set; }
    }

    public class Manager : TestingDirectoryFixture
    {
        [Fact]
        public void CreateAbstractClass()
        {
            var Test = new Utilities.DataTypes.AOP.Manager(new Compiler(), AppDomain.CurrentDomain.GetAssemblies().Objects<IAspect>(), AppDomain.CurrentDomain.GetAssemblies().Objects<IAOPModule>());
            Utilities.ORM.Aspect.ORMAspect.Mapper = new Utilities.ORM.Manager.Mapper.Manager(new List<IMapping>());
            var Item = (AOPAbstractTestClass)Test.Create(typeof(AOPAbstractTestClass));
            Assert.NotNull(Item);
            Item.A = "ASDF";
            Item.B = 543;
            Item.C = 9.56f;
            Item.D = new List<string>();
            Item.D.Add("POI");
            Item.E = "ASDF";
            Item.F = 543;
            Item.G = 9.56f;
            Item.H = new List<string>();
            Item.H.Add("POI");
            Assert.Equal("ASDF", Item.A);
            Assert.Equal(543, Item.B);
            Assert.Equal(9.56f, Item.C);
            Assert.Equal(1, Item.D.Count);
            Assert.Equal("POI", Item.D[0]);
            Assert.Equal("ASDF", Item.E);
            Assert.Equal(543, Item.F);
            Assert.Equal(9.56f, Item.G);
            Assert.Equal(1, Item.H.Count);
            Assert.Equal("POI", Item.H[0]);
            Item.TestMethod1();
            Assert.Equal(0, Item.TestMethod2());
        }

        [Fact]
        public void CreateClass()
        {
            var Test = new Utilities.DataTypes.AOP.Manager(new Compiler(), AppDomain.CurrentDomain.GetAssemblies().Objects<IAspect>(), AppDomain.CurrentDomain.GetAssemblies().Objects<IAOPModule>());
            Utilities.ORM.Aspect.ORMAspect.Mapper = new Utilities.ORM.Manager.Mapper.Manager(new List<IMapping>());
            var Item = (AOPTestClass)Test.Create(typeof(AOPTestClass));
            Assert.NotNull(Item);
            Item.A = "ASDF";
            Item.B = 543;
            Item.C = 9.56f;
            Item.D = new List<string>();
            Item.D.Add("POI");
            Assert.Equal("ASDF", Item.A);
            Assert.Equal(543, Item.B);
            Assert.Equal(9.56f, Item.C);
            Assert.Equal(1, Item.D.Count);
            Assert.Equal("POI", Item.D[0]);
        }

        [Fact]
        public void CreateInterface()
        {
            var Test = new Utilities.DataTypes.AOP.Manager(new Compiler(), AppDomain.CurrentDomain.GetAssemblies().Objects<IAspect>(), AppDomain.CurrentDomain.GetAssemblies().Objects<IAOPModule>());
            Utilities.ORM.Aspect.ORMAspect.Mapper = new Utilities.ORM.Manager.Mapper.Manager(new List<IMapping>());
            var Item = (IAOPTestInterface)Test.Create(typeof(IAOPTestInterface));
            Assert.NotNull(Item);
            Item.A = "ASDF";
            Item.B = 543;
            Item.C = 9.56f;
            Item.D = new List<string>();
            Item.D.Add("POI");
            Assert.Equal("ASDF", Item.A);
            Assert.Equal(543, Item.B);
            Assert.Equal(9.56f, Item.C);
            Assert.Equal(1, Item.D.Count);
            Assert.Equal("POI", Item.D[0]);
            Item.TestMethod1();
            Assert.Equal(0, Item.TestMethod2());
        }
    }
}