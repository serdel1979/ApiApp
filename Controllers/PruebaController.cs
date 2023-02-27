using AppApi.DTO;
using AppApi.Entidades;
using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AppApi.Controllers
{
    [ApiController]
    [Route("pruebas")]
    public class PruebaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly HttpClient _client;
        public PruebaController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration) {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
            this._client = new HttpClient();
        }

        class Respuesta
        {
            public string mensaje { get; set; }
        }


        public byte[] ConvertirArchivo(IFormFile archivo)
        {
            using (var ms = new MemoryStream())
            {
                archivo.CopyTo(ms);
                return ms.ToArray();
            }
        }


        private byte[] convertirFoto(IFormFile file)
        {
            byte[] fileByteArray = new byte[0];
            if (file != null)
            {
                using (var item = new MemoryStream())
                {
                    file.CopyTo(item);
                    fileByteArray = item.ToArray();
                }
            }

            return fileByteArray;
        }

        [HttpPost]
        public async Task<ActionResult> Recibir([FromForm] ArchivoDTO archivoDto)
        {
            try
            {
                var entidad = new Archivo
                {
                    Latitud = archivoDto.Latitud,
                    Longitud = archivoDto.Longitud,
                    UserName = archivoDto.UserName,
                    Foto = convertirFoto(archivoDto.Foto)
                };

                //var archivoFoto = ConvertirArchivo(archivoDto.Foto);
                //var entidadFoto = mapper.Map<Archivo>(archivoDto);
                context.Add(entidad);
                await context.SaveChangesAsync();

                var msj = $"archivo de {archivoDto.UserName} recibido";
                Console.WriteLine(msj);
                var resp = new Respuesta { mensaje = msj };
                return Ok(resp);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }







        [HttpGet("clima/lat/{lat}/longitud/{longitud}")]
        public async Task<ActionResult> clime(string lat, string longitud)
        {
            var key = Environment.GetEnvironmentVariable("ApiKey");
            var response = await _client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={longitud}&appid={key}&lang=es");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"latitud {lat} y {longitud} recibida");

            return Ok(content);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Archivo>> Get(int id)
        {

            var entidadArchivo = await context.Archivos.Where(x => x.Id == id).FirstAsync();

            if (entidadArchivo == null)
            {
                return NotFound();
            }
            return Ok(entidadArchivo);
        }

        [HttpGet("pdf/{id:int}")]
        public async Task<ActionResult<Archivo>> GetPdf(int id)
        {

            var entidadArchivo = await context.Archivos.Where(x => x.Id == id).FirstAsync();
            if (entidadArchivo == null || entidadArchivo.Foto.Length == 0)
            {
                return BadRequest("Error en archivo");
            }

            string imgB64 = System.Text.Encoding.UTF8.GetString(entidadArchivo.Foto);
            var memoryStream = new MemoryStream();

            MemoryStream ms = new MemoryStream();
            Document doc = new Document(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();
            Image image = Image.GetInstance(Convert.FromBase64String(imgB64));
            doc.Add(image);
            doc.Close();
            var stream = new MemoryStream(ms.ToArray());
            ms.Close();
            return File(stream, "application/pdf", "MiPDF.pdf");

        }






    }
}
