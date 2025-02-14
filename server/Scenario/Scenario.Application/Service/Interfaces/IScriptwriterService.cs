using Scenario.Application.Dtos.ScriptwriterDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IScriptwriterService
    {
        Task<int> Create(ScriptwriterCreateDto scriptwriterCreateDto);
        Task<int> Update(ScriptwriterUpdateDto scriptwriterUpdateDto);
        Task<int> Delete(int id);
        Task<List<ScriptwriterDto>> GetAll();
        Task<ScriptwriterDto> GetById(int id);
    }
}