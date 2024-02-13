﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Web.Models.VillaNumber
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }

        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
