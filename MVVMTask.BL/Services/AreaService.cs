using Microsoft.Extensions.Logging;
using MVVMTask.BL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTask.BL.Services
{
    
    public class AreaService : IAreaService
    {
        private ILogger logger;
        public HttpClient ApiClient { get; set; }
        public AreaService(ILogger logger)
        {
            this.logger = logger;

            HttpClient client = new HttpClient();
            ApiClient = client;
        }

        public async Task<IEnumerable<Area>> GetAreas()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                ApiClient.BaseAddress = new Uri(appSettings["ApiBaseAddress"]);

                HttpResponseMessage response = await ApiClient.GetAsync(appSettings["AreaApiCall"]);
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
                logger.Error(ex.Message);
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
                logger.Error(ex.Message);
                throw;
            }
        }
    }
}
