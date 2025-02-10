using Scenario.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Core.Entities
{
    public class AboutTestimonial:BaseEntity
    {
        public string Header { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
    }
}
