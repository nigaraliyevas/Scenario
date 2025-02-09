namespace Scenario.Application.Dtos.UserDtos
{
    public class UserGetDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? UserImg { get; set; }
        public int SubscriptionPlanId { get; set; }
    }
}
