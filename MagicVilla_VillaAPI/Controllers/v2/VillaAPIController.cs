using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.Villa.Dto;
using MagicVilla_VillaAPI.Repository.Villa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;
        protected APIResponse _response;

        public VillaAPIController(
            ILogging logger,
            IMapper mappingConfig,
            IVillaRepository villaRepository
        )
        {
            _logger = logger;
            _mapper = mappingConfig;
            _villaRepository = villaRepository;
            _response = new APIResponse();
        }


        [HttpGet]
        public ActionResult GetVillas()
        {
                return Ok();

        }

    }
}