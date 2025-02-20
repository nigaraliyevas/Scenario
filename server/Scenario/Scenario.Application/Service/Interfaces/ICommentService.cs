using Scenario.Application.Dtos.CommentDtos;
using Scenario.Core.Entities;

namespace Scenario.Application.Service.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> Create(CommentCreateDto commentCreateDto);
        Task<int> AddReplyToComment(int parentCommentId, CommentCreateDto commentCreateDto);
        Task<int> Update(CommentUpdateDto commentUpdateDto, int id);
        Task<int> Delete(int id);
        Task<List<Comment>> GetAll();
        Task<Comment> GetById(int id);
    }
}
