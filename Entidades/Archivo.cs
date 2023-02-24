using System.ComponentModel.DataAnnotations;

namespace AppApi.Entidades
{
    public class Archivo
    {
        [Required]
        public int Id { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string UserName { get; set; }
        public Byte[] Foto { get; set; }
    }
}
