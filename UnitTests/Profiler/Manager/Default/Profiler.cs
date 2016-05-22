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

using System.Linq;
using System.Threading;
using Xunit;

namespace UnitTests.Profiler
{
    public class Profiler
    {
        [Fact]
        public void BasicTest()
        {
            var TestObject = new Utilities.Profiler.Manager.Default.Profiler("Func1");
            Thread.Sleep(600);
            var A = new Utilities.Profiler.Manager.Default.Profiler("A");
            Thread.Sleep(600);
            A.Stop();
            TestObject.Stop();
            Assert.InRange(A.Entries.Sum(x => x.Time), 0, 1000);
            Assert.InRange(TestObject.Entries.Sum(x => x.Time), 0, 1500);
            Assert.InRange(A.Entries.Sum(x => x.Memory), 0, 100);
            Assert.InRange(A.Entries.Sum(x => x.CPUUsage), 0, 100);
        }
    }
}