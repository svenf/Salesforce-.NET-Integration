using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using be.absi.evsdemo.Salesforce.Utilities;

namespace be.absi.evsdemo.Salesforce.Core
{
    public sealed class SforceServiceGzip : SforceService
    {
        public SforceServiceGzip(bool enableGzip, bool keepalive)
        {
            gzip = enableGzip;
            this.keepalive = keepalive;
        }

        private bool gzip, keepalive;

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest wr = base.GetWebRequest(uri);
            if (!keepalive)
                ((HttpWebRequest)wr).KeepAlive = false;
            // sforce support compression in both directions.
            return new GzipWebRequest(wr, gzip, gzip);
        }
    }
}
