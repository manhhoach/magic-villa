using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MagicVilla_VillaAPI.Models.Villa;

namespace MagicVilla_VillaAPI.Models.VillaNumber
{
    public class VillaNumberModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }

        [ForeignKey("Villa_VillaNumber")]
        public int VillaId { get; set; }

        public VillaModel Villa { get; set; }  


        public string SpecialDetails { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
