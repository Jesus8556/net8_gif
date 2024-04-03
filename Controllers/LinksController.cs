using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LinksController : ControllerBase
{
    private readonly LinkExtractor _linkExtractor;

    public LinksController(LinkExtractor linkExtractor)
    {
        _linkExtractor = linkExtractor;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int limit = 8)
    {
        var links = await _linkExtractor.ExtractLinksAsync(limit);
        return Ok(links);
    }
}
