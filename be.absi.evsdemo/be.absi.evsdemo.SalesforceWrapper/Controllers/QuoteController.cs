using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;
using be.absi.evsdemo.SalesforceWrapper.Utilities;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class QuoteController : SforceControllerBase
    {
        public string addQuote(Quote quote)
        {            
            return ParseFirstSaveResult(SforceProvider.Instance.Add(quote));            
        }

        public Quote getQuoteByID(string quoteId)
        {
            string sql = string.Format(@"SELECT {0}, (select {2} from quotelineitems) FROM Quote where Id='{1}'", SforceFields.QuoteFields, quoteId, SforceFields.QuoteLineItemsFields);
            return SforceProvider.Instance.Get<Quote>(sql);
        }
    }
}
