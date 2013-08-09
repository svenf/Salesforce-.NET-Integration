using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace be.absi.evsdemo.SalesforceWrapper.Utilities
{
    internal class DateTimeIsoFormat
    {
        public static string ToIsoFormat(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
