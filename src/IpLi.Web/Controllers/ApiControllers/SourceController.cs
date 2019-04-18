using System;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IpLi.Web.Controllers.ApiControllers
{
    [Route("api/sources")]
    public class SourceController : BaseController
    {
        private readonly ISourceManager _sourceManager;

        public SourceController(ISourceManager sourceManager)
        {
            _sourceManager = sourceManager;
        }

        [HttpPost("import")]
        public async Task<ActionResult> ImportFromPlaylist([FromForm]IFormFile file)
        {
            var importedSources = await _sourceManager.ImportSourcesAsync(file.OpenReadStream(), Cancel);
            return Ok(new {importedCount = importedSources});
        }
    }
}