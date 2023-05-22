using Microsoft.Extensions.Logging;
using MVVMTask.BL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTask.BL.Services
{
    
    public class AreaService : IAreaService
    {
        private ILogger<AreaService> areaServiceLogger;
        public HttpClient ApiClient { get; set; }
        public AreaService(ILogger<AreaService> logger)
        {
            areaServiceLogger = logger;

            HttpClient client = new HttpClient();
            ApiClient = client;
            ApiClient.BaseAddress = new Uri("https://api-dbw.stat.gov.pl/api/1.1.0/");
        }

        public async Task<IEnumerable<Area>> GetAreas()
        {
            try
            {
                HttpResponseMessage response = await ApiClient.GetAsync("area/area-area");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JsonSerializer serializer = new JsonSerializer();
                var deserialized = serializer.Deserialize(new JsonTextReader(new StringReader(responseBody)));
                JArray array = (JArray)deserialized;

                IList<Area> areaList = MapJsonToAreas(array);

                return areaList;
            }
            catch(Exception ex)
            {
                areaServiceLogger.LogError(ex.Message);
                throw;
            }
        }

        public IList<Area> MapJsonToAreas(JArray areaArray)
        {
            try
            {
                IList<Area> areaList = new List<Area>();
                foreach (JObject area in areaArray)
                {
                    Area newArea = new Area();
                    newArea.Id = area["id"].ToObject<int>();
                    newArea.Name = area["nazwa"].ToString();
                    if (area["id-nadrzedny-element"] != null)
                    {
                        newArea.PrecendentLevelId = area["id-nadrzedny-element"].ToObject<int>();
                    }
                    else
                    {
                        newArea.PrecendentLevelId = null;
                    }
                    newArea.LevelId = area["id-poziom"].ToObject<int>();
                    newArea.LevelName = area["nazwa-poziom"].ToString();
                    newArea.IsEditable = area["czy-zmienne"].ToObject<bool>();

                    areaList.Add(newArea);
                }

                return areaList;
            }
            catch( Exception ex )
            {
                areaServiceLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
