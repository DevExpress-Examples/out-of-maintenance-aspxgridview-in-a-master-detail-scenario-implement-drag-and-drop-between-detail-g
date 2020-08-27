<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Content/MyStyles.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.4/jquery-ui.min.js"></script>
    <script src="jquery.ui.touch-punch.min.js"></script>
    <script src="MyJavaScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="gvMaster" ClientInstanceName="gridMaster" DataSourceID="gvMasterDS"
            OnCustomCallback="gvMaster_CustomCallback" runat="server" KeyFieldName="CategoryID" AutoGenerateColumns="False">
            <SettingsDetail ShowDetailRow="true" />
            <Columns>
                <dx:GridViewDataColumn FieldName="CategoryID" Caption="ID">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="CategoryName" Caption="Category Name">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Description" Caption="Description">
                </dx:GridViewDataColumn>
            </Columns>
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="gvDetail" DataSourceID="gvDetailDS" KeyFieldName="ProductID" AutoGenerateColumns="False"
                        runat="server" OnHtmlRowPrepared="gvDetail_HtmlRowPrepared" OnInit="gvDetail_Init"
                        OnBeforePerformDataSelect="gvDetail_BeforePerformDataSelect">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="ProductID" Caption="ID">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="ProductName" Caption="Product Name">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="CategoryID" Caption="Category ID">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UnitPrice" Caption="Unit Price">
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Styles>
                            <Table CssClass="droppableGrid"></Table>
                            <Row CssClass="draggableRow"></Row>
                        </Styles>
                    </dx:ASPxGridView>
                </DetailRow>
            </Templates>
        </dx:ASPxGridView>
        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" EndCallback="OnControlsInitialized" />
        </dx:ASPxGlobalEvents>
        <asp:AccessDataSource ID="gvMasterDS" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]"></asp:AccessDataSource>
        <asp:AccessDataSource ID="gvDetailDS" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT [ProductID], [ProductName], [CategoryID], [UnitPrice] FROM [Products] WHERE ([CategoryID] = ?)">
            <SelectParameters>
                <asp:SessionParameter Name="CategoryID" SessionField="CategoryID" Type="Int32" />
            </SelectParameters>
        </asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
