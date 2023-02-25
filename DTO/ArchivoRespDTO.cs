namespace AppApi.DTO
{
    public class ArchivoRespDTO
    {
        public int Id { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string UserName { get; set; }
        public IFormFile Foto { get; set; }
    }
}
