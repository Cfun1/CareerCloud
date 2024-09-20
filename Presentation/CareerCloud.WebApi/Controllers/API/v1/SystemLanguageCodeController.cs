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
/// api/careercloud/SystemLanguageCode/v1
public /* partial */ class SystemLanguageCodeController : ControllerBase
{
    internal readonly SystemLanguageCodeLogic? _logic;

    //public SystemLanguageCodeController(IDataRepository<SystemLanguageCodePoco> repo)
    //{
    //    _logic = new SystemLanguageCodeLogic(repo);
    //}

    public SystemLanguageCodeController()
    {
        var repo = new EFGenericRepository<SystemLanguageCodePoco>();
        _logic = new SystemLanguageCodeLogic(repo);
    }

    /// POST: api/careercloud/SystemLanguageCode/v1/LanguageCode/
    [HttpPost("LanguageCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<SystemLanguageCodePoco>(StatusCodes.Status200OK)]
    [ProducesResponseType<SystemLanguageCodePoco>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco?[] pocos)
    {
        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            if (pocos.ToList().Any(poco =>
               poco.LanguageID == string.Empty))
                throw new ArgumentNullException();

            _logic.Add(pocos);
            return Ok();
        }

        catch (AggregateException ex)
        {
            return BadRequest(ex.InnerExceptions);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// GET: api/careercloud/SystemLanguageCode/v1/LanguageCode/
    [HttpGet("LanguageCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ICollection<SystemLanguageCodePoco>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<ICollection<SystemLanguageCodePoco>> GetSystemLanguageCodes()
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

    /// GET: api/careercloud/SystemLanguageCode/v1/LanguageCode/{id}
    [HttpGet("LanguageCode/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<SystemLanguageCodePoco>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult GetSystemLanguageCode(string id)
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

            return Ok(apiResult);
        }

        catch (AggregateException ex)
        {
            return BadRequest(ex);
        }

        catch (NullReferenceException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// DELETE: api/careercloud/SystemLanguageCode/v1/LanguageCode/
    [HttpDelete("LanguageCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
    {
        try
        {
            if (_logic is null)
                throw new NullReferenceException(nameof(_logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            _logic.Delete(pocos);
            return Ok();
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /*
    /// DELETE: api/careercloud/SystemLanguageCode/v1/LanguageCode/{id}
    [HttpDelete("LanguageCode/{id}")]
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
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
    */

    /// PUT: api/careercloud/SystemLanguageCode/v1/LanguageCode/
    [HttpPut("LanguageCode")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
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
