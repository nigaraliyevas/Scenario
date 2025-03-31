using AutoMapper;
using Scenario.Application.Dtos.ScriptwriterDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;
using System.Text.RegularExpressions;


namespace Scenario.Application.Service.Implementations
{
    public class ScriptwriterService : IScriptwriterService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ScriptwriterService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Create(ScriptwriterCreateDto scriptwriterCreateDto)
        {
            if (scriptwriterCreateDto == null) throw new CustomException(404, "Null Exception");
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            Match match = regex.Match(scriptwriterCreateDto.Email);
            if (!(match.Success)) throw new CustomException(400, "The email stucture is wrong");
            var isExist = await _unitOfWork.ScriptwriterRepository.IsExist(x => x.Email.ToLower() == scriptwriterCreateDto.Email.ToLower() || x.Phone == scriptwriterCreateDto.Phone);
            if (isExist) throw new CustomException(400, "Email or Phone number already belongs to another scriptwriter");
            var newScriptwriter = _mapper.Map<Scriptwriter>(scriptwriterCreateDto);
            await _unitOfWork.ScriptwriterRepository.Create(newScriptwriter);
            _unitOfWork.Commit();
            return newScriptwriter.Id;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var scriptwriter = await _unitOfWork.ScriptwriterRepository.GetEntity(x => x.Id == id);
            if (scriptwriter == null) throw new CustomException(404, "Not Found");
            await _unitOfWork.ScriptwriterRepository.Delete(scriptwriter);
            _unitOfWork.Commit();
            return scriptwriter.Id;
        }

        public async Task<List<ScriptwriterDto>> GetAll()
        {
            var scriptwriters = await _unitOfWork.ScriptwriterRepository.GetAll(null, "");
            if (scriptwriters == null) throw new CustomException(404, "Not Found");
            var scriptwriterDto = _mapper.Map<List<ScriptwriterDto>>(scriptwriters);
            return scriptwriterDto;
        }

        public async Task<ScriptwriterDto> GetById(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var scriptwriter = await _unitOfWork.ScriptwriterRepository.GetEntity(x => x.Id == id, "PlotCategories");

            if (scriptwriter == null) throw new CustomException(404, "Not Found");
            var scriptwriterDto = _mapper.Map<ScriptwriterDto>(scriptwriter);
            return scriptwriterDto;
        }

        public async Task<int> Update(ScriptwriterUpdateDto scriptwriterUpdateDto)
        {
            if (scriptwriterUpdateDto == null) throw new CustomException(404, "Null Exception");
            var existScriptwriter = await _unitOfWork.ScriptwriterRepository.GetEntity(x => x.Id == scriptwriterUpdateDto.Id);
            if (existScriptwriter == null) throw new CustomException(404, "Not Found");
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            Match match = regex.Match(scriptwriterUpdateDto.Email);
            if (!(match.Success)) throw new CustomException(400, "The email stucture is wrong");
            var isExist = await _unitOfWork.ScriptwriterRepository.IsExist(x =>
                 (x.Email.ToLower() == scriptwriterUpdateDto.Email.ToLower() ||
                  x.Phone == existScriptwriter.Phone) &&
                    x.Id != existScriptwriter.Id);
            if (isExist) throw new CustomException(400, "Email or Phone number already belongs to another scriptwriter");
            existScriptwriter.UpdatedDate = DateTime.Now;
            await _unitOfWork.ScriptwriterRepository.Update(existScriptwriter);
            _unitOfWork.Commit();
            return existScriptwriter.Id;
        }
    }
}
