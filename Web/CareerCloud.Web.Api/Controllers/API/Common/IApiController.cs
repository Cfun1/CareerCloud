using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebApp.API;

public interface IApiController<TDto> where TDto : new()
{
    ActionResult<string> GetAll();

    ActionResult<string> GetSingle(Guid id);

    ActionResult<string> Add(TDto[] pocoDtos);

    ActionResult<string> Update(TDto[] pocoDtos);

    //don't return deleted object: https://datatracker.ietf.org/doc/html/rfc7231
    ActionResult Remove(TDto[] pocoDtos);
}