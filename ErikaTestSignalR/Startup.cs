using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErikaTestSignalR
{
    class Startup
    {

        //itt történik hogy összekapcsolja a hubokat, neked nem kell 
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true
            };
            app.MapSignalR(hubConfiguration);
        }
    }


    //public class Startup
    //{
    //    public void Configuration(IAppBuilder app)
    //    {
    //        app.Map("/signalr", map =>
    //        {
    //            // Setup the cors middleware to run before SignalR.
    //            // By default this will allow all origins. You can 
    //            // configure the set of origins and/or http verbs by
    //            // providing a cors options with a different policy.
    //            map.UseCors(CorsOptions.AllowAll);

    //            var hubConfiguration = new HubConfiguration
    //            {
    //                // You can enable JSONP by uncommenting line below.
    //                // JSONP requests are insecure but some older browsers (and some
    //                // versions of IE) require JSONP to work cross domain
    //                // EnableJSONP = true
    //            };

    //            // Run the SignalR pipeline. We're not using MapSignalR
    //            // since this branch is already runs under the "/signalr"
    //            // path.
    //            map.RunSignalR(hubConfiguration);
    //        });
    //    }
    //}    

}
