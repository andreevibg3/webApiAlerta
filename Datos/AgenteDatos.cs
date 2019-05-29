using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace Datos
{
    public class AgenteDatos
    {
        BaseAlertaEntities contexto;
        public AgenteDatos()
        {
            contexto = new BaseAlertaEntities();
            contexto.Configuration.ProxyCreationEnabled = false;
        }

        public AGENTE SeleccionarAgentePorUsuario(string usuario)
        {
            AGENTE respuesta = contexto.AGENTE.Where(x => x.usuario == usuario).FirstOrDefault();
            return respuesta;
        }

        public List<AGENTE> SeleccionarAgentePorUsuarioAsignado(string usuario)
        {
            List<AGENTE> respuesta = contexto.AGENTE.Where(x => x.usuarioAsignado == usuario).ToList();
            return respuesta;
        }

        public List<AGENTE> SeleccionarAgentes()
        {
            List<AGENTE> respuesta = contexto.AGENTE.ToList();
            return respuesta;
        }

        public bool Insertar(AGENTE insertado)
        {
            contexto.AGENTE.Add(insertado);
            contexto.SaveChanges();
            return true;
        }

        public bool Actualizar(AGENTE actualizado)
        {
            AGENTE respuesta = contexto.AGENTE.Where(x => x.CODIGO == actualizado.CODIGO).Single();
            respuesta.usuarioAsignado = actualizado.usuarioAsignado;
            contexto.SaveChanges();
            return true;
        }
    }
}
