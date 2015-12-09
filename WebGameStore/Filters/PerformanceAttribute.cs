using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;

namespace WebGameStore.Filters
{
    public class PerformanceAttribute : Attribute, IActionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            Stopwatch timer = Stopwatch.StartNew();
            HttpResponseMessage result = await continuation();
            double seconds = timer.ElapsedMilliseconds / 1000.0;
            Log.Debug($"Method {actionContext.ActionDescriptor.ActionName} worked {seconds} seconds");
            return result;
        }
    }

}