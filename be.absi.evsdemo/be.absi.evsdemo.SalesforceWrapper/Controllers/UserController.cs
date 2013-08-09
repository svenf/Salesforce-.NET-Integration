using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;
using be.absi.evsdemo.SalesforceWrapper.Utilities;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class UserController : SforceControllerBase
    {
        public User getUserByID(string userId)
        {
            string sql = string.Format(@"SELECT {0} FROM User where Id='{1}'", SforceFields.UserFields, userId);
            return SforceProvider.Instance.Get<User>(sql);
        }
    }
}
