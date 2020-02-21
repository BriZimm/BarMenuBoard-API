using System;
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
    [Route("api/inventory")]
    [ApiController]
    [SwaggerGroup("Inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly BarMenuBoardContext _context;

        public InventoryController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: Get Inventory List
        [HttpGet(Name = "Get Inventory List")]
        [SwaggerOperation(OperationId = "Get a list of Inventory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Inventory>>> GetInventory()
        {
            return await _context.Inventory.ToListAsync();
        }

        // GET: Get Inventory Item by Id
        [HttpGet("{id}", Name = "Get Inventory Item By Id")]
        [SwaggerOperation(OperationId = "Get an Inventory item by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Inventory>> GetInventoryitem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FirstOrDefaultAsync(m => m.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: Create an Inventory Item
        [HttpPut(Name = "Create Inventory Item")]
        [SwaggerOperation(OperationId = "Create an Inventory Item")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Inventory>> Create([FromBody]Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                inventory.Updated = DateTime.Now;
                _context.Inventory.Add(inventory);
                await _context.SaveChangesAsync();
                return inventory;
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: Edit Inventory Item
        [HttpPost(Name = "Edit Inventory Item")]
        [SwaggerOperation(OperationId = "Edit an Inventory Item")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Inventory>> EditInventoryItem([FromBody]Inventory inventory)
        {
            if (!InventoryExists(inventory.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    inventory.Updated = DateTime.Now;
                    _context.Inventory.Update(inventory);
                    await _context.SaveChangesAsync();
                    return inventory;
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

        // DELETE: Delete Inventory Item
        [HttpDelete("{id}", Name = "Delete Inventory Item")]
        [SwaggerOperation(OperationId = "Delete an Inventory Item by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteInventoryItem(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }
    }
}