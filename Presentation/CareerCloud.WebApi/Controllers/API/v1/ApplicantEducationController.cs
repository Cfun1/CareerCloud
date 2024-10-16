using Asp.Versioning;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using CareerCloud.WebAPI.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CareerCloud.WebAPI.Controllers;
[ApiController] //auto validation, verbose aggregated response in case of
                //failed model validation with BadRequet
                //without this, valdiation needs to be performed manually in each endpoint method
                //using ModelState.IsValid

[ApiVersion("1.0")]
[Route("api/careercloud/[Controller]/v{version:apiVersion}")]
/// api/careercloud/ApplicantEducation/v1
public partial class ApplicantEducationController :
                     CareerCloudBaseController<ApplicantEducationPoco,
                                                ApplicantEducationLogic>
//,IApiController<ApplicantEducationDto>
{
    public ApplicantEducationController(IDataRepository<ApplicantEducationPoco> applicantEducationRepo) : base(applicantEducationRepo)
    {
    }

    //todo: only needed for the test, DI workaround, replace with upper later
    //public ApplicantEducationController() : base() { }

    /// POST: api/careercloud/ApplicantEducation/v1/Education
    [HttpPost("Education")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ApplicantEducationPoco>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApplicantEducationPoco>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco?[] pocos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            pocos.ToList().ForEach(poco => poco.Id =
               poco.Id == Guid.Empty ? Guid.NewGuid() : poco.Id);

            logic.Add(pocos);
            //CreatedAtAction allows HATEOS implementation
            //return CreatedAtAction(nameof(GetApplicantEducation), new { pocos.First()!.Id }, pocos);
            return Ok();
        }

        catch (AggregateException ex)
        {
            return BadRequest(ex.InnerExceptions);
        }
        catch (Exception ex)
        {
            //todo response could be null here
            //Response.Headers?.TryAdd("Retry-After", "3m");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// GET: api/careercloud/ApplicantEducation/v1/Education
    [HttpGet("Education")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ICollection<ApplicantEducationPoco>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<ICollection<ApplicantEducationPoco>> GetAll()
    {
        try
        {
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

    /*https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-constraint-reference
    [HttpGet("{id:Guid}")]  //automatic validate the param type.
    Avoid using it because it returns returns 404 if type mismatch, which is confusing
    */

    /// GET: api/careercloud/ApplicantEducation/v1/Education/{id}

    [HttpGet("education/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ApplicantEducationPoco>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    //public ActionResult<ApplicantEducationDto> GetApplicantEducation(Guid id)
    public ActionResult GetApplicantEducation(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest(id);

        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            var apiResult = logic.Get(id);

            if (apiResult == null)
                //return NotFound($"The object with id={id} was not found");
                return NotFound();

            return Ok(apiResult);
        }

        catch (AggregateException ex)
        {
            return BadRequest(ex);
        }

        catch (NullReferenceException)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// DELETE: api/careercloud/ApplicantEducation/v1/Education
    [HttpDelete("Education")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            logic.Delete(pocos);
            //return StatusCode(StatusCodes.Status202Accepted);
            return Ok();
        }

        catch (Exception)
        {
            //Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /*
    /// DELETE: api/careercloud/ApplicantEducation/v1/Education/{Id}
    [HttpDelete("Education/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(Guid id)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            var poco = logic.Get(id);
            if (poco is null)
                return NotFound();

            logic.Delete([poco]);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }*/

    /// PUT: api/careercloud/ApplicantEducation/v1/Education/
    [HttpPut("Education")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            //todo find a solution on how to get timestamp whenever mapper dto -> model -> EF
            //otherwise EF sql operation will fail

            logic.Update(pocos);

            return Ok();
        }

        catch (Exception ex)
        {
            //Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}
