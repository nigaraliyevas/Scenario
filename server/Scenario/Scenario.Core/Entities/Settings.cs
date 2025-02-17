using Scenario.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Core.Entities
{
    public class Settings:BaseEntity
    {
        public string About { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Linkedin { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
    }
}
