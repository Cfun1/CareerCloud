using CareerCloud.DataTransfer;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebApp.API;

public interface IApiController<TDto> where TDto : class, IDto
{
    ActionResult<ICollection<TDto>> GetAll();

    ActionResult<TDto> GetSingle(Guid id);

    ActionResult<ICollection<TDto>> Create(TDto[] pocoDtos);

    ActionResult<ICollection<TDto>> Update(TDto[] pocoDtos);

    //don't return deleted object: https://datatracker.ietf.org/doc/html/rfc7231
    ActionResult Delete(TDto[] pocoDtos);
}