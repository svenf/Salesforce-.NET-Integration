using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;
using be.absi.evsdemo.SalesforceWrapper.Utilities;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class PriceBookController : SforceControllerBase
    {
        public IEnumerable<Pricebook2> getPricebooks()
        {
            return
                SforceProvider.Instance.List<Pricebook2>(
                    "SELECT {0} FROM Pricebook2 where isactive = true", SforceFields.Pricebook2Fields);

        }

    }
}
