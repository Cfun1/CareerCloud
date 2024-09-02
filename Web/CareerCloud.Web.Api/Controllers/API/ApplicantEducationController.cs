using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.DataTransfer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

/*Futur consideration:
 * sorting, pagination, versionning, security: (authorizing, authentication, rate limit)
 * https://datatracker.ietf.org/doc/html/rfc7231
 * don't return deleted object
 */

namespace CareerCloud.WebApp.API;

[ApiController]
[Route("api/[controller]")]

public class ApplicantEducationController : ControllerBase, IApiController<ApplicantEducationPoco>
{
    readonly ApplicantEducationLogic? logic;
    ApplicantEducationDto? applicantEducationModel;

    public ApplicantEducationController(IDataRepository<ApplicantEducationPoco> applicantEducationRepo)
    {
        logic = new ApplicantEducationLogic(applicantEducationRepo);
    }

    // POST: api/ApplicantEducation/Add/{Id}
    [HttpPost("Add")]
    public ActionResult<string> Add([FromBody] ApplicantEducationPoco[] pocos)
    {
        throw new NotImplementedException();
    }

    // GET: api/ApplicantEducation
    [HttpGet]
    [ProducesResponseType<ICollection<ApplicantEducationDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<string> GetAll()
    {
        try
        {
            if (logic is null)
                throw new ArgumentNullException(nameof(logic));

            var apiResult = logic.GetAll();
            string json = JsonConvert.SerializeObject(apiResult, Formatting.Indented);
            return Ok(json);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "3m");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }


    // GET: api/ApplicantEducation/{Id}
    [HttpGet("{id}")]
    [ProducesResponseType<ApplicantEducationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<string> GetSingle(Guid id)
    {
        try
        {
            if (logic is null)
                throw new ArgumentNullException(nameof(logic));

            var apiResult = logic.Get(id);

            if (apiResult == null)
                return NotFound($"The object with id={id} was not found");

            string json = JsonConvert.SerializeObject(apiResult, Formatting.Indented);
            return Ok(json);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    //PUT: api/ApplicantEducation/Remove/
    [HttpDelete("Remove")]
    public ActionResult<string> Remove([FromBody] ApplicantEducationPoco[] pocos)
    {
        throw new NotImplementedException();
    }

    //PUT: api/ApplicantEducation/Update/
    [HttpPut("Update")]
    public ActionResult<string> Update([FromBody] ApplicantEducationPoco[] pocos)
    {
        throw new NotImplementedException();
    }
}
