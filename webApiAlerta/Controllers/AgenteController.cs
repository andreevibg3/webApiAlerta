using AccesoDatos;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace webApiAlerta.Controllers
{
    [RoutePrefix("api/Agente")]
    public class AgenteController : ApiController
    {
        AgenteDatos a = new AgenteDatos();
        // GET api/values
        public List<AGENTE> Get()
        {
            return a.SeleccionarAgentes();
        }

        // GET api/values/5

        [HttpGet]
        public AGENTE Get(string usuario)
        {
            return a.SeleccionarAgentePorUsuario(usuario);
        }

        [Route("Asignado")]
        [HttpGet]
        public List<AGENTE> GetUsuarioAsignado(string usuario)
        {
            return a.SeleccionarAgentePorUsuarioAsignado(usuario);
        }

        // POST api/values
        [HttpPost]
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
        [HttpPut]
        public Respuesta Put([FromBody]AGENTE value)
        {
            Respuesta resp = new Respuesta();
            if (a.Actualizar(value))
                resp.valor = "si valio actualizar";
            else
            {
                resp.valor = "no valio actualizar";
            }
            return resp;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
