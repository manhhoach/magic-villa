using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.VillaNumber.Dto
{
    public class VilllaNumberCreateDto
    {
        [Required]
        public int VillaNo { get; set; }


        public string SpecialDetails { get; set; }
    }
}
