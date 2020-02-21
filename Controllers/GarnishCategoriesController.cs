using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarMenuBoardAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using BarMenuBoardAPI.Attributes;

namespace BarMenuBoardAPI.Controllers
{
    [Route("api/garnish-categories")]
    [ApiController]
    [SwaggerGroup("Garnish Categories")]
    public class GarnishCategoriesController : ControllerBase
    {
        private readonly BarMenuBoardContext _context;

        public GarnishCategoriesController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: Get Garnish Categories List
        [HttpGet(Name = "Get List of Garnish Categories")]
        [SwaggerOperation(OperationId = "Get a list of Garnish Categories")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GarnishCategory>>> GetGarnishCategories()
        {
            return await _context.GarnishCategory.OrderBy(x => x.Category).ToListAsync();
        }

        // GET: Get Garnish Category by Id
        [HttpGet("{id}", Name = "Get Garnish Category By Id")]
        [SwaggerOperation(OperationId = "Get a Garnish Category by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GarnishCategory>> GetGarnishCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.GarnishCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: Create a Garnish Category
        [HttpPut(Name = "Create Garnish Category")]
        [SwaggerOperation(OperationId = "Create a Garnish Category")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GarnishCategory>> CreateGarnishCategory([FromBody]GarnishCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.GarnishCategory.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: Edit Garnish Category
        [HttpPost(Name = "Edit Garnish Category")]
        [SwaggerOperation(OperationId = "Edit a Garnish Category")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GarnishCategory>> EditGarnishCategory([FromBody]GarnishCategory category)
        {
            if (!GarnishCategoryExists(category.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.GarnishCategory.Update(category);
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

        // DELETE: Delete Garnish Category
        [HttpDelete("{id}", Name = "Delete Garnish Category")]
        [SwaggerOperation(OperationId = "Delete a Garnish Category by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteGarnishCategory(int id)
        {
            var category = await _context.GarnishCategory.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.GarnishCategory.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool GarnishCategoryExists(int id)
        {
            return _context.GarnishCategory.Any(e => e.Id == id);
        }
    }
}