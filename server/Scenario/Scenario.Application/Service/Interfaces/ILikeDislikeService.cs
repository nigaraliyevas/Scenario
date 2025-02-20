using Scenario.Application.Dtos.ActorDtos;
using Scenario.Application.Dtos.LikeDislikeDtos;
using Scenario.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
