using Application.Service.Interface;
using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<ActionResult<ItemDiff>> GetAll()
        {
            try
            {
                return Ok(await _diffService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll: {ex}");
                return BadRequest(ex);
            }
        }

        // GET: v1/diff/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDiff>> GetById(long id)
        {
            try
            {
                // Check input parameters
                if (id <= 0) return BadRequest("Invalid id");

                var result = await _diffService.GetById(id);

                if (result is EmptyResult)
                {
                    return NotFound();
                }
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
        [HttpPut("{id}/{side}")]
        public async Task<ActionResult<ResultDiff>> PutById(long id, string side, InputDiff diffInput)
        {
            try
            {
                if (side != "left" && side != "right") return NotFound();
                if (diffInput is null || diffInput.Data is null) return BadRequest();
                _diffService.PutById(id, side, diffInput);
                return new CreatedResult("Dados salvos com sucesso", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PutById: {ex}");
                return BadRequest(ex);
            }

        }
    }


























    //// GET: Diff
    //public async Task<IActionResult> Index()
    //{
    //      return _context.DiffItem != null ? 
    //                  View(await _context.DiffItem.ToListAsync()) :
    //                  Problem("Entity set 'DiffContext.DiffItem'  is null.");
    //}

    //// GET: Diff/Details/5
    //public async Task<IActionResult> Details(long? id)
    //{
    //    if (id == null || _context.DiffItem == null)
    //    {
    //        return NotFound();
    //    }

    //    var diffItem = await _context.DiffItem
    //        .FirstOrDefaultAsync(m => m.id == id);
    //    if (diffItem == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(diffItem);
    //}

    //// GET: Diff/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: Diff/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("id,left,right")] DiffItem diffItem)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(diffItem);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(diffItem);
    //}

    //// GET: Diff/Edit/5
    //public async Task<IActionResult> Edit(long? id)
    //{
    //    if (id == null || _context.DiffItem == null)
    //    {
    //        return NotFound();
    //    }

    //    var diffItem = await _context.DiffItem.FindAsync(id);
    //    if (diffItem == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(diffItem);
    //}

    //// POST: Diff/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(long? id, [Bind("id,left,right")] DiffItem diffItem)
    //{
    //    if (id != diffItem.id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(diffItem);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!DiffItemExists(diffItem.id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(diffItem);
    //}

    //// GET: Diff/Delete/5
    //public async Task<IActionResult> Delete(long? id)
    //{
    //    if (id == null || _context.DiffItem == null)
    //    {
    //        return NotFound();
    //    }

    //    var diffItem = await _context.DiffItem
    //        .FirstOrDefaultAsync(m => m.id == id);
    //    if (diffItem == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(diffItem);
    //}

    //// POST: Diff/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(long? id)
    //{
    //    if (_context.DiffItem == null)
    //    {
    //        return Problem("Entity set 'DiffContext.DiffItem'  is null.");
    //    }
    //    var diffItem = await _context.DiffItem.FindAsync(id);
    //    if (diffItem != null)
    //    {
    //        _context.DiffItem.Remove(diffItem);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool DiffItemExists(long? id)
    //{
    //  return (_context.DiffItem?.Any(e => e.id == id)).GetValueOrDefault();
    //}
}

