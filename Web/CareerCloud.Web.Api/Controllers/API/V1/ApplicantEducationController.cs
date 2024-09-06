using Asp.Versioning;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.DataTransfer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CareerCloud.WebApp.API;

[ApiController]   //auto validation, verbose aggregated output in case of failed model validation BadRequet
                  //without this, valdiation needs to be performed manually in each endpoint method
                  //using ModelState.IsValid
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
public partial class ApplicantEducationController : ControllerBase,
                                            IApiController<ApplicantEducationDto>
{
    readonly ApplicantEducationLogic? logic;
    ApplicantEducationDto? applicantEducationModel;

    public ApplicantEducationController(IDataRepository<ApplicantEducationPoco> applicantEducationRepo)
    {
        logic = new ApplicantEducationLogic(applicantEducationRepo);
    }

    /// POST: api/ApplicantEducation/Add/{Id}
    [HttpPost("Add")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ApplicantEducationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApplicantEducationDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<string> Add([FromBody] ApplicantEducationDto[] dtos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            var pocos = dtos.ToModel().ToArray();
            logic.Add(pocos);
            string jsonResult = JsonConvert.SerializeObject(pocos, Formatting.Indented);
            return Created("", jsonResult);
        }

        catch (AggregateException ex)
        {
            return BadRequest(ex);
        }
        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "3m");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// GET: api/ApplicantEducation
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ICollection<ApplicantEducationDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<string> GetAll()
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

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

    /*https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-constraint-reference
    [HttpGet("{id:Guid}")]  //automatic validate the param type.
    Avoid using it because it returns returns 404 if type mismatch, which is confusing
    */

    /// GET: api/ApplicantEducation/{Id}
    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ApplicantEducationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<string> GetSingle(Guid id)
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

            string json = JsonConvert.SerializeObject(apiResult, Formatting.Indented);
            return Ok(json);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// PUT: api/ApplicantEducation/Remove/
    [HttpDelete("Remove")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Remove([FromBody] ApplicantEducationDto[] dtos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (dtos.Length == 0)
                throw new ArgumentException(nameof(dtos));

            var originalPoco = logic.Get(dtos[0].Id);
            originalPoco.CompletionPercent = 55;

            // logic.Update(dtos.Model());
            logic.Update([originalPoco]);

            return StatusCode(StatusCodes.Status202Accepted);
        }

        catch (Exception ex)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// PUT: api/ApplicantEducation/Update/
    [HttpPut("Update")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<string> Update([FromBody] ApplicantEducationDto[] dtos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (dtos.Length == 0)
                throw new ArgumentException(nameof(dtos));

            var pocos = dtos.ToModel().ToArray();
            var ids = pocos.Select(x => x.Id).ToArray();
            var pocosReturned = logic.GetList(ids);

            pocosReturned.ForEach(p => p.CompletionPercent = 55);

            logic.Update(pocosReturned.ToArray());

            string json = JsonConvert.SerializeObject(pocos, Formatting.Indented);
            return Ok(json);
        }

        catch (Exception ex)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}
