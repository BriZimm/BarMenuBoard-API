using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarMenuBoardAPI.Models;
using BarMenuBoardAPI.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BarMenuBoardAPI.Controllers
{
    [Route("api/mixer-categories")]
    [ApiController]
    [SwaggerGroup("Mixer Categories")]
    public class MixerCategoriesController : ControllerBase
    {
        private readonly BarMenuBoardContext _context;

        public MixerCategoriesController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: Get Mixer Categories List
        [HttpGet(Name = "Get List of Mixer Categories")]
        [SwaggerOperation(OperationId = "Get a list of Mixer Categories")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<MixerCategory>>> GetMixerCategories()
        {
            return await _context.MixerCategory.OrderBy(x => x.Category).ToListAsync();
        }

        // GET: Get Mixer Category by Id
        [HttpGet("{id}", Name = "Get Mixer Category By Id")]
        [SwaggerOperation(OperationId = "Get a Mixer Category by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<MixerCategory>> GetMixerCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.MixerCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: Create a Mixer Category
        [HttpPut(Name = "Create Mixer Category")]
        [SwaggerOperation(OperationId = "Create a Mixer Category")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MixerCategory>> CreateMixerCategory([FromBody]MixerCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.MixerCategory.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: Edit Mixer Category
        [HttpPost(Name = "Edit Mixer Category")]
        [SwaggerOperation(OperationId = "Edit a Mixer Category")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MixerCategory>> EditMixerCategory([FromBody]MixerCategory category)
        {
            if (!MixerCategoryExists(category.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.MixerCategory.Update(category);
                    await _context.SaveChangesAsync();
                    return category;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: Delete Mixer Category
        [HttpDelete("{id}", Name = "Delete Mixer Category")]
        [SwaggerOperation(OperationId = "Delete a Mixer Category by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteMixerCategory(int id)
        {
            var category = await _context.MixerCategory.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.MixerCategory.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool MixerCategoryExists(int id)
        {
            return _context.MixerCategory.Any(e => e.Id == id);
        }
    }
}