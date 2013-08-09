using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    

    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        arr.Add(new ListItem("Item1", "1"));
        arr.Add(new ListItem("Item2", "2"));
        arr.Add(new ListItem("Item3", "3"));
        arr.Add(new ListItem("Item4", "4"));
        arr.Add(new ListItem("Item5", "5"));
        return arr;
    }

    private void FillDropDownList(DropDownList ddl)
    {
        ArrayList arr = GetDummyData();
        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        //Define the Columns
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));

        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        //Bind the DataTable to the Grid
        Gridview1.DataSource = dt;
        Gridview1.DataBind();

        //Extract and Fill the DropDownList with Data
        DropDownList ddl1 = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("DropDownList1");
        DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[2].FindControl("DropDownList2");
        DropDownList ddl3 = (DropDownList)Gridview1.Rows[0].Cells[3].FindControl("DropDownList3");

        FillDropDownList(ddl1);
        FillDropDownList(ddl2);
        FillDropDownList(ddl3);

    }
    private void AddNewRowToGrid()
    {

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //extract the DropDownList Selected Items
                    DropDownList ddl1 = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("DropDownList1");
                    DropDownList ddl2 = (DropDownList)Gridview1.Rows[i].Cells[2].FindControl("DropDownList2");
                    DropDownList ddl3 = (DropDownList)Gridview1.Rows[i].Cells[3].FindControl("DropDownList3");

                    // Update the DataRow with the DDL Selected Items
                    dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["Column2"] = ddl2.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["Column3"] = ddl3.SelectedItem.Text;

                }

                //Rebind the Grid with the current data
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
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("DropDownList1");
                    DropDownList ddl2 = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("DropDownList2");
                    DropDownList ddl3 = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("DropDownList3");

                    //Fill the DropDownList with Data
                    FillDropDownList(ddl1);
                    FillDropDownList(ddl2);
                    FillDropDownList(ddl3);

                    if (i < dt.Rows.Count - 1)
                    {
                        ddl1.ClearSelection();
                        ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;

                        ddl2.ClearSelection();
                        ddl2.Items.FindByText(dt.Rows[i]["Column2"].ToString()).Selected = true;

                        ddl3.ClearSelection();
                        ddl3.Items.FindByText(dt.Rows[i]["Column3"].ToString()).Selected = true;
                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetInitialRow();
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
}