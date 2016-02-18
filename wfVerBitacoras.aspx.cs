using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

namespace AplicacionesEnatrel.Bitacoras
{
    public partial class wfVerBitacoras : System.Web.UI.Page
    {
        string aplicacion = Properties.Settings.Default.aplicacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarModulo();
            }
        }
        
        private void BindDropDownList(ASPxComboBox ddl, string query, string text, string value, string defaultText)
        {
            u_static.setConx(aplicacion);
            
            //string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(u_static.getConString()))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    con.Open();
                    ddl.DataSource = cmd.ExecuteReader();
                    ddl.TextField = text;
                    ddl.ValueField = value;
                    ddl.DataBind();
                    con.Close();
                }
            }

            ddl.Items.Insert(0, new ListEditItem(defaultText, "0"));
            ddl.SelectedIndex = 0;
        }

        private void llenarModulo()
        {
            string query =
                    "SELECT DISTINCT " +
                    "ab.modulo AS nombre, " +
                    "ab.modulo AS valor " +
                    "FROM Bitacora.AdmonBitacoras ab " +
                    "WHERE ab.estado = 1 ";

            BindDropDownList(this.cbModulo, query, "nombre", "valor", "Seleccionar Módulo");
            this.cbBD.Enabled = false;
            this.cbEsquema.Enabled = false;
            this.cbTabla.Enabled = false;
            this.cbFiltro.Enabled = false;
            //btVer.Enabled = false;
            this.tbFechaIni.Enabled = false;
            this.tbFechaFin.Enabled = false;

            cbBD.Items.Insert(0, new ListEditItem("Seleccionar Base de Datos", "0"));
            cbEsquema.Items.Insert(0, new ListEditItem("Seleccionar Esquema", "0"));
            cbTabla.Items.Insert(0, new ListEditItem("Seleccionar Tabla", "0"));
            cbFiltro.Items.Insert(0, new ListEditItem("Seleccionar Filtro", "0"));

            Session["col"] = "col";
        }

        protected void Modulo_Changed(object sender, EventArgs e)
        {
            this.cbBD.Enabled = false;
            this.cbEsquema.Enabled = false;
            this.cbTabla.Enabled = false;
            this.cbFiltro.Enabled = false;
            this.tbFechaIni.Enabled = false;
            this.tbFechaFin.Enabled = false;
            //btVer.Enabled = false;

            cbBD.Items.Clear();
            cbEsquema.Items.Clear();
            cbTabla.Items.Clear();
            cbFiltro.Items.Clear();
            tbFechaFin.Text = string.Empty;
            tbFechaIni.Text = string.Empty;

            cbBD.Items.Insert(0, new ListEditItem("Seleccionar Base de Datos", "0"));
            cbEsquema.Items.Insert(0, new ListEditItem("Seleccionar Esquema", "0"));
            cbTabla.Items.Insert(0, new ListEditItem("Seleccionar Tabla", "0"));
            cbFiltro.Items.Insert(0, new ListEditItem("Seleccionar Filtro", "0"));

            string ModuloId = cbModulo.SelectedItem.Value.ToString();

            if (ModuloId != "")
            {
                string query = 
                                string.Format(
                                " SELECT DISTINCT "+
                                " ab.bd AS nombre, "+
                                " ab.bd AS valor "+
                                " FROM Bitacora.AdmonBitacoras ab "+
                                " WHERE ab.estado = 1 "+
                                " AND ab.modulo = '{0}'", ModuloId);


                BindDropDownList(this.cbBD, query, "nombre", "valor", "Seleccionar Base de Datos");
                this.cbBD.Enabled = true;
            }
        }

        protected void bd_Changed(object sender, EventArgs e)
        {            
            this.cbEsquema.Enabled = false;
            this.cbTabla.Enabled = false;
            this.cbFiltro.Enabled = false;
            this.tbFechaIni.Enabled = false;
            this.tbFechaFin.Enabled = false;
            //btVer.Enabled = false;

            cbEsquema.Items.Clear();
            cbTabla.Items.Clear();
            cbFiltro.Items.Clear();

            cbEsquema.Items.Insert(0, new ListEditItem("Seleccionar Esquema", "0"));
            cbTabla.Items.Insert(0, new ListEditItem("Seleccionar Tabla", "0"));
            cbFiltro.Items.Insert(0, new ListEditItem("Seleccionar Filtro", "0"));

            string ModuloId = cbModulo.SelectedItem.Value.ToString();
            string BDId = cbBD.SelectedItem.Value.ToString();

            if (BDId != "" && ModuloId != "" )
            {
                object[] val = { ModuloId, BDId };
                string query =
                                string.Format(
                                " SELECT DISTINCT " +
                                " ab.esquema AS nombre, " +
                                " ab.esquema AS valor " +
                                " FROM Bitacora.AdmonBitacoras ab " +
                                " WHERE ab.estado = 1 " +
                                " AND ab.modulo = '{0}' "+
                                " AND ab.bd = '{1}'", val );


                BindDropDownList(this.cbEsquema, query, "nombre", "valor", "Seleccionar Esquema");
                this.cbEsquema.Enabled = true;
            }
        }

        protected void Esquema_Changed(object sender, EventArgs e)
        {
            this.cbTabla.Enabled = false;
            this.cbFiltro.Enabled = false;
            this.tbFechaIni.Enabled = false;
            this.tbFechaFin.Enabled = false;
            //btVer.Enabled = false;

            cbTabla.Items.Clear();
            cbFiltro.Items.Clear();

            cbTabla.Items.Insert(0, new ListEditItem("Seleccionar Tabla", "0"));
            cbFiltro.Items.Insert(0, new ListEditItem("Seleccionar Filtro", "0"));

            string ModuloId = cbModulo.SelectedItem.Value.ToString();
            string BDId = cbBD.SelectedItem.Value.ToString();
            string EsquemaId = cbEsquema.SelectedItem.Value.ToString();


            if (BDId != "" && ModuloId != "" && EsquemaId != "")
            {
                object[] val = { ModuloId, BDId, EsquemaId };
                string query =
                                string.Format(
                                " SELECT DISTINCT " +
                                " ab.tabla AS nombre, " +
                                " ab.tabla AS valor " +
                                " FROM Bitacora.AdmonBitacoras ab " +
                                " WHERE ab.estado = 1 " +
                                " AND ab.modulo = '{0}' " +
                                " AND ab.bd = '{1}' " + 
                                " AND ab.esquema = '{2}'", val);


                BindDropDownList(this.cbTabla, query, "nombre", "valor", "Seleccionar Tabla");
                this.cbTabla.Enabled = true;
            }
        }

        protected void Tabla_Changed(object sender, EventArgs e)
        {
            this.cbFiltro.Enabled = false;
            this.tbFechaIni.Enabled = false;
            this.tbFechaFin.Enabled = false;

            //btVer.Enabled = false;

            cbFiltro.Items.Clear();

            cbFiltro.Items.Insert(0, new ListEditItem("Seleccionar Filtro", "0"));

            string ModuloId = cbModulo.SelectedItem.Value.ToString();
            string BDId = cbBD.SelectedItem.Value.ToString();
            string EsquemaId = cbEsquema.SelectedItem.Value.ToString();
            string TablaId = cbTabla.SelectedItem.Value.ToString();

            if (BDId != "" && ModuloId != "" && EsquemaId != "" && TablaId != "")
            {
                object[] val = { ModuloId, BDId, EsquemaId, TablaId };
                string query =
                                string.Format(
                                " SELECT * "+
                                " FROM "+
                                " ( "+
                                    " SELECT "+
                                    " 'Fecha Doc.' as nombre, " +
                                    " ab.fechaDoc as valor "+
                                    " FROM Bitacora.AdmonBitacoras ab "+
                                    " WHERE ab.estado = 1 "+
                                    " AND ab.modulo = '{0}' "+
                                    " AND ab.bd = '{1}' "+
                                    " AND ab.esquema = '{2}' "+
                                    " AND ab.tabla = '{3}' "+
                                    " UNION "+
                                    " SELECT "+
                                    " 'Fecha Bit.' as nombre, " +
                                    " ab.fechaBitacora as valor "+
                                    " FROM Bitacora.AdmonBitacoras ab "+
                                    " WHERE ab.estado = 1 "+
                                    " AND ab.modulo = '{0}' "+
                                    " AND ab.bd = '{1}' "+
                                    " AND ab.esquema = '{2}' "+
                                    " AND ab.tabla = '{3}' "+
                                " ) TABLA "+
                                " WHERE TABLA.valor IS NOT NULL "                                
                                , val);


                BindDropDownList(this.cbFiltro, query, "nombre", "valor", "Seleccionar Filtro");
                this.cbFiltro.Enabled = true;
            }
        }

        protected void CbFiltro_Changed(object sender, EventArgs e)
        {

            string ModuloId = cbModulo.SelectedItem.Value.ToString();
            string BDId = cbBD.SelectedItem.Value.ToString();
            string EsquemaId = cbEsquema.SelectedItem.Value.ToString();
            string TablaId = cbTabla.SelectedItem.Value.ToString();
            string Filtro = cbFiltro.SelectedItem.Value.ToString();
            string campo = null;
            string cbit = null;

            if (cbFiltro.SelectedItem.Text.Contains("Fecha Doc."))
            { 
                campo = "fechaDoc";
                cbit = "fechaDocxDia";
            }
            else
            { 
                campo = "fechaBitacora";
                cbit = "fechaBitxDia";
            }

            if (BDId != "" && ModuloId != "" && EsquemaId != "" && TablaId != "" && Filtro != "")
            {
                object[] val = {cbit, ModuloId, BDId, EsquemaId, TablaId, campo, Filtro };
                string query =
                                string.Format(
                                    " SELECT "+
                                    " ab.{0} " +
                                    " FROM Bitacora.AdmonBitacoras ab "+
                                    " WHERE ab.estado = 1 "+
                                    " AND ab.modulo = '{1}' "+
                                    " AND ab.bd = '{2}' "+
                                    " AND ab.esquema = '{3}' "+
                                    " AND ab.tabla = '{4}' "+
                                    " AND ab.{5} = '{6}' "
                                , val);

                u_static.setConx(aplicacion);
                DataSet ds = new DataSet();

                SqlConnection con = new SqlConnection(u_static.getConString());
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Tabla");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.tbFechaIni.Enabled = true;
                    if (bool.Parse(ds.Tables[0].Rows[0][0].ToString()) == true)
                        tbFechaFin.Enabled = false;
                    else
                        tbFechaFin.Enabled = true;
                }

                //btVer.Enabled = true;
            }
        }
               
        private void verificarTablaDetalle()
        {
            string BDId = cbBD.SelectedItem.Value.ToString();
            string EsquemaId = cbEsquema.SelectedItem.Value.ToString();
            string TablaId = cbTabla.SelectedItem.Value.ToString();
            string modulo = cbModulo.SelectedItem.Value.ToString();
            string query =

            " SELECT * " +
            " FROM " +
            " ( " +
            " SELECT  " +
            " ab.tablaDetalle, " +
            "  ab.cPrincipal, " +
            "  ab.cDetalle " +
            " FROM " +
            "   Bitacora.AdmonBitacoras ab " +
            " WHERE " +
            "   ab.modulo = '" + modulo + "' AND  " +
            "   ab.bd = '"+BDId+"' AND  " +
            "    ab.esquema = '" + EsquemaId + "' AND  " +
            "    ab.tabla = '" + TablaId + "' " +
            "  ) t " +
            "  where t.tablaDetalle IS NOT NULL ";

            u_static.setConx(aplicacion);
                DataSet ds = new DataSet();

                SqlConnection con = new SqlConnection(u_static.getConString());
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(ds, "Tabla");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBit.KeyFieldName = ds.Tables[0].Rows[0]["cPrincipal"].ToString();
                    gvBit.SettingsDetail.ShowDetailRow = true;
                    hfTabla.Value = BDId + "." + EsquemaId + "." + ds.Tables[0].Rows[0]["tablaDetalle"].ToString();
                    hfCampo.Value = ds.Tables[0].Rows[0]["cDetalle"].ToString();
                }
                else
                {
                    gvBit.KeyFieldName = string.Empty;
                    gvBit.SettingsDetail.ShowDetailRow = false;
                    hfTabla.Value = "";
                    hfCampo.Value = "";
                }

        }

        private DataSet GetDetalle(string tabla, string campo, string id)
        {
            DataSet ds = new DataSet();
            String query = "SELECT * FROM " + tabla + " WHERE " + campo + "=" + id;

            u_static.setConx(aplicacion);
            SqlConnection cn = new SqlConnection(u_static.getConString());

            SqlDataAdapter da = new SqlDataAdapter(query, cn);
            da.Fill(ds, "Detalle");

            return ds;
        }

        private bool verificarControles()
        {
            bool control = false;
            int cont = 0;

            if (cbBD.SelectedIndex < 1)
                cont += 1;

            if (cbEsquema.SelectedIndex < 1)
                cont += 1;

            if (cbTabla.SelectedIndex < 1)
                cont += 1;

            if (cbFiltro.SelectedIndex < 1)
                cont += 1;

            DateTime d1 = DateTime.Today;
            DateTime d2 = DateTime.Today;

            if (tbFechaIni.Enabled == false)
                cont += 1;

            if (!DateTime.TryParse(tbFechaIni.Text.Trim(), out d1))
            {
                cont += 1;
            }

            if (tbFechaFin.Enabled == true)
            {
                if (!DateTime.TryParse(tbFechaFin.Text.Trim(), out d2))
                {
                    cont += 1;
                }
            }

            if (cont == 0)
                control = true;

            return control;
        }

        private DataTable getSource()
        {
            DataTable dt = new DataTable();

            string BDId = cbBD.SelectedItem.Value.ToString();
            string EsquemaId = cbEsquema.SelectedItem.Value.ToString();
            string TablaId = cbTabla.SelectedItem.Value.ToString();
            string Filtro = cbFiltro.SelectedItem.Value.ToString();
            string fechaIni = tbFechaIni.Text;
            string fechaFin = tbFechaFin.Text;

            object[] val = { BDId, EsquemaId, TablaId, Filtro, fechaIni, fechaFin };
            string query = string.Empty;

            if (tbFechaFin.Enabled == true)
                query = string.Format(
                        " SELECT * " +
                        " FROM {0}.{1}.{2} AS x " +
                        " WHERE x.{3} BETWEEN '{4}' AND '{5}' "
                    , val);
            else
                query = string.Format(
                        " SELECT * " +
                        " FROM {0}.{1}.{2} AS x " +
                        " WHERE x.{3} = '{4}' "
                    , val);

            u_static.setConx(aplicacion);
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(u_static.getConString());
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dt);

            verificarTablaDetalle();

            return dt;

        }
        
        protected void btVer_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (verificarControles())
                {
                    gvBit.Columns.Clear();
                    gvBit.DataBind();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Seleccione las opciones y fechas requeridas');", true);
            }
            catch { }
            
        }

        protected void btRegresar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Management/opciones.aspx");
        }

        protected void btExportar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.gvExporter.WriteXlsToResponse();
            }
            catch { }
        }
       
        protected void gvDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailGridView = (ASPxGridView)sender;

                detailGridView.DataSource = GetDetalle(hfTabla.Value, hfCampo.Value, detailGridView.GetMasterRowKeyValue().ToString());
                hfIdMaster.Value = detailGridView.GetMasterRowKeyValue().ToString();
            }
            catch { }
        }
        
        protected void gvBit_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                if (verificarControles())
                {
                    (sender as ASPxGridView).DataSource = getSource();
                    //verificarTablaDetalle();
                }
            }
            catch { }
        }

        protected void gvBit_Load(object sender, EventArgs e)
        {
            try
            {
                if (verificarControles())
                {
                    (sender as ASPxGridView).AutoGenerateColumns = true;
                    (sender as ASPxGridView).DataBind();
                }
            }
            catch { }
        }
        
        bool bnd = false;

        protected void gvBit_PageIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (IsPostBack && bnd != true)
            //    {
            //           // gvBit.Columns.Clear();
            //            gvBit.DataBind();
            //            bnd = true;
            //    }
            //}
            //catch
            //{

            //}

            //flag = true;
            //gvBit.DataBind();
            //flag = false;

        }

        
    }
}