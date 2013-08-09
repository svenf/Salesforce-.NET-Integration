using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using be.absi.evsdemo.SalesforceWrapper.Controllers;
using System.Data;
using System.Collections.Specialized;
using be.absi.evsdemo.Dao;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    public string OpportunityId { get; set; }
    public Opportunity opportunity { get; set; }
    public List<PricebookEntry> lsPriceBookEntries { get; set; }
    public List<ListItem> lsPriceBookEntryOptions { get; set; }
    public List<Contact> lsContacts { get; set; }
    public List<ListItem> lsContactOptions { get; set; }
    public List<product> lsDbProducts { get; set; }
    public List<Pricebook2> lsPriceBooks { get; set; }
    public List<ListItem> lsPriceBookItems { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        OpportunityId = this.Request.QueryString["id"];

        if (!String.IsNullOrEmpty(OpportunityId))
        {
            this.fillInData();

            if (!Page.IsPostBack)
            {
                SetInitialRow();
            }
        }
        else
        {
            info.Visible = false;
            lblAccountResult.Text = "No opportunity ID was found.";
        }
    }

    protected void fillInData()
    {        
        this.opportunity = new OpportunityController().getWithSubData(OpportunityId);

        this.txtAccountName.Text = this.opportunity.Account.Name;
        this.txtAccountBillingcountry.Text = this.opportunity.Account.BillingCountry;
        this.txtAccountBillingpostalcode.Text = this.opportunity.Account.BillingPostalCode;
        this.txtAccountBillingState.Text = this.opportunity.Account.BillingState;
        this.txtAccountBillingstreet.Text = this.opportunity.Account.BillingStreet;
        this.txtAccountBillingCity.Text = this.opportunity.Account.BillingCity;

        this.createContactOptions();
        this.ddlContacts.DataTextField = "Text";
        this.ddlContacts.DataValueField = "Value";
        this.ddlContacts.DataSource = this.lsContactOptions;
        this.ddlContacts.DataBind();
            
        if (!IsPostBack)
        {
            this.createPriceBookOptions();
            this.ddlPricebooks.DataTextField = "Text";
            this.ddlPricebooks.DataValueField = "Value";
            this.ddlPricebooks.DataSource = this.lsPriceBookItems;
            this.ddlPricebooks.DataBind();
            this.createProductOptions();
        }
    }

    protected void createProductOptions()
    {
        this.lsPriceBookEntryOptions = new List<ListItem>();
        this.lsPriceBookEntries = new PriceBookEntryController().getPriceBookEntries(ddlPricebooks.SelectedValue).ToList();

        this.lsPriceBookEntryOptions.Add(new ListItem("None", "0"));
        foreach (PricebookEntry pbe in this.lsPriceBookEntries)
        {
            this.lsPriceBookEntryOptions.Add(new ListItem(pbe.Name, pbe.Id));
        }
    }

    protected void createContactOptions()
    {
        this.lsContactOptions = new List<ListItem>();
        this.lsContacts = new ContactController().getContactsByAccount(this.opportunity.Account.Id).ToList();

        foreach (Contact c in lsContacts)
        {
            this.lsContactOptions.Add(new ListItem(c.Name, c.Id));
        }
    }

    protected void createPriceBookOptions()
    {
        this.lsPriceBookItems = new List<ListItem>();
        this.lsPriceBooks = new PriceBookController().getPricebooks().ToList();

        foreach (Pricebook2 pb2 in this.lsPriceBooks)
        {
            this.lsPriceBookItems.Add(new ListItem(pb2.Name, pb2.Id));
        }
    }

    protected void gvQuoteLineItems_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["QuoteLineItemTable"];
            LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dt.Rows.Count - 1)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }

    protected void gvQuoteLineItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            var ddlProducts = (DropDownList)e.Row.FindControl("ddlProducts");
            ddlProducts.DataTextField = "Text";
            ddlProducts.DataValueField = "Value";
            ddlProducts.DataSource = this.lsPriceBookEntryOptions;
            ddlProducts.DataBind();
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("ProductName", typeof(string)));
        dt.Columns.Add(new DataColumn("ProductReference", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitPrice", typeof(string)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(string))); 
        dr = dt.NewRow();        
        dr["ProductName"] = string.Empty;
        dr["ProductReference"] = string.Empty;
        dr["UnitPrice"] = string.Empty;
        dr["Quantity"] = string.Empty; ; 
        dt.Rows.Add(dr);
        //Store the DataTable in ViewState
        ViewState["QuoteLineItemTable"] = dt;

        Gridview1.DataSource = dt;
        Gridview1.DataBind();        
    }

    protected void ddlPriceBooks_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlPriceBooksIndexChanged();
        
    }

    protected void ddlPriceBooksIndexChanged()
    {
        DropDownList ddlProduct = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("ddlProducts");
        this.createProductOptions();
        ddlProduct.DataSource = lsPriceBookEntryOptions;
        ddlProduct.DataBind();

        //this.ddlProducts_SelectedIndexChanged(sender, e);
    }

    protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlStatus.NamingContainer;
        
        DropDownList ddlProduct = (DropDownList)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("ddlProducts");

        Label lblReference = (Label)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("lblProductReference");
        TextBox txtUnitPrice = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtUnitPrice");
        TextBox txtQuant = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtQuantity");
        this.createProductOptions();
        var q = from pbe in this.lsPriceBookEntries
                where pbe.Id == ddlProduct.SelectedValue
                select pbe;
        if (ddlProduct.SelectedIndex > 1)
        {
            lblReference.Text = q.ToList().FirstOrDefault().ProductCode;
            txtUnitPrice.Text = q.ToList().FirstOrDefault().UnitPrice.ToString();
            txtQuant.Text = "1";
            lblReference.DataBind();
            txtUnitPrice.DataBind();
            txtQuant.DataBind();
        }
        else
        {
            lblReference.Text = "";
            txtUnitPrice.Text = "";
            txtQuant.Text = "";
            lblReference.DataBind();
            txtUnitPrice.DataBind();
            txtQuant.DataBind();
        }
    }

    protected void lbtnRemove_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;
        if (ViewState["QuoteLineItemTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["QuoteLineItemTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlProduct = (DropDownList)Gridview1.Rows[rowIndex].Cells[0].FindControl("ddlProducts");
                    Label lblReference = (Label)Gridview1.Rows[rowIndex].Cells[1].FindControl("lblProductReference");
                    TextBox txtUnit = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtUnitPrice");
                    TextBox txtQuant = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                    drCurrentRow = dtCurrentTable.NewRow();

                    this.createProductOptions();
                    foreach (ListItem item in this.lsPriceBookEntryOptions)
                    {
                        ddlProduct.Items.Add(item);
                    }

                    drCurrentRow["ProductName"] = ddlProduct.SelectedItem.Value;
                    drCurrentRow["ProductReference"] = lblReference.Text;
                    drCurrentRow["UnitPrice"] = txtUnit.Text;
                    drCurrentRow["Quantity"] = txtQuant.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["QuoteLineItemTable"] = dtCurrentTable;

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
            }
        }
        
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["QuoteLineItemTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["QuoteLineItemTable"];
            if (dt.Rows.Count > 0)
            {
                if (gvRow.RowIndex < dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
                        
                //Store the current data in ViewState for future reference
                ViewState["QuoteLineItemTable"] = dt;
                //Re bind the GridView for the updated data
                rowIndex = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlProduct = (DropDownList)Gridview1.Rows[rowIndex].Cells[0].FindControl("ddlProducts");
                    

                        this.createProductOptions();
                        foreach (ListItem item in this.lsPriceBookEntryOptions)
                        {
                            ddlProduct.Items.Add(item);
                        }

                    
                        rowIndex++;
                    }
                } 
                Gridview1.DataSource = dt;
                Gridview1.DataBind();
                //Set Previous Data on Postbacks
                SetPreviousData();
                ddlPriceBooksIndexChanged();
            }
        }
    }

    protected void btnAddQuoteLineItem_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["QuoteLineItemTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["QuoteLineItemTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlProduct = (DropDownList)Gridview1.Rows[rowIndex].Cells[0].FindControl("ddlProducts");
                    Label lblReference = (Label)Gridview1.Rows[rowIndex].Cells[1].FindControl("lblProductReference");
                    TextBox txtUnit = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtUnitPrice");
                    TextBox txtQuant = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtQuantity");                    
                    drCurrentRow = dtCurrentTable.NewRow();
                    
                    this.createProductOptions();
                    foreach (ListItem item in this.lsPriceBookEntryOptions)
                    {
                        ddlProduct.Items.Add(item);
                    }

                    drCurrentRow["ProductName"] = ddlProduct.SelectedItem.Value;
                    drCurrentRow["ProductReference"] = lblReference.Text;
                    drCurrentRow["UnitPrice"] = txtUnit.Text;
                    drCurrentRow["Quantity"] = txtQuant.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["QuoteLineItemTable"] = dtCurrentTable;

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["QuoteLineItemTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["QuoteLineItemTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlProduct = (DropDownList)Gridview1.Rows[rowIndex].Cells[0].FindControl("ddlProducts");
                    Label lblReference = (Label)Gridview1.Rows[rowIndex].Cells[1].FindControl("lblProductReference");
                    TextBox txtUnit = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtUnitPrice");
                    TextBox txtQuant = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtQuantity");

                    this.createProductOptions();
                    foreach (ListItem item in this.lsPriceBookEntryOptions)
                    {
                        ddlProduct.Items.Add(item);
                    }                   

                    ddlProduct.SelectedValue = dt.Rows[i]["ProductName"].ToString();
                    txtQuant.Text = dt.Rows[i]["Quantity"].ToString();
                    lblReference.Text = dt.Rows[i]["ProductReference"].ToString();
                    txtUnit.Text = dt.Rows[i]["UnitPrice"].ToString();
                    rowIndex++;
                }
            }  
        }
    }

    protected void btnAddQuote_Click(object sender, EventArgs e)
    {
        Quote q = new Quote();
        q.Name = txtQuoteName.Text;
        
        q.OpportunityId = this.opportunity.Id;
        q.BillingName = this.opportunity.Account.Name;
        q.BillingCity = this.opportunity.Account.BillingCity;
        q.BillingCountry = this.opportunity.Account.BillingCountry;
        q.BillingPostalCode = this.opportunity.Account.BillingPostalCode;
        q.BillingStreet = this.opportunity.Account.BillingStreet;
        q.ShippingName = this.opportunity.Account.Name;
        q.Pricebook2Id = ddlPricebooks.SelectedValue;
        q.ContactId = this.ddlContacts.SelectedValue;
                
        string email = lsContacts.Where(c => c.Id == this.ddlContacts.SelectedValue).First().Email.ToString();
        q.Email = email;

        string result = new QuoteController().addQuote(q).ToString();
        this.createQuoteLineItems(result);

        Quote resultQuote = new QuoteController().getQuoteByID(result);
        this.addPDF(result, resultQuote);

        this.info.Visible = false;
        lblAccountResult.Text = "The quote has been saved succesfully.";
        Response.Redirect("https://eu2.salesforce.com/" + this.OpportunityId);
    }

    protected void createQuoteLineItems(string quoteId)
    {
        int rowIndex = 0;
        List<QuoteLineItem> lsQuoteLineItems = new List<QuoteLineItem>();
        if (ViewState["QuoteLineItemTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["QuoteLineItemTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlProduct = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddlProducts");

                    if (ddlProduct.SelectedValue != "0")
                    {
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtQuantity");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtUnitPrice");
                        QuoteLineItem qli = new QuoteLineItem();
                        qli.Quantity = double.Parse(box2.Text);
                        qli.PricebookEntryId = ddlProduct.SelectedValue;
                        qli.QuoteId = quoteId;
                        qli.UnitPrice = double.Parse(box3.Text);
                        qli.QuantitySpecified = true;
                        qli.UnitPriceSpecified = true;
                        lsQuoteLineItems.Add(qli);
                        rowIndex++;
                    }
                }
                //Call the method for executing inserts
                foreach (QuoteLineItem qlis in lsQuoteLineItems)
                {
                    new QuoteLineItemController().addQuoteLineItem(qlis);
                }
            }
        }
    }

    protected void addPDF(string result, Quote q)
    {
        Attachment a = new Attachment();
        a.ParentId = result;
        a.Name = q.QuoteNumber + ".pdf";
        /*System.IO.FileStream fs = System.IO.File.OpenRead(HttpRuntime.AppDomainAppPath + @"test.pdf");
        byte[] buffer = File.ReadAllBytes(HttpRuntime.AppDomainAppPath + @"test.pdf");

        fs.Read(buffer, 0, buffer.Length);
        a.Body = buffer;*/

        pdfcrowd.Client client = new pdfcrowd.Client("svenf", "61033983119d2b2b1956980ad74f1add");
        FileStream fs = new FileStream(HttpRuntime.AppDomainAppPath + a.Name, FileMode.CreateNew);
        client.convertURI("http://80.65.129.14/PdfPage.aspx?id=" + result, fs);
        BinaryReader br = new BinaryReader(fs);
        long numBytes = new FileInfo(HttpRuntime.AppDomainAppPath + a.Name).Length;
        byte[] buffer = br.ReadBytes((int)numBytes);
        fs.Close();
        a.Body = buffer;

        new AttachmentController().addQuote(a);              
    }
}