using Content.Core.Entities.Media;
using Content.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Content.EndPoint.Controllers.Media
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresenterController : ControllerBase
    {
        private readonly Context _context;

        public PresenterController(Context context)
        {
            _context = context;
        }

        [HttpPost("add-presenter")]
        public async Task<IActionResult> AddPresenter(string name)
        {
            var presenter = _context.Set<Presenter>().Where(p => p.Name == name).FirstOrDefault();
            if (presenter != null)
                return BadRequest("ارائه دهنده مورد نظر از پیش وجود دارد!");

            presenter = new Presenter(name);
            await _context.Set<Presenter>().AddAsync(presenter);
            await _context.SaveChangesAsync();
            return Ok(presenter.Id);
        }



    }
}
