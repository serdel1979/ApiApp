using AppApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AppApi.Controllers
{
    [ApiController]
    [Route("pruebas")]
    public class PruebaController:ControllerBase
    {

        class Respuesta
        {
            public string mensaje { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult> Recibir([FromForm] ArchivoDTO archivoDto)
        {
            var msj = $"archivo de {archivoDto.UserName} recibido";
            Console.WriteLine(msj);
            var resp = new Respuesta { mensaje = msj };
            return Ok(resp);
        }







        [HttpGet]
        public async Task<ActionResult> Prueba()
        {

            Console.WriteLine("lógica del get");

            return Ok("Ok");
        }




    }
}
