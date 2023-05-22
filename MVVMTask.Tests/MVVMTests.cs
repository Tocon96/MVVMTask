using Microsoft.Extensions.Logging;
using MVVMTask.BL.Models;
using MVVMTask.BL.Services;

namespace MVVMTask.Tests
{
    public class Tests
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

            Assert.That(areaList.Count(), Is.Not.EqualTo(0));
        }
    }
}