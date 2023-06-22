using Api_Marcador.Bussines.Marcadores;
using Api_Marcador.Resultado.Marcadores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api_Marcador.Controllers
{
    [ApiController]

    public class MarcadoresController:ControllerBase
    {
        private readonly IMediator _mediator;
        public MarcadoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/Marcadores")]
        public async Task<ListaMarcadores> ObtenerMarcadores()
        {
            var resultado=new ListaMarcadores();
            var marcadores= await _mediator.Send(new GetMarcadores_Bussines.GetMarcadoresComando());
            if (marcadores != null)
            {
                foreach(var item in marcadores)
                {
                    var itemMarcador = new ItemMarcador();
                    itemMarcador.Latitud = item.Latitud;
                    itemMarcador.Longitud = item.Longitud;
                    itemMarcador.Info = item.Info;

                    resultado.ListMarcadores.Add(itemMarcador);                    
                }
                resultado.StatusCode=System.Net.HttpStatusCode.OK;
                resultado.Ok = true;
                return resultado;
            }
            else
            {
                resultado.StatusCode= System.Net.HttpStatusCode.NotFound;
                resultado.MensajeError = "Marcadores no encontrados";
                resultado.Ok = false;

                return resultado;
            }
            
        }
    }

}
