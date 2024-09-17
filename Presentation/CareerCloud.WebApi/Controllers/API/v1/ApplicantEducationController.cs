using Asp.Versioning;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.DataTransfer;
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

    //todo: only needed for the test, DI workaround, delete after
    public ApplicantEducationController() : base() { }

    /// POST: api/ApplicantEducations/
    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ApplicantEducationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApplicantEducationDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<ICollection<ApplicantEducationDto>> Create([FromBody] ApplicantEducationDto?[] dtos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (dtos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(dtos));

            var pocos = dtos!.ToModel().ToArray();
            pocos.ToList().ForEach(poco => poco.Id =
                poco.Id == Guid.Empty ? Guid.NewGuid() : poco.Id);
            dtos.ToList().Clear();
            dtos = pocos!.ToDto().ToArray();


            logic.Add(pocos);
            //CreatedAtAction allows HATEOS implementation
            return CreatedAtAction(nameof(GetApplicantEducation), new { dtos.First()!.Id }, dtos);
        }

        catch (AggregateException ex)
        {
            return BadRequest(ex.InnerExceptions);
        }
        catch (Exception ex)
        {
            Response.Headers?.TryAdd("Retry-After", "3m");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// GET: api/ApplicantEducations
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ICollection<ApplicantEducationDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<ICollection<ApplicantEducationDto>> GetAll()
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            var apiResult = logic.GetAll();

            return Ok(apiResult.ToDto());
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

    /// GET:  api/careercloud/ApplicantEducation/v1/education/{id}
    [HttpGet("education/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ApplicantEducationDto>(StatusCodes.Status200OK)]
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
                return NotFound($"The object with id={id} was not found");

            return Ok(apiResult.ToDto());
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

    /// DELETE: api/ApplicantEducations/
    [HttpDelete]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete([FromBody] ApplicantEducationDto[] dtos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (dtos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(dtos));

            logic.Delete(dtos.ToModel().ToArray());
            return StatusCode(StatusCodes.Status202Accepted);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// DELETE: api/ApplicantEducations/id
    [HttpDelete("{id}")]
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
    }

    /// PUT: api/ApplicantEducations/
    [HttpPut]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<ICollection<ApplicantEducationDto>> Update([FromBody] ApplicantEducationDto[] dtos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (dtos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(dtos));

            //todo find a solution on how to get timestamp whenever mapper dto -> model -> EF
            //otherwise EF sql operation will fail

            var pocos = dtos.ToModel().ToArray();
            logic.Update(pocos);

            return Ok(pocos.ToDto());
        }

        catch (Exception ex)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}
