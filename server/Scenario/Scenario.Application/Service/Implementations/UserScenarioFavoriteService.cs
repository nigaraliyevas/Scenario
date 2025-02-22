using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;
namespace Scenario.Application.Service.Implementations
{
    public class UserScenarioFavoriteService : IUserScenarioFavoriteService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserScenarioFavoriteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AddToFavorites(int scenarioId, string userId)
        {

            // Əvvəlcədən favorit olub-olmadığını yoxla
            var existingFavorite = await _unitOfWork.UserScenarioFavoriteRepository
                .GetEntity(x => x.UserId == Guid.Parse(userId) && x.PlotId == scenarioId);

            if (existingFavorite != null)
            {
                return "Ssenari artıq favoritlərdədir.";
            }

            // Əgər favorit deyilsə, əlavə et
            var userLikedScenario = new UserScenarioFavorite
            {
                UserId = Guid.Parse(userId),
                PlotId = scenarioId
            };

            await _unitOfWork.UserScenarioFavoriteRepository.Create(userLikedScenario);
            _unitOfWork.Commit();

            return "Ssenari favoritlərə əlavə edildi.";
        }

        public async Task<List<Plot>> GetLikedScenariosByUserId(string userId)
        {
            // İstifadəçinin ID-sinə uyğun bütün favorit ssenariləri əldə et
            var likedScenarioIds = await _unitOfWork.UserScenarioFavoriteRepository
    .GetAllAsQeuryable(x => x.UserId == Guid.Parse(userId))
    .Select(x => x.PlotId)
    .ToListAsync();

            // PlotId-lərini istifadə edərək ssenariləri tap
            var likedScenarios = await _unitOfWork.PlotRepository
                .GetList(x => likedScenarioIds.Contains(x.Id))
                .ToListAsync();

            return likedScenarios;
        }

        public async Task<string> RemoveFromFavorites(int scenarioId, string userId)
        {
            // Əvvəlcədən favoritdə olub-olmadığını yoxla
            var existingFavorite = await _unitOfWork.UserScenarioFavoriteRepository
                .GetEntity(x => x.UserId == Guid.Parse(userId) && x.PlotId == scenarioId);

            if (existingFavorite == null)
            {
                return "Ssenari favoritlərdə deyil.";
            }

            // Əgər favoritdədirsə, sil
            await _unitOfWork.UserScenarioFavoriteRepository.Delete(existingFavorite);
            _unitOfWork.Commit();

            return "Ssenari favoritlərdən silindi.";
        }



    }
}
