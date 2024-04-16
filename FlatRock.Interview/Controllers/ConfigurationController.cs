using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _conf;

        public ConfigurationController(IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPost]
        public IActionResult Configure(bool captureVideos, bool capturePhotos, int captureFrequency)
        {
            var confSetting = new RecorderConfigurationModel { VideoCapture = captureVideos, CapturePhoto = capturePhotos, CaptureFrequency = captureFrequency };

            string json = System.IO.File.ReadAllText("Configuration/RecorderConfiguration.json");
            json = JsonConvert.SerializeObject(confSetting, Formatting.Indented);
            System.IO.File.WriteAllText("Configuration/RecorderConfiguration.json", json);

            return Ok(confSetting);
        }

        [HttpGet] 
        public IActionResult Get() 
        {
            string json = System.IO.File.ReadAllText("Configuration/RecorderConfiguration.json");

            return Ok(JsonConvert.DeserializeObject<RecorderConfigurationModel>(json));
        }
    }
}
