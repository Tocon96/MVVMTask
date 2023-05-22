using Microsoft.Extensions.Logging;
using MVVMTask.BL.Models;
using MVVMTask.BL.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MVVMTask.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MapJsonToArrayTest()
        {
            LoggerFactory factory = new LoggerFactory();
            ILogger<AreaService> areaServiceLogger = factory.CreateLogger<AreaService>();
            IAreaService Service = new AreaService(areaServiceLogger);

            var json = "[{\r\n    \"id\": 1,\r\n    \"nazwa\": \"Ceny\",\r\n    \"id-nadrzedny-element\": 727,\r\n    \"id-poziom\": 1,\r\n    \"nazwa-poziom\": \"Dziedzina\",\r\n    \"czy-zmienne\": false\r\n  },\r\n  {\r\n    \"id\": 4,\r\n    \"nazwa\": \"Budownictwo\",\r\n    \"id-nadrzedny-element\": 727,\r\n    \"id-poziom\": 1,\r\n    \"nazwa-poziom\": \"Dziedzina\",\r\n    \"czy-zmienne\": false\r\n  },\r\n  {\r\n    \"id\": 29,\r\n    \"nazwa\": \"Finanse publiczne\",\r\n    \"id-nadrzedny-element\": 727,\r\n    \"id-poziom\": 1,\r\n    \"nazwa-poziom\": \"Dziedzina\",\r\n    \"czy-zmienne\": false\r\n  }]";
            JsonSerializer serializer = new JsonSerializer();
            var deserialized = serializer.Deserialize(new JsonTextReader(new StringReader(json)));
            JArray array = (JArray)deserialized;

            IList<Area> areaList = Service.MapJsonToAreas(array);

            Assert.That(areaList[1].PrecendentLevelId, Is.EqualTo(727));
            Assert.That(areaList[0].LevelName, Is.EqualTo("Dziedzina"));
            Assert.That(areaList[2].LevelId, Is.EqualTo(1));
        }
    }
}