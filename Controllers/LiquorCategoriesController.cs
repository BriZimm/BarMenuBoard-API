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
    [Route("api/liquor-categories")]
    [ApiController]
    [SwaggerGroup("Liquor Categories")]
    public class LiquorCategoriesController : ControllerBase
    {
        private readonly BarMenuBoardContext _context;

        public LiquorCategoriesController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: Get Liquor Categories List
        [HttpGet(Name = "Get List of Liquor Categories")]
        [SwaggerOperation(OperationId = "Get a list of Liquor Categories")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<LiquorCategory>>> GetLiquorCategories()
        {
            return await _context.LiquorCategory.OrderBy(x => x.Category).ToListAsync();
        }

        // GET: Get Liquor Category by Id
        [HttpGet("{id}", Name = "Get Liquor Category By Id")]
        [SwaggerOperation(OperationId = "Get a Liquor Category by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<LiquorCategory>> GetLiquorCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.LiquorCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: Create a Liquor Category
        [HttpPut(Name = "Create Liquor Category")]
        [SwaggerOperation(OperationId = "Create a Liquor Category")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LiquorCategory>> CreateLiquorCategory([FromBody]LiquorCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.LiquorCategory.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: Edit Liquor Category
        [HttpPost(Name = "Edit Liquor Category")]
        [SwaggerOperation(OperationId = "Edit a Liquor Category")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LiquorCategory>> EditLiquorCategory([FromBody]LiquorCategory category)
        {
            if (!LiquorCategoryExists(category.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.LiquorCategory.Update(category);
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

        // DELETE: Delete Liquor Category
        [HttpDelete("{id}", Name = "Delete Liquor Category")]
        [SwaggerOperation(OperationId = "Delete a Liquor Category by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteLiquorCategory(int id)
        {
            var category = await _context.LiquorCategory.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.LiquorCategory.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LiquorCategoryExists(int id)
        {
            return _context.LiquorCategory.Any(e => e.Id == id);
        }
    }
}