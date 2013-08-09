using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;
using be.absi.evsdemo.SalesforceWrapper.Utilities;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class ProductController : SforceControllerBase
    {
        public IEnumerable<Product2> getProductList()
        {
            return
                SforceProvider.Instance.List<Product2>(
                    "SELECT {0} FROM Product2", SforceFields.ProductFields);

        }
    }
}
