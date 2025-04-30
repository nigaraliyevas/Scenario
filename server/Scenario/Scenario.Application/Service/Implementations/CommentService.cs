using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Scenario.Application.Dtos.CommentDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations;
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

        public Task<int> AddReplyToComment(int parentCommentId, CommentCreateDto commentCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CommentDto> Create(CommentCreateDto commentCreateDto)
        {
            if (commentCreateDto == null) throw new CustomException(404, "Null Exception");
            var user = await _userManager.FindByIdAsync(commentCreateDto.AppUserId);
            if (user == null) throw new CustomException(404, "Not Found");
            var newComment = _mapper.Map<Comment>(commentCreateDto);
            newComment.AppUserName = user.UserName;
            //if (commentCreateDto.ParentCommentId == 0)
            //    commentCreateDto.ParentCommentId = null;
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

        public async Task<List<CommentDto>> GetAll()
        {
            var comments = await _unitOfWork.CommentRepository.GetAll(null, "AppUser");
            if (comments == null) throw new CustomException(404, "Null Exception");
            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<List<Comment>> GetAllByChapterId(int id)
        {
            if (id == null || id <= 0) throw new CustomException(404, "Null Exception");
            var chapter = await _unitOfWork.ChapterRepository.GetEntity(x => x.Id == id, "Comments");
            if (chapter == null) throw new CustomException(404, "Not Found");
            return chapter.Comments;
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
