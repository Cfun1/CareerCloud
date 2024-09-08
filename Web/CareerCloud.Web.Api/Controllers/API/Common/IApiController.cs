using CareerCloud.DataTransfer;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebApp.API;

public interface IApiController<TDto> where TDto : class, IDto
{
    ActionResult<string> GetAll();

    ActionResult<string> GetSingle(Guid id);

    ActionResult<string> Create(TDto[] pocoDtos);

    ActionResult<string> Update(TDto[] pocoDtos);

    //don't return deleted object: https://datatracker.ietf.org/doc/html/rfc7231
    ActionResult Delete(TDto[] pocoDtos);
}