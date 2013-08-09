using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace be.absi.evsdemo.Salesforce.Core
{
    public abstract class SforceControllerBase
    {
        protected IEnumerable<T> ConvertQueryResult<T>(QueryResult result) where T : sObject
        {
            var data = new T[result.size];
            for (int i = 0; i < data.Length; i++)
            {
                yield return (T)result.records[i];
            }
        }

        protected IEnumerable<string> ParseSaveResult(SaveResult[] saveResults, out List<string> errors)
        {
            errors = null;
            if (saveResults == null)
                return null;

            var retVal = new List<string>();
            if (saveResults.Length > 0)
            {
                for (int i = 0; i < saveResults.Length; i++)
                    //add to return value if success
                    if (saveResults[i].success)
                        retVal.Add(saveResults[i].id);
                    else
                    {
                        if (errors == null) errors = new List<string>();
                        errors.Add(GetErrorMessage(saveResults[i].errors));
                    }
            }

            return retVal;
        }

        private static string GetErrorMessage(IEnumerable<Error> errors)
        {
            string errorMsg = "";
            foreach (Error err in errors)
                errorMsg += string.Format("Code:{0} - Message:{1}", err.statusCode, err.message);

            return errorMsg;
        }

        protected string ParseFirstSaveResult(params SaveResult[] saveResults)
        {
            if (saveResults == null)
                return string.Empty;

            if ((saveResults.Length == 1) && (saveResults[0].success))
            {
                return saveResults[0].id;
            }

            // fetch all errors and rethrow as exception
            string errors = String.Empty;
            foreach (SaveResult result in saveResults)
                foreach (Error err in result.errors)
                    errors += string.Format("Code:{0} - Message:{1}", err.statusCode, err.message);

            // TODO: find a way to persist the errors to easen support

            if (!String.IsNullOrEmpty(errors))
                throw new Exception(errors);

            // otherwise return empty string
            return string.Empty;
        }
    }
}
