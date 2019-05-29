using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data;
using System.ComponentModel;

namespace ClienteAlerta
{
    public partial class paginaAlerta : System.Web.UI.Page
    {
        string requerimiento;
        public List<AGENTE> GetProductAsync(string path)
        {
            List<AGENTE> myInstance = new List<AGENTE>();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                myInstance = JsonConvert.DeserializeObject<List<AGENTE>>(response.Content.ReadAsStringAsync().Result);
            }
            return myInstance;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            requerimiento = Request.QueryString["tipo"];
            if (string.IsNullOrEmpty(requerimiento))
                requerimiento = 1.ToString();
            List<AGENTE> myInstance = GetProductAsync("http://www.alerta.amazonebaycomprasecuador.com/api/Agente");
            if (requerimiento.Equals(1.ToString()))
                myInstance = myInstance.Where(x => x.estado == "Activo").ToList();
            else
                if (requerimiento.Equals(2.ToString()))
                     myInstance = myInstance.Where(x => x.estado == "Procesando").ToList();
            if (requerimiento.Equals(3.ToString()))
            {
                string url = "http://www.alerta.amazonebaycomprasecuador.com/api/Agente";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://www.alerta.amazonebaycomprasecuador.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = client.DeleteAsync(url).Result;
            }            
            //usurios.Items.Add("POLICIA");
            //usurios.Items.Add("ABOGADO");
            //usurios.Items.Add("TRABAJADOR SOCIAL");
            DataTable d = ConvertToDataTable(myInstance);
            //d.Columns.Remove("CODIGO");
            d.Columns.Remove("localizacion");
            d.Columns.Remove("fechaCierre"); 
            //d.Columns.Remove("UsuarioAsignado");

            //d.Columns.Add("hola", usurios);
            GridView1.DataSource = d;
            GridView1.DataBind();
            cargarAlertas(myInstance);
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public void cargarAlertas(List<AGENTE> lista)
        {
            Literal literal = new Literal();
            literal.Text += "<script>";
            for (int i = 0; i < lista.Count; i++)
            {
                List<string> coordenadas = lista[i].localizacion.Split('|').ToList();
                literal.Text += "var m" + i + " = L.marker([" + coordenadas[0] + "," + coordenadas[1] + "]).bindPopup('<b>Alerta</b><br />Ubicación  " + lista[i].usuario + "')";
                literal.Text += ".on('click', function(e){mymap.setView([" + coordenadas[0] + "," + coordenadas[1] + "], 18);}); ";
                literal.Text += "markerClusters.addLayer( m" + i + ");";

            }
            literal.Text += "mymap.addLayer( markerClusters );";
            literal.Text += "</script>";
            contenedorAgentes.Controls.Add(literal);
        }

        protected void GridView1_PageIndexChanging(Object  sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string url ="http://www.alerta.amazonebaycomprasecuador.com/api/Agente";
            if (e.CommandName == "editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                /*inicio cambio*/
                GridViewRow curruntRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                DropDownList ddlCopyStatus = (DropDownList)curruntRow.FindControl("DropDownList2") as DropDownList;//copystatus dropdownlist
                string selectedNewValue = ddlCopyStatus.SelectedValue;
                //here i want to get the selectedValue
                /*fin cambio*/

                DropDownList drplist = (DropDownList)GridView1.Rows[index].FindControl("DropDownList2");
                string usuario = GridView1.DataKeys[index]["CODIGO"].ToString();
                AGENTE a = new AGENTE();
                a.CODIGO = Convert.ToInt32(usuario);
                a.usuarioAsignado = "hola";
                a.estado = "Procesando";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://www.alerta.amazonebaycomprasecuador.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("api/Agente"));
                HttpResponseMessage response = client.PutAsJsonAsync(url, a).Result;
            }
        }
    }
}