using Scenario.Application.Dtos.LikeDislikeDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface ILikeDislikeService
    {
        Task<int> CreateLike(LikeDislikeCreateDto likeDislikeCreateDto);        
        Task<int> CreateDislike(LikeDislikeCreateDto likeDislikeCreateDto);        
        
        //Task<int> GetLikeDislikeCount();
        
        //Task<Actor> GetById(int id);
    }
}
