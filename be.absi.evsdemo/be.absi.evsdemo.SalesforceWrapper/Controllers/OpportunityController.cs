using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.SalesforceWrapper.Utilities;
using be.absi.evsdemo.Salesforce.Core;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class OpportunityController : SforceControllerBase
    {
        public Opportunity getWithSubData(string opportunityId)
        {
            string sql = string.Format(
                @"SELECT {0} FROM Opportunity where Id='{1}'",
                SforceFields.OpportunityFields,                
                opportunityId);
            return SforceProvider.Instance.Get<Opportunity>(sql);
        }
    }
}
