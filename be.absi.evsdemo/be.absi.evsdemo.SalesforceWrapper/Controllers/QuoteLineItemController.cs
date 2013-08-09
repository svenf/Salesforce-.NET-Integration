using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class QuoteLineItemController : SforceControllerBase
    {
        public string addQuoteLineItem(QuoteLineItem quoteLineItem)
        {
            return ParseFirstSaveResult(SforceProvider.Instance.Add(quoteLineItem));
        }
    }
}
