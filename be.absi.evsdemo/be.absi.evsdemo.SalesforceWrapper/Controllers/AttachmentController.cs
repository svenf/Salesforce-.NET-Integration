using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class AttachmentController : SforceControllerBase
    {
        public string addQuote(Attachment a)
        {
            return ParseFirstSaveResult(SforceProvider.Instance.Add(a));
        }
    }
}
