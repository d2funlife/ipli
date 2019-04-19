using System;
using System.Linq;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Web.Models.Requests;
using IpLi.Web.Models.Responses;
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

        [HttpGet]
        public async Task<ActionResult<Page<SourceResponse>>> Get([FromQuery] GetSourcesRequest request)
        {
            var sourcesPage = await _sourceManager.GetAsync(request.ToDomain(), Cancel);
            SetTotalCountHeader(sourcesPage.TotalCount);
            return Ok(sourcesPage.Items.Select(x => new SourceResponse(x)));
        }

        [HttpGet("aggregations/title")]
        public async Task<ActionResult<Page<SourceAggregationResponse>>> GetAggregationByTitle([FromQuery] GetSourcesRequest request)
        {
            var sourcesPage = await _sourceManager.GetAggregationByTitleAsync(request.ToDomain(), Cancel);
            SetTotalCountHeader(sourcesPage.TotalCount);
            return Ok(sourcesPage.Items.Select(x => new SourceAggregationResponse(x.Title, x.Sources)));
        }

        [HttpGet("titles")]
        public async Task<ActionResult<Page<String>>> GetTitles([FromQuery] GetSourcesRequest request)
        {
            var sourcesPage = await _sourceManager.GetAggregationTitlesAsync(request.ToDomain(), Cancel);
            SetTotalCountHeader(sourcesPage.TotalCount);
            return Ok(sourcesPage.Items);
        }

        [HttpPost("import")]
        public async Task<ActionResult> ImportFromPlaylist([FromForm] IFormFile file)
        {
            var importedSources = await _sourceManager.ImportSourcesAsync(file.OpenReadStream(), Cancel);
            return Ok(new {importedCount = importedSources});
        }
    }
}