using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;
using be.absi.evsdemo.SalesforceWrapper.Utilities;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class PriceBookEntryController : SforceControllerBase
    {
        public IEnumerable<PricebookEntry> getPriceBookEntries(string pricebookId)
        {
            return
                SforceProvider.Instance.List<PricebookEntry>(
                    "SELECT {0} FROM PricebookEntry WHERE Pricebook2Id ='{1}' and isActive = true order by name", SforceFields.PricebookEntryFields, pricebookId);

        }
    }
}
