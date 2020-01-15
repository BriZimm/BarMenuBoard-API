using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarMenuBoardAPI.Models;
using BarMenuBoardAPI.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Collections.Generic;

namespace BarMenuBoardAPI.Controllers
{
    [Route("api/todays-special")]
    [ApiController]
    [SwaggerGroup("Todays Specials")]
    public class TodaysSpecialsController : Controller
    {
        private readonly BarMenuBoardContext _context;

        public TodaysSpecialsController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: TodaysSpecials
        [HttpGet(Name = "Get Specials List")]
        [SwaggerOperation(OperationId = "Get a list of specials")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<TodaysSpecial>>> GetSpecialsList()
        {
            return await _context.TodaysSpecials.ToListAsync();
        }

        // GET: todays-special/5
        [HttpGet("{id}", Name = "Get Special By Id")]
        [SwaggerOperation(OperationId = "Get a special by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<TodaysSpecial>> GetSpecial(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var special = await _context.TodaysSpecials.FirstOrDefaultAsync(m => m.Id == id);

            if (special == null)
            {
                return NotFound();
            }

            return special;
        }

        // Put: Create a Special
        [HttpPut(Name = "Create Special")]
        [SwaggerOperation(OperationId = "Create a special")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TodaysSpecial>> Create(TodaysSpecial special)
        {
            if (ModelState.IsValid)
            {
                _context.Add(special);
                await _context.SaveChangesAsync();
                return special;
            }
            else
            {
                return BadRequest();
            }
        }

        // Post: Edit a Special
        [HttpPost("{special}", Name = "Edit Special")]
        [SwaggerOperation(OperationId = "Edit a special")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TodaysSpecial>> EditRecipe(TodaysSpecial special)
        {
            if (!TodaysSpecialExists(special.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(special);
                    await _context.SaveChangesAsync();
                    return special;
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

        // DELETE: /5
        [HttpDelete("{id}", Name = "Delete Special")]
        [SwaggerOperation(OperationId = "Delete a special by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var special = await _context.TodaysSpecials.FindAsync(id);

            if (special == null)
            {
                return NotFound();
            }
            _context.TodaysSpecials.Remove(special);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool TodaysSpecialExists(int id)
        {
            return _context.TodaysSpecials.Any(e => e.Id == id);
        }
    }
}
