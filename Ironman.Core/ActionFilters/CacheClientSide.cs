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
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ironman.Core.ActionFilters
{
    /// <summary>
    /// Caches items client side
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ClientSideCache : ActionFilterAttribute
    {
        /// <summary>
        /// Days that the item should be cached
        /// </summary>
        public virtual int DaysToCache { get; set; }

        /// <summary>
        /// Action executing
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                return;
            HttpContextBase Context = filterContext.HttpContext;
            Context.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;
            DateTime Date = GetDateLastModified(Context);
            string Etag = GetETag(Context);
            string IncomingEtag = Context.Request.Headers["If-None-Match"];
            string ModifiedSince = Context.Request.Headers["If-Modified-Since"];
            DateTime ModifiedSinceDate = DateTime.Now;
            if (!DateTime.TryParse(ModifiedSince, out ModifiedSinceDate))
                ModifiedSinceDate = DateTime.Now;

            Date = DateTime.Parse(Date.ToString("r"));

            Context.Response.Cache.SetLastModified(Date);
            Context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime().AddDays(DaysToCache));
            Context.Response.Cache.SetCacheability(HttpCacheability.Public);
            Context.Response.Cache.SetMaxAge(new TimeSpan(DaysToCache, 0, 0, 0));
            Context.Response.Cache.SetRevalidation(HttpCacheRevalidation.None);
            Context.Response.Cache.SetETag(Etag);
            if (string.Compare(IncomingEtag, Etag, StringComparison.Ordinal) == 0
                || Date.CompareTo(ModifiedSinceDate) <= 0)
            {
                Context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Gets the date that file was last modified
        /// </summary>
        /// <param name="Context">HTTP Context</param>
        /// <returns>The date that the file was last modified</returns>
        private static DateTime GetDateLastModified(HttpContextBase Context)
        {
            if (Context == null || Context.Cache == null)
                return DateTime.Now;
            DateTime Date = DateTime.Now;
            if (Context.Cache[Context.Request.Path + "date"] != null)
                Date = (DateTime)Context.Cache[Context.Request.Path + "date"];
            else
                Context.Cache[Context.Request.Path + "date"] = Date;
            return Date;
        }

        /// <summary>
        /// Gets the incoming etag
        /// </summary>
        /// <param name="Context">HTTP context</param>
        /// <returns>The string ETag</returns>
        private static string GetETag(HttpContextBase Context)
        {
            if (Context == null || string.IsNullOrEmpty(Context.Request.Path))
                return "\"\"";
            return "\"" + Context.Request.Path.GetHashCode() + "\"";
        }
    }
}