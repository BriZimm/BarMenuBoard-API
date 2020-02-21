using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarMenuBoardAPI.Models;
using BarMenuBoardAPI.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BarMenuBoardAPI.Controllers
{
    [Route("api/board-style")]
    [ApiController]
    [SwaggerGroup("Board Styles")]
    public class BoardStylesController : ControllerBase
    {
        private readonly BarMenuBoardContext _context;

        public BoardStylesController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: api/BoardStyles
        [HttpGet(Name = "Get Board Style List")]
        [SwaggerOperation(OperationId = "Get a list of recipes")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BoardStyle>>> GetBoardStyles()
        {
            return await _context.BoardStyles.ToListAsync();
        }

        // GET: api/BoardStyles/5
        [HttpGet("{id}", Name = "Get Board Style By Id")]
        [SwaggerOperation(OperationId = "Get a Board Style by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BoardStyle>> GetBoardStyle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardStyle = await _context.BoardStyles.FirstOrDefaultAsync(m => m.Id == id);

            if (boardStyle == null)
            {
                return NotFound();
            }

            return boardStyle;
        }

        // Put: Create a Board Style
        [HttpPut(Name = "Create Board Style")]
        [SwaggerOperation(OperationId = "Create a Board Style")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BoardStyle>> Create([FromBody]BoardStyle boardStyle)
        {
            if (ModelState.IsValid)
            {
                boardStyle.Updated = DateTime.Now;
                _context.BoardStyles.Add(boardStyle);
                await _context.SaveChangesAsync();
                return boardStyle;
            }
            else
            {
                return BadRequest();
            }
        }

        // Post: Edit a Board Style
        [HttpPost(Name = "Edit Board Style")]
        [SwaggerOperation(OperationId = "Edit a Board Style")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BoardStyle>> EditBoardStyle([FromBody] BoardStyle boardStyle)
        {
            if (!BoardStyleExists(boardStyle.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    boardStyle.Updated = DateTime.Now;
                    _context.BoardStyles.Update(boardStyle);
                    await _context.SaveChangesAsync();
                    return boardStyle;
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

        // Post: Edit a Board Style
        [HttpPost("make-active", Name = "Make Board Style Active")]
        [SwaggerOperation(OperationId = "Make a Board Style")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BoardStyle>> MakeBoardStyleActive([FromBody] BoardStyle boardStyle)
        {
            if (!BoardStyleExists(boardStyle.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // make current active style inactive
                    var currentlyActiveStyle = await _context.BoardStyles.FirstOrDefaultAsync(m => m.Active == true);

                    currentlyActiveStyle.Active = false;
                    currentlyActiveStyle.Updated = DateTime.Now;
                    _context.BoardStyles.Update(currentlyActiveStyle);

                    // Make selected style active
                    boardStyle.Active = true;
                    boardStyle.Updated = DateTime.Now;
                    _context.BoardStyles.Update(boardStyle);

                    await _context.SaveChangesAsync();
                    return boardStyle;
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

        // POST: Board Style/Delete/5
        [HttpDelete("{id}", Name = "Delete Board Style")]
        [SwaggerOperation(OperationId = "Delete a Board Style by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteBoardStyle(int id)
        {
            var boardStyle = await _context.BoardStyles.FindAsync(id);

            if (boardStyle == null)
            {
                return NotFound();
            }

            _context.BoardStyles.Remove(boardStyle);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool BoardStyleExists(int id)
        {
            return _context.BoardStyles.Any(e => e.Id == id);
        }
    }
}