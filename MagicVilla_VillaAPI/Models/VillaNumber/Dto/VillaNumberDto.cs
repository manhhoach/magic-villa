using MagicVilla_VillaAPI.Models.Villa.Dto;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.VillaNumber.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }

        public int VillaId { get; set; }

        public string SpecialDetails { get; set; }

        public VillaDTO Villa { get; set; }
    }
}
