using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTask.BL.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PrecendentLevelId { get; set; }
        public int? LevelId { get; set; }
        public string? LevelName { get; set; }
        public bool IsEditable { get; set; }
    }
}
