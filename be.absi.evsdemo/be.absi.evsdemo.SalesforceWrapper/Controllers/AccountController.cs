using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using be.absi.evsdemo.Salesforce.Core;
using be.absi.evsdemo.SalesforceWrapper.Utilities;

namespace be.absi.evsdemo.SalesforceWrapper.Controllers
{
    public class AccountController : SforceControllerBase
    {
        public Account GetWithSubData(string accountId)
        {
            string sql = string.Format(
                @"SELECT {0}, (select {1} from contracts), (select {2} from contacts) FROM Account where Id='{3}'",
                SforceFields.AccountFields,
                SforceFields.ContractFields,
                SforceFields.ContactFields,
                accountId);

            return SforceProvider.Instance.Get<Account>(sql);

        }

        public IEnumerable<Account> List()
        {
            return
                SforceProvider.Instance.List<Account>(
                    "SELECT {0} FROM Account where name <> ''", SforceFields.AccountFields);

        }

        public Account Get(string accountId)
        {
            return
                SforceProvider.Instance.Get<Account>(
                    "SELECT {0} FROM Account where Id='{1}'", SforceFields.AccountFields, accountId);

        }

        public Account GetByName(string accountName)
        {
            return
                SforceProvider.Instance.Get<Account>(
                    "SELECT {0} from Account where Name='{1}'", SforceFields.AccountFields, accountName);

        }

        public string Update(Account account)
        {
            return ParseFirstSaveResult(SforceProvider.Instance.Update(account));
        }
    }
}
