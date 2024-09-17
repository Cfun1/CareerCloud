using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebApp.API;

public class CareerCloudBaseController<Tpoco, Tlogic> : ControllerBase
    where Tpoco : IPoco, new()
    where Tlogic : BaseLogic<Tpoco>
{
    internal readonly Tlogic? logic;
    public CareerCloudBaseController(IDataRepository<Tpoco> applicantEducationRepo)
    {
        logic = (Tlogic)Activator.CreateInstance(typeof(Tlogic), applicantEducationRepo)!;
    }
}