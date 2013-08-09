using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;
using be.absi.evsdemo.SalesforceWrapper.Utilities;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class ContactController : SforceControllerBase
    {
        public IEnumerable<Contact> getContactsByAccount(string accountID)
        {
            return
                SforceProvider.Instance.List<Contact>(
                    "SELECT {0} FROM Contact WHERE accountID = '{1}'", SforceFields.ContactFields, accountID);

        }
    }
}
