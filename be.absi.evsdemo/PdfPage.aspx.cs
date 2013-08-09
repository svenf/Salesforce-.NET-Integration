using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using be.absi.evsdemo.SalesforceWrapper.Controllers;

public partial class pdf_PdfPage : System.Web.UI.Page
{
    public string quoteId { get; set; }
    public Quote quote { get; set; }
    public User user { get; set; }
    public Opportunity opportunity { get; set; }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        quoteId = this.Request.QueryString["id"];
        if (!String.IsNullOrEmpty(quoteId))
        {
            this.quote = new QuoteController().getQuoteByID(quoteId);
            
            this.opportunity = new OpportunityController().getWithSubData(this.quote.OpportunityId);
            this.user = new UserController().getUserByID(this.opportunity.OwnerId);

            this.rptQuoteLineItems.DataSource = this.quote.QuoteLineItems.records.ToList();
            this.rptQuoteLineItems.DataBind();
        }
    }
}