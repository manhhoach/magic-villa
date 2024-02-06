using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.Villa.Dto;
using MagicVilla_VillaAPI.Repository.Villa;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;

        public VillaAPIController(
            ILogging logger,
            IMapper mappingConfig,
            IVillaRepository villaRepository
        )
        {
            _logger = logger;
            _mapper = mappingConfig;
            _villaRepository = villaRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<VillaModel> villaList = await _villaRepository.GetAll();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }


        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.Log("get villa error with id " + id, "error");
                return BadRequest();
            }
            var data = await _villaRepository.GetOne(e => e.Id == id);
            if (data != null)
            {
                return Ok(_mapper.Map<VillaDTO>(data));
            }
            return NotFound();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            // validate tay
            //if (ModelState.IsValid == false)
            //{
            //    return BadRequest(ModelState);
            //}

            if (createDTO == null)
            {
                return BadRequest();
            }

            VillaModel model = _mapper.Map<VillaModel>(createDTO);
            await _villaRepository.Create(model);
            return Ok(_mapper.Map<VillaDTO>(model));
        }

        [HttpDelete("{id}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();
            var villa = await _villaRepository.GetOne(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _villaRepository.Remove(villa);
            return NoContent();

        }

        [HttpPut("{id}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            if (updateDTO == null || updateDTO.Id != id)
                return BadRequest();
            VillaModel model = _mapper.Map<VillaModel>(updateDTO);
            await _villaRepository.Update(model);
            return NoContent();

        }

        [HttpPatch("{id}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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