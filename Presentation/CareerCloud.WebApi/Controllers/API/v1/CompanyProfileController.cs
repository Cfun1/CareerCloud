using Asp.Versioning;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.WebAPI.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CareerCloud.WebAPI.Controllers;
[ApiController]

[ApiVersion("1.0")]
[Route("api/careercloud/[Controller]/v{version:apiVersion}")]
/// api/careercloud/CompanyProfile/v1
public /* partial */ class CompanyProfileController :
                     CareerCloudBaseController<CompanyProfilePoco,
                                                CompanyProfileLogic>
//,IApiController<CompanyProfilePoco>
{
    //public CompanyProfileController(IDataRepository<CompanyProfilePoco> companyProfileRepo) : base(companyProfileRepo)
    //{
    //}

    //todo: only needed for the test, DI workaround, replace with upper later
    public CompanyProfileController() : base() { }

    /// POST: api/careercloud/CompanyProfile/v1/Profile
    [HttpPost("Profile")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<CompanyProfilePoco>(StatusCodes.Status200OK)]
    [ProducesResponseType<CompanyProfilePoco>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco?[] pocos)
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

    /// GET: api/careercloud/CompanyProfile/v1/Profile
    [HttpGet("Profile")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<ICollection<CompanyProfilePoco>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult<ICollection<CompanyProfilePoco>> GetAll()
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            var apiResult = logic.GetAll();
            return Ok();
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /// GET: api/careercloud/CompanyProfile/v1/Profile/Id
    [HttpGet("Profile/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType<CompanyProfilePoco>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public ActionResult GetCompanyProfile(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest(id);

        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            var apiResult = logic.Get(id);

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

    /// DELETE: api/careercloud/CompanyProfile/v1/Profile
    [HttpDelete("Profile")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            logic.Delete(pocos);
            return Ok();
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    /*
     /// DELETE: api/careercloud/CompanyProfile/v1/Profile/Id
    [HttpDelete("Profile/{id}")]
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
            return Ok();
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
    */

    /// PUT: api/careercloud/CompanyProfile/v1/Profile
    [HttpPut("Profile")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
    {
        try
        {
            if (logic is null)
                throw new NullReferenceException(nameof(logic));

            if (pocos.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(pocos));

            logic.Update(pocos);

            return Ok();
        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}
