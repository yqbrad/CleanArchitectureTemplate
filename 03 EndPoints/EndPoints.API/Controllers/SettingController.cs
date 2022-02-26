using Framework.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using YQB.Infra.Service.ServiceInfos;

namespace YQB.EndPoints.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SettingController : BaseController
    {
        [Route("")]
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default")]
        [ProducesResponseType(typeof(ServiceInfo), 200)]
        public IActionResult Get()
        {
            var info = new ServiceInfo();
            var ip = SystemManager.GetLocalIpAddress();
            var mac = SystemManager.GetMacByIp(ip);

            info.Mac = mac;
            info.Name = Config.Id;
            info.Ip = Request.Scheme + "://" + ip;
            info.Port = Request.Host.Port ?? 0;
            info.Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            info.BuildType = typeof(SettingController)
                .Assembly
                .GetCustomAttribute<AssemblyConfigurationAttribute>()?
                .Configuration;
            return new ObjectResult(info);
        }
    }
}