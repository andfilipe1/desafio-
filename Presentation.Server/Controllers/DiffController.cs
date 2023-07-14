using Application.Service.Interface;
using Domain.Model.DTO;
using Domain.Model.Entities;
using Domain.Model.Enuns;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Server.Controllers
{
    [Route("v1/diff")]
    [ApiController]
    public class DiffController : Controller
    {
        private readonly ILogger<DiffController> _logger;
        private readonly IDiffService _diffService;

        public DiffController(ILogger<DiffController> logger, IDiffService diffService)
        {
            _logger = logger;
            _diffService = diffService;
        }

        // GET: v1/diff/
        /// <summary>
        /// Retrieves all ItemDiff entities.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ItemDiff>> GetAll()
        {
            try
            {
                var result = await _diffService.GetAll();
                var resultCount = result.Count();
                if (resultCount == 0)
                {
                    return Content("No data found.", "text/plain");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll: {ex}");
                return BadRequest(ex);
            }
        }



        // GET: v1/diff/{id}
        /// <summary>
        /// Retrieves a specific ItemDiff entity by ID.
        /// </summary>
        /// <param name="id">The ID of the ItemDiff entity.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDiff>> GetById(long id)
        {
            try
            {
                // Check input parameters
                if (id <= 0) return BadRequest("Invalid id");
                var result = await _diffService.GetById(id);
                if (result.TypeDiffResult == TypeDiffResult.Empty) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetById: {ex}");
                return BadRequest(ex);
            }
        }

        // PUT: v1/diff/{id}/left
        // PUT: v1/diff/{id}/right
        /// <summary>
        /// Updates the left or right side of an ItemDiff entity by ID.
        /// </summary>
        /// <param name="id">The ID of the ItemDiff entity.</param>
        /// <param name="side">The side to update ("left" or "right").</param>
        /// <param name="diffInput">The input data containing the diff.</param>
        [HttpPut("{id}/{side}")]
        public ActionResult<ResultDiff> PutById(long id, string side, InputDiff diffInput)
        {
            if (!Enum.TryParse<Side>(side, true, out var sideEnum))
                return NotFound();

            if (diffInput is null || diffInput.Data is null)
                return BadRequest();

            try
            {
                _diffService.PutById(id, sideEnum, diffInput);
                return new CreatedResult("Data successfully saved", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PutById: {ex}");
                return BadRequest(ex);
            }
        }
    }
}
