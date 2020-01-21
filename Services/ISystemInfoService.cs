using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogUI.Services
{
    public interface ISystemInfoService
    {
        public Task<string> GetSystemInfo();
    }
}
