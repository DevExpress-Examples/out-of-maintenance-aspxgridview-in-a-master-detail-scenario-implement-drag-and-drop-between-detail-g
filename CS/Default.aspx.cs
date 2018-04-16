using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void gvDetail_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        Session["CategoryID"] = grid.GetMasterRowKeyValue();
        grid.ClientInstanceName = "detailGrid" + grid.GetMasterRowKeyValue();
    }

    protected void gvDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            e.Row.Attributes.Add("categoryID", grid.GetMasterRowKeyValue().ToString());
            e.Row.Attributes.Add("productID", e.KeyValue.ToString());
        }
    }

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        string productID = e.Parameters.Split('|')[0];
        string newCategoryID = e.Parameters.Split('|')[1];
        gvDetailDS.UpdateCommand = "UPDATE Products SET CategoryID = " + newCategoryID + " WHERE ProductID = " + productID + "";
        gvDetailDS.Update();
        grid.DataBind();
    }

    protected void gvDetail_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        grid.ClientSideEvents.Init = @"
        function (s, e) {
            var table = s.GetMainTable();  
            var att = document.createAttribute('categoryID');      
            att.value = " + grid.GetMasterRowKeyValue().ToString() + @";                           
            table.setAttributeNode(att);
        }";
    }
}