using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCatalogUI.Services;

namespace ProductCatalogUI.Controllers
{
    public class SystemInfoController : Controller
    {
        private readonly ISystemInfoService _service;

        public SystemInfoController(ISystemInfoService service)
        {
            _service = service;
        }

        // GET: SystemInfo
        [HttpGet]
        public ActionResult Index()
        {
            dynamic info = JsonConvert.DeserializeObject<object>(_service.GetSystemInfo().Result);

            return View(model: info);
        }

    }
}
