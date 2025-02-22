namespace Scenario.Application.Service.Interfaces
{
    public interface IUserScenarioFavoriteService
    {
        Task<string> AddToFavorites(int scenarioId, string userId);
        Task<string> RemoveFromFavorites(int scenarioId, string userId);
        Task<List<Plot>> GetLikedScenariosByUserId(string userId);
    }
}
