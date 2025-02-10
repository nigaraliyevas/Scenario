using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Scenario.Application.Dtos.CommentDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public CommentService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        //public Task<int> AddReplyToComment(int parentCommentId, CommentCreateDto commentCreateDto) //todo
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<CommentDto> Create(CommentCreateDto commentCreateDto)
        {
            if (commentCreateDto == null) throw new CustomException(404, "Null Exception");
            var user = await _userManager.FindByIdAsync(commentCreateDto.AppUserId);
            if (user == null) throw new CustomException(404, "Not Found");
            var newComment = _mapper.Map<Comment>(commentCreateDto);
            await _unitOfWork.CommentRepository.Create(newComment);
            _unitOfWork.Commit();
            return _mapper.Map<CommentDto>(newComment);
        }

        public async Task<int> Delete(int id)
        {
            if (id == null || id <= 0) throw new CustomException(404, "Null Exception");
            var existComment = await _unitOfWork.CommentRepository.GetEntity(x => x.Id == id);
            if (existComment == null) throw new CustomException(404, "Not Found");
            await _unitOfWork.CommentRepository.Delete(existComment);
            _unitOfWork.Commit();
            return existComment.Id;
        }

        public async Task<List<Comment>> GetAll()
        {
            var comments = await _unitOfWork.CommentRepository.GetAll(null, "AppUser");
            if (comments == null) throw new CustomException(404, "Null Exception");
            return comments;
        }

        public async Task<Comment> GetById(int id)
        {
            if (id == null || id <= 0) throw new CustomException(404, "Null Exception");
            var existComment = await _unitOfWork.CommentRepository.GetEntity(x => x.Id == id, "AppUser");
            if (existComment == null) throw new CustomException(404, "Not Found");
            return existComment;
        }

        public async Task<int> Update(CommentUpdateDto commentUpdateDto, int id)
        {
            if (id == null || id <= 0 || commentUpdateDto == null) throw new CustomException(404, "Null Exception");
            var existComment = await _unitOfWork.CommentRepository.GetEntity(x => x.Id == id, "AppUser");
            if (existComment == null) throw new CustomException(404, "Not Found");
            _mapper.Map<Comment>(commentUpdateDto);
            return existComment.Id;
        }
    }
}
