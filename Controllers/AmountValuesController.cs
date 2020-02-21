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

namespace BarMenuBoardAPI.Controllers
{
    [Route("api/amount-values")]
    [ApiController]
    public class AmountValuesController : ControllerBase
    {
        private readonly BarMenuBoardContext _context;

        public AmountValuesController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: api/AmountValues
        [HttpGet(Name = "Get List of Amount Values")]
        [SwaggerOperation(OperationId = "Get a list of Garnish Categories")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<AmountValue>>> GetAmountValues()
        {
            return await _context.AmountValues.OrderBy(x => x.ListOrder).ToListAsync();
        }

        // GET: api/AmountValues/5
        [HttpGet("{id}", Name = "Get Amount Value By Id")]
        [SwaggerOperation(OperationId = "Get an Amount Value by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AmountValue>> GetAmountValue(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amountValue = await _context.AmountValues.FirstOrDefaultAsync(m => m.Id == id);

            if (amountValue == null)
            {
                return NotFound();
            }

            return amountValue;
        }

        // PUT: api/AmountValues/5
        [HttpPut(Name = "Create Amount Value")]
        [SwaggerOperation(OperationId = "Create a Amount Value")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AmountValue>> CreateAmountValue([FromBody]AmountValue amountValue)
        {
            if (ModelState.IsValid)
            {
                _context.AmountValues.Add(amountValue);
                await _context.SaveChangesAsync();
                return amountValue;
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/AmountValues
        [HttpPost(Name = "Edit Amount Value")]
        [SwaggerOperation(OperationId = "Edit an Amount Value")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AmountValue>> EditAmountValue([FromBody]AmountValue amountValue)
        {
            if (!AmountValueExists(amountValue.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.AmountValues.Update(amountValue);
                    await _context.SaveChangesAsync();
                    return amountValue;
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

        // DELETE: api/AmountValues/5
        [HttpDelete("{id}", Name = "Delete Amount Value")]
        [SwaggerOperation(OperationId = "Delete an Amount Value by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteAmountValue(int id)
        {
            var amountValue = await _context.AmountValues.FindAsync(id);

            if (amountValue == null)
            {
                return NotFound();
            }

            _context.AmountValues.Remove(amountValue);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AmountValueExists(int id)
        {
            return _context.AmountValues.Any(e => e.Id == id);
        }
    }
}