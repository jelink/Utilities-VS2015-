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

using System.Collections.Concurrent;
using System.Collections.Generic;
using Utilities.DataTypes;
using Utilities.DataTypes.Patterns.BaseClasses;
using Utilities.IO.Logging.Interfaces;

namespace Utilities.IO.Logging.BaseClasses
{
    /// <summary>
    /// Logger base
    /// </summary>
    public abstract class LoggerBase : SafeDisposableBaseClass, ILogger
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected LoggerBase()
        {
            Logs = new ConcurrentDictionary<string, ILog>();
        }

        /// <summary>
        /// Called to log the current message
        /// </summary>
        public IDictionary<string, ILog> Logs { get; private set; }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Adds a log object or replaces one already in use
        /// </summary>
        /// <param name="Name">The name of the log file</param>
        public abstract void AddLog(string Name = "Default");

        /// <summary>
        /// Gets a specified log
        /// </summary>
        /// <param name="Name">The name of the log file</param>
        /// <returns>The log file specified</returns>
        public ILog GetLog(string Name = "Default")
        {
            if (!Logs.ContainsKey(Name))
                AddLog(Name);
            return Logs.GetValue(Name);
        }

        /// <summary>
        /// String representation of the logger
        /// </summary>
        /// <returns>The name of the logger</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Disposes of the objects
        /// </summary>
        /// <param name="Managed">
        /// True to dispose of all resources, false only disposes of native resources
        /// </param>
        protected override void Dispose(bool Managed)
        {
            if (Logs != null)
            {
                foreach (KeyValuePair<string, ILog> Log in Logs)
                {
                    Log.Value.Dispose();
                }
                Logs.Clear();
            }
        }
    }
}