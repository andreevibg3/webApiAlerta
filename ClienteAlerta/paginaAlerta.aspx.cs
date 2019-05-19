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

namespace ClienteAlerta
{
    public partial class paginaAlerta : System.Web.UI.Page
    {
        List<AGENTE> myInstance = new List<AGENTE>();
        public List<AGENTE> GetProductAsync(string path)
        {
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
            myInstance=GetProductAsync("http://www.alerta.amazonebaycomprasecuador.com/api/Agente");
            GridView1.DataSource = myInstance;
            GridView1.DataBind();
        }
    }
}