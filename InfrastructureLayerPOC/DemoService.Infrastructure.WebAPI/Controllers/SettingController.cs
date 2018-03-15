namespace DemoService.Infrastructure.WebAPI.Controllers
{
    using Contract;
    using Microsoft.Practices.Unity.Configuration;
    using Unity;
    using System.Web.Http;
    using Models;

    [RoutePrefix("setting")]
    public class VersionedSettingController : ApiController
    {
        /// <summary>
        /// The Unity container
        /// </summary>
        private static IUnityContainer _unityContainer;

        /// <summary>
        /// The demonstration service implementation
        /// </summary>
        private readonly IDemonstrationService _demonstrationServiceImplementation;

        public VersionedSettingController()
        {
            //Bootstrap from Unity
            if(_unityContainer == null)
                _unityContainer = new UnityContainer().LoadConfiguration(); //Uses the default Unity section "unity" (c.f. https://msdn.microsoft.com/en-us/library/ff660935(v=pandp.20).aspx)

            _demonstrationServiceImplementation = _unityContainer.Resolve<IDemonstrationService>("demonstrationServiceImpl");
        }

        [HttpGet]
        [Route("latest")]
        public string Get()
        {
            return _demonstrationServiceImplementation.GetLatestVersionedSetting();
        }

        [HttpGet]
        [Route("version/{version:int}")]
        public string Get(int version)
        {
            return _demonstrationServiceImplementation.GetVersionedSetting(version);
        }

        [HttpPost]
        [Route("add")]
        public void Post([FromBody]string setting)
        {
            _demonstrationServiceImplementation.AddVersionedSetting(setting);
        }

        [HttpPut]
        [Route("update")]
        public void Put([FromBody]VersionedSetting updatedSetting)
        {
            _demonstrationServiceImplementation.UpdateVersionedSetting(updatedSetting.Version, updatedSetting.Setting);
        }

        [HttpDelete]
        [Route("remove/{version:int}")]
        public void Delete(int version)
        {
            _demonstrationServiceImplementation.RemoveVersionedSetting(version);
        }
    }
}