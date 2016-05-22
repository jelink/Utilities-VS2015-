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
using Utilities.Random.BaseClasses;
using Utilities.Random.Interfaces;

namespace Utilities.Random.DefaultClasses
{
    /// <summary>
    /// Randomly generates Guid
    /// </summary>
    public class GuidGenerator : GeneratorAttributeBase, IGenerator<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidGenerator"/> class.
        /// </summary>
        public GuidGenerator()
            : base(new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99999999-9999-9999-9999-999999999999"))
        {
        }

        /// <summary>
        /// Generates a random value of the specified type
        /// </summary>
        /// <param name="Rand">Random number generator that it can use</param>
        /// <returns>A randomly generated object of the specified type</returns>
        public Guid Next(System.Random Rand)
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Generates a random value of the specified type
        /// </summary>
        /// <param name="Rand">Random number generator that it can use</param>
        /// <param name="Min">Minimum value (inclusive)</param>
        /// <param name="Max">Maximum value (inclusive)</param>
        /// <returns>A randomly generated object of the specified type</returns>
        public Guid Next(System.Random Rand, Guid Min, Guid Max)
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Randomly generates an object
        /// </summary>
        /// <param name="Rand">Random number generator</param>
        /// <returns>A randomly generated object</returns>
        public override object NextObj(System.Random Rand)
        {
            return Next(Rand);
        }
    }
}