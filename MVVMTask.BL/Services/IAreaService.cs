using MVVMTask.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTask.BL.Services
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAreas();
    }
}
