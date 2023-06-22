using Api_Marcador.Models;
using Api_Marcador.Resultado.Marcadores;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Api_Marcador.Bussines.Marcadores
{
    public class GetMarcadores_Bussines
    {
        public class GetMarcadoresComando : IRequest<List<ItemMarcador>>
        {            

        }

        public class EjecutaValidacion : AbstractValidator<GetMarcadoresComando>
        {
            public EjecutaValidacion()
            {                
            }

        }

        public class ManejadorBussines:IRequestHandler<GetMarcadoresComando, List<ItemMarcador>>
        {
            private readonly IValidator<GetMarcadoresComando> _validator;
            public ManejadorBussines(IValidator<GetMarcadoresComando> validator)
            {
                _validator = validator;
            }
            
            public async Task<List<ItemMarcador>> Handle(GetMarcadoresComando request,CancellationToken cancellationToken)
            {
                var validacion=await _validator.ValidateAsync(request);

                if (!validacion.IsValid)
                {

                }

                using(HttpClient cliente=new HttpClient())
                {
                    string usuario = "admin";
                    string clave = "123";

                    var data = new
                    {
                        nombreUsuario = usuario,
                        password = clave
                    };

                    string jsonContent = JsonConvert.SerializeObject(data);
                    var content=new StringContent(jsonContent,Encoding.UTF8,"application/json");

                    HttpResponseMessage response = await cliente.PostAsync("https://prog3.nhorenstein.com/api/usuario/LoginUsuarioWeb",content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse=await response.Content.ReadAsStringAsync();
                        var datos = JsonConvert.DeserializeObject<UsuarioToken>(jsonResponse);
                        string idUsuario = datos.idUsuario;
                        string token = datos.token;

                        return await ObtenerMarcadores(token);
                    }

                }
                return null;

            }
            public async Task<List<ItemMarcador>> ObtenerMarcadores(string token)
            {
                using (HttpClient client=new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response =await client.GetAsync("https://prog3.nhorenstein.com/api/marcador/GetMarcadores");
                    var listaCargadaMarcadores=new List<ItemMarcador>();
                    if (response.IsSuccessStatusCode)
                    {
                       string jsonResponse=await response.Content.ReadAsStringAsync();
                        var respuesta =JsonConvert.DeserializeObject<RespuestaMarcadores>(jsonResponse);

                        if (respuesta != null && respuesta.Ok)
                        {
                            listaCargadaMarcadores.AddRange(respuesta.LitadoMarcadores);
                            return listaCargadaMarcadores;
                        }

                    }
                }
                return null;
            }


        }

    }

}
