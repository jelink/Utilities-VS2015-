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
using System.Diagnostics.Contracts;
using System.Reflection;
using Utilities.IoC.Interfaces.Contracts;

namespace Utilities.IoC.Interfaces.Contracts
{
    /// <summary>
    /// IBootstrapper contract class
    /// </summary>
    [ContractClassFor(typeof(IBootstrapper))]
    internal abstract class IBootstrapperContract : IBootstrapper
    {
        /// <summary>
        /// Name of the bootstrapper
        /// </summary>
        public string Name
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return "";
            }
        }

        /// <summary>
        /// Adds the assembly.
        /// </summary>
        /// <param name="Assemblies">The assemblies.</param>
        public void AddAssembly(params Assembly[] Assemblies)
        {
            Contract.Requires<ArgumentNullException>(Assemblies != null);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Registers an object with the bootstrapper
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">Object to register</param>
        /// <param name="Name">Name associated with the object</param>
        public void Register<T>(T Object, string Name = "") where T : class
        {
            Contract.Requires<ArgumentNullException>(Name != null);
        }

        /// <summary>
        /// Registers a type with the default constructor
        /// </summary>
        /// <typeparam name="T">Object type to register</typeparam>
        /// <param name="Name">Name associated with the object</param>
        public void Register<T>(string Name = "") where T : class, new()
        {
            Contract.Requires<ArgumentNullException>(Name != null);
        }

        /// <summary>
        /// Registers a type with the default constructor of a child class
        /// </summary>
        /// <typeparam name="T1">Base class/interface type</typeparam>
        /// <typeparam name="T2">Child class type</typeparam>
        /// <param name="Name">Name associated with the object</param>
        public void Register<T1, T2>(string Name = "")
            where T1 : class
            where T2 : class, T1
        {
            Contract.Requires<ArgumentNullException>(Name != null);
        }

        /// <summary>
        /// Registers a type with a function
        /// </summary>
        /// <typeparam name="T">Type that the function returns</typeparam>
        /// <param name="Function">Function to register with the type</param>
        /// <param name="Name">Name associated with the object</param>
        public void Register<T>(Func<T> Function, string Name = "") where T : class
        {
            Contract.Requires<ArgumentNullException>(Function != null);
        }

        /// <summary>
        /// Registers all objects of a certain type with the bootstrapper
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        public void RegisterAll<T>() where T : class
        {
        }

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>
        /// An object of the specified type
        /// </returns>
        public T Resolve<T>(T DefaultObject = default(T)) where T : class
        {
            return default(T);
        }

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="Name">Name associated with the object</param>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>
        /// An object of the specified type
        /// </returns>
        public T Resolve<T>(string Name, T DefaultObject = default(T)) where T : class
        {
            Contract.Requires<ArgumentNullException>(Name != null);
            return default(T);
        }

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>
        /// An object of the specified type
        /// </returns>
        public object Resolve(Type ObjectType, object DefaultObject = null)
        {
            Contract.Requires<ArgumentNullException>(ObjectType != null);
            return default(object);
        }

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Name">Name associated with the object</param>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>
        /// An object of the specified type
        /// </returns>
        public object Resolve(Type ObjectType, string Name, object DefaultObject = null)
        {
            Contract.Requires<ArgumentNullException>(ObjectType != null);
            Contract.Requires<ArgumentNullException>(Name != null);
            return default(object);
        }

        /// <summary>
        /// Resolves the objects based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>
        /// A list of objects of the specified type
        /// </returns>
        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
            return new List<T>();
        }

        /// <summary>
        /// Resolves all objects based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <returns>
        /// A list of objects of the specified type
        /// </returns>
        public IEnumerable<object> ResolveAll(Type ObjectType)
        {
            Contract.Requires<ArgumentNullException>(ObjectType != null);
            Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);
            return new List<object>();
        }
    }
}