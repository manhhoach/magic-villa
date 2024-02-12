using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.VillaNumber.Dto
{
    public class VillaNumberUpdateDto
    {

        [Required]
        public int VillaNo { get; set; }

        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
