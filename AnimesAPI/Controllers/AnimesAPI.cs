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
        public  ActionResult<IEnumerable<AnimesDTO>> GetAnimes()
        {
            var allList = _context.Animes.ToList();
            var DTOList= new List<AnimesDTO>();

            foreach (var x in allList)
            {
              AnimesDTO d1=(new AnimesDTO
                {
                    Name = x.Name,
                    Genre = x.Genre

                });
                DTOList.Add(d1);
            }

            return Ok(DTOList);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AnimesDTO> GetAnimesById(int id)
        {
            if (id == 0)
                return BadRequest();

            var targetRecord = _context.Animes.Find(id);

            if (targetRecord == null)
                return NotFound();

            AnimesDTO d1 = new AnimesDTO
            {
                Name = targetRecord.Name,
                Genre = targetRecord.Genre
            };
            return Ok(d1);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AnimesDTO> CreateAnime([FromBody] AnimesDTO animesDTO)
        {
            if (animesDTO == null)
                return BadRequest();

            Animes a1 = new Animes
            {
                Name = animesDTO.Name,
                Genre = animesDTO.Genre
            };

            _context.Animes.Add(a1);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAnimesById), new { id = a1.Id }, animesDTO);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAnime(int id, [FromBody] AnimesDTO animesDTO)
        {
            if (animesDTO == null || id == 0)
                return BadRequest();

            var targetRecord = _context.Animes.Find(id);

            if (targetRecord == null)
                return NotFound();


            targetRecord.Name = animesDTO.Name;
            targetRecord.Genre = animesDTO.Genre;

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
