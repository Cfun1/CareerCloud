using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebApp.API;

public interface IApiController<TPoco> where TPoco : IPoco, new()
{
    ActionResult<string> GetAll();

    ActionResult<string> GetSingle(Guid id);

    ActionResult<string> Add(TPoco[] pocos);

    ActionResult<string> Update(TPoco[] pocos);

    //don't return deleted object: https://datatracker.ietf.org/doc/html/rfc7231
    ActionResult Remove(TPoco[] pocos);
}