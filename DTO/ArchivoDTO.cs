namespace AppApi.DTO
{
    public class ArchivoDTO
    {
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string UserName { get; set; }
        public IFormFile Foto { get; set; }
    }
}
