using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Scenario.Application.Dtos.CommentDtos;
using Scenario.Application.Dtos.LikeDislikeDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Application.Service.Implementations
{
    public class LikeDislikeService : ILikeDislikeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LikeDislikeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<int> CreateDislike(LikeDislikeCreateDto likeDislikeCreateDto)
        {
            if (likeDislikeCreateDto == null) throw new CustomException(404, "Null Exception");
            var comment = await _unitOfWork.LikeDislikeRepository.GetEntity(x => x.Id == likeDislikeCreateDto.CommentId);
            if (comment == null)
                throw new CustomException(400, "Comment not found");

            // Əgər artıq like varsa, geri al (decrement), əks halda artır
            if (comment.DislikeCount > 0)
                comment.DislikeCount--;
            else
                comment.DislikeCount++;

            await _unitOfWork.LikeDislikeRepository.Update(comment);
            return comment.DislikeCount;
        }

        public async Task<int> CreateLike(LikeDislikeCreateDto likeDislikeCreateDto)
        {
            if (likeDislikeCreateDto == null) throw new CustomException(404, "Null Exception");
            var comment = await _unitOfWork.LikeDislikeRepository.GetEntity(x=> x.Id == likeDislikeCreateDto.CommentId);
            if (comment == null)
                throw new CustomException(400,"Comment not found");

            // Əgər artıq like varsa, geri al (decrement), əks halda artır
            if (comment.LikeCount > 0)
                comment.LikeCount--;
            else
                comment.LikeCount++;

            await _unitOfWork.LikeDislikeRepository.Update(comment);
            return comment.LikeCount;
        }

        

     

        //public Task<int> GetLikeDislikeCount()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
