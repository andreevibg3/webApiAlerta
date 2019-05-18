using AccesoDatos;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApiAlerta.Controllers
{
    public class AgenteController : ApiController
    {
        AgenteDatos a = new AgenteDatos();
        // GET api/values
        public List<AGENTE> Get()
        {
            return a.SeleccionarAgentes();
        }

        // GET api/values/5
        public AGENTE Get(string usuario)
        {
            return a.SeleccionarAgentePorUsuario(usuario);
        }

        // POST api/values
        public Respuesta Post([FromBody]AGENTE value)
        {
            Respuesta resp = new Respuesta();
            if (a.Insertar(value))
                 resp.valor="si valio";
            else
            {
                resp.valor = "no valio";
            }
            return resp;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
