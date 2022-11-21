using Content.Core.Entities.Media;
using Content.EndPoint.Models.Media;
using Content.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Content.EndPoint.Controllers
{
    [ApiController]
    public class MediaController : ApiController
    {
        private readonly Context _context;

        public MediaController(Context context)
        {
            _context = context;
        }

        [HttpPost("add-media")]
        public async Task<IActionResult> AddMedia(AddMediaModel req)
        {
            var presenter = _context.Set<Presenter>().SingleOrDefault(p => p.Id == req.PresenterId);
            if (presenter == null)
                return NotFound("ارائه دهنده مورد نظر ثبت نشده است!");

            

            return Ok();
        }
    }
}
