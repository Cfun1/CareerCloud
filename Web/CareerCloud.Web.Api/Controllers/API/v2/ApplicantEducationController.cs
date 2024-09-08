using Asp.Versioning;
using CareerCloud.DataTransfer;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebApp.API;

[ApiVersion("2.0")] //, Deprecated = true)]
public partial class ApplicantEducationsController
{

    /// GET: api/ApplicantEducation
    [MapToApiVersion("2.0")]
    [HttpGet]
    [ProducesResponseType<ICollection<ApplicantEducationDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<string> GetAllv2()
    {
        try
        {
            throw new Exception();

            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            var apiResult = logic.GetAll();

            return Ok(apiResult);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "3m");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}