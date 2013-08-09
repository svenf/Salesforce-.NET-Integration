using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace be.absi.evsdemo.SalesforceWrapper.Utilities
{
    public class SforceFields
    {
        //TODO: improve the way of doing this
        public const string LeadFields = "id, email, firstname, lastname, leadsource, rating, status, Country, HasOptedOutOfEmail, CreatedDate";
        public const string AccountFields = "id, name, website, phone, billingstreet, billingcity, billingstate, billingpostalcode, billingcountry";
        public const string ContactFields = "id, name, email";
        public const string ContractFields = "id, accountid, ContractNumber, ActivatedDate, companysignedid, customersignedid, createdbyid, createddate, customersigneddate, startdate, enddate, ownerid, status";
        public const string ProductFields = "Name, Id, ProductCode";
        public const string OpportunityFields = "Id, Name, account.id ,account.name, account.billingstreet, account.billingcity, account.billingstate, account.billingpostalcode, account.billingcountry, ownerid";
        public const string OpportunityLineItemFields = "Name, Id, ProductCode, listprice";
        public const string Pricebook2Fields = "Id, Name";
        public const string PricebookEntryFields = "Id, Name, Pricebook2Id, UnitPrice, ProductCode";
        public const string QuoteFields = "Id, billingName, billingStreet, billingCity, billingPostalCode, billingCountry, shippingName, contact.Name, contact.Email, quoteNumber, createdDate, createdByID, opportunityId";
        public const string UserFields = "Id, Name, Email";
        public const string QuoteLineItemsFields = "Id, ListPrice, Quantity, totalPrice, unitprice, LineNumber, pricebookentry.Name";
    }
}
