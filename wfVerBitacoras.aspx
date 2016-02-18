<%@ Page Title="" Language="C#" MasterPageFile="~/projects.Master" AutoEventWireup="true" CodeBehind="wfVerBitacoras.aspx.cs" Inherits="AplicacionesEnatrel.Bitacoras.wfVerBitacoras" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 174px;
        }
        .style7
        {
            width: 93px;
        }
        .style8
        {
            width: 181px;
        }
        .style9
        {
            width: 56px;
        }
        .style10
        {
            width: 172px;
        }
        .style11
        {
            width: 35px;
        }
        .style12
        {
        }
        .style13
        {
        }
        .style14
        {
            width: 131px;
        }
        .style15
        {
            width: 61px;
        }
        .style16
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    
<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <table  align="center" style="border: thin solid #000080">
        <tr>
            <td class="style12" colspan="8">
                Seleccionar:</td>
        </tr>
        <tr>
            <td class="style12">
                Módulo:</td>
            <td class="style6">
                <dx:ASPxComboBox ID="cbModulo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Modulo_Changed">
                </dx:ASPxComboBox>                
            </td>
            <td class="style7">
                Base de Datos:</td>
            <td class="style8">
                <dx:ASPxComboBox ID="cbBD" runat="server" AutoPostBack="true" OnSelectedIndexChanged="bd_Changed">
                </dx:ASPxComboBox>
            </td>
            <td class="style9">
                Esquema:</td>
            <td class="style10">
                <dx:ASPxComboBox ID="cbEsquema" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Esquema_Changed">
                </dx:ASPxComboBox>
            </td>
            <td class="style11">
                Tabla:</td>
            <td>
                <dx:ASPxComboBox ID="cbTabla" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Tabla_Changed">
                </dx:ASPxComboBox>
            </td>            
        </tr>
    </table>
    <br />
    <table align="center" style="border: thin solid #008080">
        <tr>
            <td class="style13" colspan="7">
                Cargar información:</td>
        </tr>
        <tr>
            <td class="style13">
                Filtrar por:</td>
            <td class="style13">
                <dx:ASPxComboBox ID="cbFiltro" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CbFiltro_Changed">                
                </dx:ASPxComboBox>
            </td>
            <td class="style13">
                Fecha Inicio:</td>
            <td class="style14">
                <asp:TextBox ID="tbFechaIni" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="tbFechaIni_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbFechaIni">
                </asp:CalendarExtender>
            </td>
            <td class="style15">
                Fecha Fin:</td>
            <td>
                <asp:TextBox ID="tbFechaFin" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="tbFechaFin_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbFechaFin">
                </asp:CalendarExtender>
            </td>            
        </tr>
        </table>

         </ContentTemplate>
</asp:UpdatePanel>
<br />
    <table align="center" style="border: thin solid #008080">
        <tr>
            <td class="style16">
                <asp:ImageButton ID="btRegresar" runat="server" Height="32px" 
                    ImageUrl="~/Imagenes/Controles/back.png" ToolTip="Regresar" Width="32px" 
                    onclick="btRegresar_Click" />
            </td>
            <td class="style16">
                <asp:ImageButton ID="btVer" runat="server" Height="32px" 
                    ImageUrl="~/Imagenes/Controles/aceptar.png" onclick="btVer_Click" 
                    Width="32px" />
            </td>
            <td class="style16">
                <asp:ImageButton ID="btExportar" runat="server" Height="32px" 
                    ImageUrl="~/Imagenes/application-ms-excel.png" ToolTip="Exportar" 
                    Width="32px" onclick="btExportar_Click" />
            </td>
        </tr>
    </table>
    

    <br />
    <dx:ASPxGridViewExporter ID="gvExporter" runat="server" FileName="Bitacora" 
        GridViewID="gvBit">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="gvBit" runat="server" align="center"
        onbeforeperformdataselect="gvBit_BeforePerformDataSelect" 
        onload="gvBit_Load" 
        CssFilePath="~/App_Themes/Office2010Black/{0}/styles.css" 
        CssPostfix="Office2010Black" onpageindexchanged="gvBit_PageIndexChanged">
        <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        <Images SpriteCssFilePath="~/App_Themes/Office2010Black/{0}/sprite.css">
            <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Black/GridView/Loading.gif">
            </LoadingPanelOnStatusBar>
            <LoadingPanel Url="~/App_Themes/Office2010Black/GridView/Loading.gif">
            </LoadingPanel>
        </Images>
        <ImagesFilterControl>
            <LoadingPanel Url="~/App_Themes/Office2010Black/GridView/Loading.gif">
            </LoadingPanel>
        </ImagesFilterControl>
        <Styles CssFilePath="~/App_Themes/Office2010Black/{0}/styles.css" 
            CssPostfix="Office2010Black">
            <Header ImageSpacing="5px" SortingImageSpacing="5px">
            </Header>
            <LoadingPanel ImageSpacing="5px">
            </LoadingPanel>
        </Styles>
        <StylesPager>
            <CurrentPageNumber ForeColor="Black">
            </CurrentPageNumber>
            <PageNumber ForeColor="White">
            </PageNumber>
            <Summary ForeColor="White">
            </Summary>
            <Ellipsis ForeColor="White">
            </Ellipsis>
        </StylesPager>
        <StylesEditors ButtonEditCellSpacing="0">
            <ProgressBar Height="21px">
            </ProgressBar>
        </StylesEditors>
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="gvDetalle" runat="server" 
                    onbeforeperformdataselect="gvDetalle_BeforePerformDataSelect">
                    <SettingsDetail IsDetailGrid="True" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
    </dx:ASPxGridView>
    <br />
<br />
    <asp:HiddenField ID="hfTabla" runat="server" />
    <asp:HiddenField ID="hfCampo" runat="server" />
    <asp:HiddenField ID="hfIdMaster" runat="server" />
<br />
    
</asp:Content>
