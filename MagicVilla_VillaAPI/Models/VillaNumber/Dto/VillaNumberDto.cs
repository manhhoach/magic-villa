﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.VillaNumber.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }


        public string SpecialDetails { get; set; }
    }
}