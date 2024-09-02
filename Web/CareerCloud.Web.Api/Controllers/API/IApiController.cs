using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebApp.API;

public interface IApiController<TPoco> where TPoco : IPoco, new()
{
    ActionResult<string> GetAll();

    ActionResult<string> GetSingle(Guid id);

    ActionResult<string> Add(TPoco[] pocos);

    ActionResult<string> Update(TPoco[] pocos);

    ActionResult<string> Remove(TPoco[] pocos);

    //ActionResult<string> CallStoredProc(string name, params Tuple<string, string>[] parameters);
}