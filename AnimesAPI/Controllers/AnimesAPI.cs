using AnimesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using AnimesAPI.Models;
namespace AnimesAPI.Controllers
{
    [ApiController]
    [Route("api/AnimesAPI")]
    public class AnimesAPI : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimesAPI(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAnimes()
        {
            var allList = _context.Animes.ToList();
            return Ok(allList);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAnimesById(int id)
        {
            if (id == 0)
                return BadRequest();

            var targetRecord = _context.Animes.Find(id);

            if (targetRecord == null)
                return NotFound();

            return Ok(targetRecord);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateAnime([FromBody] Animes animes)
        {
            if (animes == null)
                return BadRequest();

            _context.Animes.Add(animes);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAnimesById),
                new { id = animes.Id },
                animes);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAnime(int id, [FromBody] Animes animes)
        {
            if (animes == null || id == 0)
                return BadRequest();

            var targetRecord = _context.Animes.Find(id);

            if (targetRecord == null)
                return NotFound();

            targetRecord.Name = animes.Name;
            targetRecord.Genre = animes.Genre;

            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAnime(int id)
        {
            if (id == 0)
                return BadRequest();

            var targetRecord = _context.Animes.Find(id);

            if (targetRecord == null)
                return NotFound();

            _context.Animes.Remove(targetRecord);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
