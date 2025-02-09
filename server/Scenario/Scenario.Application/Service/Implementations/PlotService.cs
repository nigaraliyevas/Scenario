using AutoMapper;
using Scenario.Application.Dtos.ActorDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class PlotService : IPlotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlotService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(ActorCreateDto actorCreateDto)
        {
            //if (actorCreateDto == null) throw new CustomException(404, "Null Exception");
            //var newActor = _mapper.Map<Scenario>(actorCreateDto);
            //await _unitOfWork.PlotRepository.Create(newActor);
            //_unitOfWork.Commit();
            return 1;
        }

        public async Task<int> Delete(int id)
        {
            //if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            //var actor = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == id);
            //if (actor == null) throw new CustomException(404, "Not Found");
            //await _unitOfWork.PlotRepository.Delete(actor);
            //_unitOfWork.Commit();
            //return actor.Id;
            return 1;
        }

        public async Task<List<Plot>> GetAll()
        {
            var actors = await _unitOfWork.PlotRepository.GetAll();
            if (actors == null) throw new CustomException(404, "Not Found");
            return actors;
        }

        public async Task<List<Plot>> GetAllByMovieId(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var actors = await _unitOfWork.PlotRepository.GetAll(x => x.PlotCategories.Any(ma => ma.CategoryId == id), "MovieActors");
            if (actors == null) throw new CustomException(404, "Not Found");
            var actorDtos = actors.Select((object a) => new Plot
            {
                //Id = a.Id,
                //FullName = a.FullName,
            }).ToList();
            return actorDtos;
        }

        public async Task<Plot> GetById(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var actor = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == id);
            if (actor == null) throw new CustomException(404, "Not Found");
            return actor;
        }

        public async Task<int> Update(ActorUpdateDto actorUpdateDto, int id)
        {
            if (actorUpdateDto == null) throw new CustomException(404, "Null Exception");
            var existActor = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == id);
            if (existActor == null) throw new CustomException(404, "Not Found");
            existActor.Header = actorUpdateDto.FullName;
            existActor.UpdatedDate = DateTime.Now;
            await _unitOfWork.PlotRepository.Update(existActor);
            _unitOfWork.Commit();
            return existActor.Id;
        }
    }
}
