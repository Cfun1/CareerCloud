using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers.Common;

public class CareerCloudBaseController<Tpoco, Tlogic> : ControllerBase
    where Tpoco : IPoco, new()
    where Tlogic : BaseLogic<Tpoco>
{
    internal readonly Tlogic? logic;
    public CareerCloudBaseController(IDataRepository<Tpoco> applicantEducationRepo)
    {
        logic = (Tlogic)Activator.CreateInstance(typeof(Tlogic), applicantEducationRepo)!;
    }

    //todo: only needed for the test, DI workaround, delete after
    //note that this workaround only support EF (not ADO), ADO is supported levreaging ASP built-in DI
    /*public CareerCloudBaseController()
    {
        var repositoryInterface = typeof(EFGenericRepository<>).MakeGenericType(typeof(Tpoco));
        var repo = Activator.CreateInstance(repositoryInterface);

        logic = (Tlogic)Activator.CreateInstance(typeof(Tlogic), repo)!;
    }*/
}