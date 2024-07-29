//using Eticaret12.Migrations;
using System.ComponentModel.DataAnnotations;

namespace Eticaret12.Models
{
   
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        
        public string? KitabınAdı { get; set; }
        [Required(ErrorMessage = "zorunlu alan")]
      [StringLength(100)]
        public string? Baslik { get; set; }
       
        [Range(0, 300)]
        public decimal Fiyat { get; set; }
        public bool StokdaVarmı { get; set; }
      //  public int TurId { get; set; }
       // public virtual Tür Tür { get; set; }
       public string? KitapArtUrl { get; set; }

        public int TurId { get; set; }
        public virtual Tur Tur { get; set; }

    }
}
