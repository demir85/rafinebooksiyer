using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Eticaret12.Models
{
    public class Tur
    {
        [Key]
        public int? TurId { get; set; }
        public string TurAdi { get; set; }
        public string FotografYolu { get; set; }
    }
}
