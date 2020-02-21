using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarMenuBoardAPI.Models;
using BarMenuBoardAPI.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Microsoft.AspNetCore.Cors;

namespace BarMenuBoardAPI.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    [SwaggerGroup("Recipes")]
    public class RecipesController : Controller
    {
        private readonly BarMenuBoardContext _context;

        public RecipesController(BarMenuBoardContext context)
        {
            _context = context;
        }

        // GET: recipe
        [HttpGet(Name = "Get Recipe List")]
        [SwaggerOperation(OperationId = "Get a list of recipes")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Recipe>>> GetRecipeList()
        {
            return await _context.Recipes.ToListAsync();
        }

        // GET: recipe/5
        [HttpGet("{id}", Name = "Get Recipe By Id")]
        [SwaggerOperation(OperationId = "Get a recipe by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Recipe>> GetRecipe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            
            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // Put: Create a Recipe
        [HttpPut(Name = "Create Recipe")]
        [SwaggerOperation(OperationId = "Create a recipe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Recipe>> Create([FromBody]Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return recipe;
            }
            else
            {
                return BadRequest();
            }
        }

        // Post: Edit a Recipe
        [HttpPost(Name = "Edit Recipe")]
        [SwaggerOperation(OperationId = "Edit a recipe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Recipe>> EditRecipe([FromBody]Recipe recipe)
        {
            if (!RecipeExists(recipe.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var special = await _context.TodaysSpecials.FirstOrDefaultAsync(m => m.Id == recipe.SpecialId);

                    if (special != null)
                    {
                        _context.Recipes.Update(recipe);
                    } 
                    else
                    {
                        // The recipe is a confirmed special, so let's update it
                        special.Name = recipe.Name;
                        special.Description = recipe.Description;
                        special.Type = recipe.Type;
                        special.Glass = recipe.Glass;
                        special.Served = recipe.Served;
                        special.Garnish = recipe.Garnish;
                        special.SimilarTastes = recipe.SimilarTastes;
                        special.Strength = recipe.Strength;
                        special.Image = recipe.Image;
                        _context.TodaysSpecials.Update(special);
                    }

                    await _context.SaveChangesAsync();
                    return recipe;
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

        // POST: Recipes/Delete/5
        [HttpDelete("{id}", Name = "Delete Recipe")]
        [SwaggerOperation(OperationId = "Delete a recipe by Id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var special = await _context.TodaysSpecials.FindAsync(recipe.SpecialId);

            if (special != null)
            {
                _context.TodaysSpecials.Remove(special);
                await _context.SaveChangesAsync();
            }
            
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
