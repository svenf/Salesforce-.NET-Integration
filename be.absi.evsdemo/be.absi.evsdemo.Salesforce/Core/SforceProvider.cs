using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;

namespace be.absi.evsdemo.Salesforce.Core
{
    public sealed class SforceProvider : SforceControllerBase
    {
        private SforceServiceGzip _binding;
        private static SforceProvider _instance;
        LoginResult _loginResult;
        private bool _isLoggedIn;
        private static readonly object padlock = new object();
        private const int _batchSize = 1000;
        private int _loginRetryCount = 3;

        /// <summary>
        /// Lazy Singleton Instance of SalesForceController
        /// </summary>
        public static SforceProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                            _instance = new SforceProvider();
                    }
                }

                return _instance;
            }
        }

        private SforceProvider() { } // private constructor for singleton pattern

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
        }

        public SforceServiceGzip SFBinding
        {
            get
            {
                if (_binding == null)
                {
                    _binding = new SforceServiceGzip(true, true);
                }

                return _binding;
            }
        }

        public DescribeGlobalResult DescribeGlobal()
        {
            return LoginAndVerify() == false ? null : SFBinding.describeGlobal();
        }

        private bool Login()
        {
            if (_isLoggedIn == false)
            {
                lock (this)
                {
                    if (_isLoggedIn == false)
                    {
                        try
                        {
                            _loginResult = SFBinding.login(Properties.SalesforceSettings.Default.Username,
                                                           Properties.SalesforceSettings.Default.Password);
                        }
                        catch (Exception ex)
                        {
                            // silencing the exception here increases the debug problem.
                            // log to event_log or something ?
                            //LogManager.GetLogger("File").Error("SF:Login()", ex);
                            return false;
                        }

                        if (_loginResult.passwordExpired)
                        {
                            // the password for the account expired, log the error
                            return false;
                        }

                        SFBinding.SessionHeaderValue = new SessionHeader();
                        SFBinding.Url = _loginResult.serverUrl;
                        SFBinding.SessionHeaderValue.sessionId = _loginResult.sessionId;

                        _isLoggedIn = true;
                        _loginRetryCount = 3;
                    }
                }
            }

            return _isLoggedIn;

        }

        public LoginResult UserLoggedIn
        {
            get { return _loginResult; }
        }

        public LeadConvertResult[] ConvertLead(params LeadConvert[] leadConverts)
        {
            if (LoginAndVerify() == false) return null;

            return SFBinding.convertLead(leadConverts);
        }

        private bool LoginAndVerify()
        {
            return !((_isLoggedIn == false) && (Login() == false));
        }


        public SaveResult[] Update(params sObject[] newItems)
        {
            if (LoginAndVerify() == false) return null;

            return SFBinding.update(newItems);
        }

        public SaveResult[] Add(params sObject[] newItems)
        {
            if (LoginAndVerify() == false) return null;

            return SFBinding.create(newItems);


        }

        /// <summary>
        /// Lists all objects based on a query. 
        /// This function uses the query function in salesforce
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerable<T> List<T>(string query, params object[] args) where T : sObject
        {
            QueryResult queryResult = Query(query, args);

            if (queryResult != null)
            {
                return ConvertQueryResult<T>(queryResult);
            }

            return null;
        }



        /// <summary>
        /// Lists all objects based on a fieldList. 
        /// This function uses the retrieve function of salesforce
        /// </summary>
        public T[] List<T>(string fieldList, string[] id) where T : sObject
        {
            if (LoginAndVerify() == false) return null;

            sObject[] items = SFBinding.retrieve(fieldList, typeof(T).Name, id);

            return (T[])items;

        }

        public T Get<T>(string fieldList, string[] id) where T : sObject
        {
            if (LoginAndVerify() == false) return null;

            sObject[] items = SFBinding.retrieve(fieldList, typeof(T).Name, id);

            if ((items != null) && items.Length == 1)
                return (T)items[0];

            return null;
        }

        public T Get<T>(string query, params object[] args) where T : sObject
        {
            int recordsRequired = 1;
            QueryResult queryResult = Query(recordsRequired, query, args);

            if ((queryResult != null) && (queryResult.size > 0))
            {
                return (T)queryResult.records[0];
            }

            return null;

        }

        private QueryResult Query(string query, params object[] args)
        {
            return Query(_batchSize, query, args);
        }

        private QueryResult Query(int batchSize, string query, params object[] args)
        {
            if (LoginAndVerify() == false) return null;

            // This built-in string formatter gives the caller a cleaner syntax for formatting queries
            query = string.Format(query, args);

            QueryResult qr = null;
            SFBinding.QueryOptionsValue = new QueryOptions();
            SFBinding.QueryOptionsValue.batchSize = batchSize;
            SFBinding.QueryOptionsValue.batchSizeSpecified = true;

            try
            {
                //LogManager.GetLogger("File").Info("Query SF- query: " + query);
                qr = SFBinding.query(query);
            }
            catch (SoapException soapEx)
            {
                bool sessionExpired = soapEx.Code.Name.Contains("INVALID_SESSION_ID");

                // if the session has staled, we set _isLoggedIn to false and try 3 times to re-login to salesforce and
                // re-execute the query statement;
                if (sessionExpired)
                {

                    _isLoggedIn = false;

                    if (_loginRetryCount-- > 0)
                        Query(batchSize, query, args);


                }

                //System.Windows.Forms.MessageBox.Show(soapEx.Message.ToString());
                //LogManager.GetLogger("File").Error("SF:SoapException: " + query, soapEx);
            }

            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //LogManager.GetLogger("File").Error("SF:SoapException: " + query, ex);
            }

            return qr;

        }

    }
}
