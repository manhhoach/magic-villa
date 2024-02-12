using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.Villa.Dto;
using MagicVilla_VillaAPI.Models.VillaNumber;
using MagicVilla_VillaAPI.Models.VillaNumber.Dto;
using MagicVilla_VillaAPI.Repository.Villa;
using MagicVilla_VillaAPI.Repository.VillaNumber;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;
        protected APIResponse _response;

        public VillaNumberAPIController(
            ILogging logger,
            IMapper mappingConfig,
            IVillaNumberRepository villaNumberRepository,
            IVillaRepository villaRepository
        )
        {
            _logger = logger;
            _mapper = mappingConfig;
            _villaNumberRepository = villaNumberRepository;
            _villaRepository = villaRepository;
            _response = new APIResponse();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<VillaNumberModel> villaList = await _villaNumberRepository.GetAll();
                _response.Result = _mapper.Map<List<VillaNumberDto>>(villaList);
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


        [HttpGet("{id}")]
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
                var data = await _villaNumberRepository.GetOne(e => e.VillaNo == id);
                if (data != null)
                {
                    _response.Result = _mapper.Map<VillaNumberDto>(data);
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
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaNumberCreateDto createDTO)
        {
            try
            {
                var villa = await _villaRepository.GetOne(v => v.Id == createDTO.VillaId);
                if(villa == null) {
                    return BadRequest();
                }
                if (createDTO == null)
                {
                    return BadRequest();
                }

                VillaNumberModel model = _mapper.Map<VillaNumberModel>(createDTO);
                await _villaNumberRepository.Create(model);
                _response.Result = _mapper.Map<VillaNumberDto>(model);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                var villa = await _villaNumberRepository.GetOne(u => u.VillaNo == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _villaNumberRepository.Remove(villa);
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

        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateVilla([FromBody] VillaNumberUpdateDto updateDTO)
        {
            try
            {
                var villa = await _villaRepository.GetOne(v => v.Id == updateDTO.VillaId);
                if (villa == null)
                {
                    return BadRequest();
                }
                VillaNumberModel model = _mapper.Map<VillaNumberModel>(updateDTO);
                await _villaNumberRepository.UpdateAsync(model);
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

        
    }
}