using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Application.Dtos.AboutTestimonialDtos
{
    public class AboutTestimonialCreateDto
    {
        public string Header { get; set; }  
        public string Title { get; set; }  
        public IFormFile Image { get; set; }
    }
}
