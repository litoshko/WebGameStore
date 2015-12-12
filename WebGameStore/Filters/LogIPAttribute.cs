using System;
using System.Collections.Generic;
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
    public class LogIPAttribute : Attribute, IActionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool AllowMultiple {
            get { return false; }
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            HttpResponseMessage result = await continuation();
            var userHostAddress = HttpContext.Current.Request.UserHostAddress;
            Log.Debug($"Request IP address {userHostAddress}");
            return result;
        }
    }
}