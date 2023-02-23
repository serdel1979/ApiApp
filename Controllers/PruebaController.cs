using AppApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AppApi.Controllers
{
    [ApiController]
    [Route("pruebas")]
    public class PruebaController:ControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> Recibir([FromBody] ArchivoDTO archivoDto)
        {

            Console.WriteLine("procesa imagen");
            Console.WriteLine(archivoDto.Foto);

            return Ok("Image saved successfully");
        }


        [HttpGet]
        public async Task<ActionResult> Prueba()
        {

            Console.WriteLine("lógica del get");

            return Ok("Ok");
        }




    }
}
