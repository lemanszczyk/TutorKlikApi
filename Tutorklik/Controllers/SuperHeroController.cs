using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;
using Tutorklik.Data;
using Tutorklik.Models;

namespace Hero_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;  
        public SuperHeroController(DataContext context)
        {
                _context = context;
        }
        [HttpGet(Name ="GetSuper"), Authorize(Roles = "Tutor")]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHero()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            dbHero.Name = hero.Name;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            _context.SuperHeroes.Remove(dbHero);
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
