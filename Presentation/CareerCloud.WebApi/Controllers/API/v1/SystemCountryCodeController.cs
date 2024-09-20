using Asp.Versioning;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CareerCloud.WebAPI.Controllers;
[ApiController]

[ApiVersion("1.0")]
[Route("api/careercloud/[Controller]/v{version:apiVersion}")]
/// api/careercloud/SystemCountryCode/v1
public /* partial */ class SystemCountryCodeController : ControllerBase
{
    internal readonly SystemCountryCodeLogic? _logic;

    //public SystemCountryCodeController(IDataRepository<SystemCountryCodePoco> repo)
    //{
    //    _logic = new SystemCountryCodeLogic(repo);
    //}

    //todo: only needed for the test, DI workaround, replace with upper later
    public SystemCountryCodeController()
    {
        var repo = new EFGenericRepository<SystemCountryCodePoco>();
        _logic = new SystemCountryCodeLogic(repo);
    }

    /// POST: api/careercloud/SystemCountryCode/v1/CountryCode
    [HttpPost("CountryCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<SystemCountryCodePoco>(StatusCodes.Status200OK)]
    [ProducesResponseType<SystemCountryCodePoco>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco?[] pocos)
    {
        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            if (pocos.ToList().Any(poco =>
               poco.Code == string.Empty))
                throw new ArgumentNullException();

            _logic.Add(pocos);
            return Ok();

            //return CreatedAtAction(nameof(GetSystemCountryCode), new { pocos.First()!.Code }, pocos);
        }

        catch (AggregateException ex)
        {
            return BadRequest(ex.InnerExceptions);
        }
        catch (Exception ex)
        {
            //resposne could be null here
            //Response.Headers?.TryAdd("Retry-After", "3m");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// GET: api/careercloud/SystemCountryCode/v1/CountryCode
    [HttpGet("CountryCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ICollection<SystemCountryCodePoco>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<ICollection<SystemCountryCodePoco>> GetSystemCountryCodes()
    {
        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            var apiResult = _logic.GetAll();

            return Ok(apiResult);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "3m");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// GET: api/careercloud/SystemCountryCode/v1/CountryCode/{id}
    [HttpGet("CountryCode/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<SystemCountryCodePoco>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    //public ActionResult<SystemCountryCodePoco> GetApplicantEducation(Guid id)
    public ActionResult GetSystemCountryCode(string id)
    {
        if (id == string.Empty)
            return BadRequest(id);

        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            var apiResult = _logic.Get(id);

            if (apiResult == null)
                return NotFound();
            //return NotFound($"The object with id={id} was not found");

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

    /// DELETE: api/careercloud/SystemCountryCode/v1/CountryCode
    [HttpDelete("CountryCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
    {
        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            _logic.Delete(pocos);
            return Ok();
            //return StatusCode(StatusCodes.Status202Accepted);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /*
    /// DELETE: api/careercloud/SystemCountryCode/v1/CountryCode/{id}
    [HttpDelete("CountryCode/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(string id)
    {
        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            if (id == string.Empty)
                throw new ArgumentException(nameof(id));

            var poco = _logic.Get(id);
            if (poco is null)
                return NotFound();

            _logic.Delete([poco]);
            return Ok();
            //return StatusCode(StatusCodes.Status202Accepted);
        }

        catch (Exception)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
            // return NotFound();
        }
    }
    */

    /// PUT: api/careercloud/SystemCountryCode/v1/CountryCode/
    [HttpPut("CountryCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
    {
        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            _logic.Update(pocos);

            return Ok();
        }

        catch (Exception ex)
        {
            Response.Headers?.TryAdd("Retry-After", "500");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}
