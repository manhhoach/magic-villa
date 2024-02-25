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

namespace MagicVilla_VillaAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
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
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<VillaModel> villaList = await _villaRepository.GetAll();
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;

        }


        [HttpGet("{id}", Name = "GetVilla")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.Log("get villa error with id " + id, "error");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var data = await _villaRepository.GetOne(e => e.Id == id);
                if (data != null)
                {
                    _response.Result = _mapper.Map<VillaDTO>(data);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest();
                }

                VillaModel model = _mapper.Map<VillaModel>(createDTO);
                await _villaRepository.Create(model);
                _response.Result = _mapper.Map<VillaDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpDelete("{id}", Name = "DeleteVilla")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                var villa = await _villaRepository.GetOne(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _villaRepository.Remove(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;

        }

        [HttpPut("{id}", Name = "UpdateVilla")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.Id != id)
                    return BadRequest();
                VillaModel model = _mapper.Map<VillaModel>(updateDTO);
                await _villaRepository.Update(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;

        }

        [HttpPatch("{id}", Name = "UpdatePartialVilla")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
                return BadRequest();
            var villa = await _villaRepository.GetOne(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaUpdateDTO updateDTO = _mapper.Map<VillaUpdateDTO>(villa);

            patchDTO.ApplyTo(updateDTO, ModelState); // validate and assign data to villaDTO

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            VillaModel model = _mapper.Map<VillaModel>(updateDTO);
            await _villaRepository.Update(model);
            return NoContent();
        }
    }
}