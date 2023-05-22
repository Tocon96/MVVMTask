using MVVMTask.BL.Models;
using MVVMTask.BL.Services;

namespace MVVM.Tests
{
    public class ApiTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAreasTest()
        {
            LoggerFactory factory = new LoggerFactory();
            ILogger<AreaService> areaServiceLogger = factory.CreateLogger<AreaService>();
            IAreaService Service = new AreaService(areaServiceLogger);
            IEnumerable<Area> areaList = Task.Run(async () => await Service.GetAreas()).Result;
        }
    }
}