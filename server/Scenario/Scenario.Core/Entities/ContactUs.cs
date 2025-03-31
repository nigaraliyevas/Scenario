using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class ContactUs : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
