using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class ContactUs : BaseEntity
    {
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
