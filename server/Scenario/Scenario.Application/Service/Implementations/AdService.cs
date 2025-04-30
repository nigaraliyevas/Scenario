using AutoMapper;
using Microsoft.AspNetCore.Http;
using Scenario.Application.Dtos.AdDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Extensions.Extension;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class AdService : IAdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AdService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Create(AdCreateDto adCreateDto)
        {
            if (string.IsNullOrWhiteSpace(adCreateDto.Image)) throw new CustomException(400, "Image is null");
            string image = null;
            if (!string.IsNullOrEmpty(adCreateDto.Image))
            {
                if (!adCreateDto.Image.CheckContentType("image"))
                    throw new CustomException(400, "The file has to be img");

                if (adCreateDto.Image.CheckSize(25600))
                    throw new CustomException(400, "The file is too large");

                image = await adCreateDto.Image.SaveFile("adImages", _httpContextAccessor);
            }

            var newAd = _mapper.Map<Ad>(adCreateDto);
            newAd.Image = image;
            await _unitOfWork.AdRepository.Create(newAd);
            _unitOfWork.Commit();
            return newAd.Id;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0) throw new CustomException(400, "Id is not right");
            var ad = await _unitOfWork.AdRepository.GetEntity(x => x.Id == id);
            if (ad == null) throw new CustomException(400, "Null Exception");
            await _unitOfWork.AdRepository.Delete(ad);
            _unitOfWork.Commit();
            return ad.Id;
        }

        public async Task<List<AdDto>> GetAll()
        {
            var ads = await _unitOfWork.AdRepository.GetAll();
            var adDto = _mapper.Map<List<AdDto>>(ads);
            return adDto;
        }

        public async Task<List<AdDto>> GetAll(int takeCount)
        {
            var ads = await _unitOfWork.AdRepository.GetAll();
            if (takeCount > 0)
                ads.Take(takeCount);
            var adDto = _mapper.Map<List<AdDto>>(ads);
            return adDto;
        }

        public async Task<AdDto> GetById(int id)
        {
            if (id <= 0) throw new CustomException(400, "Id is not right");
            var ad = await _unitOfWork.AdRepository.GetEntity(x => x.Id == id);
            if (ad == null) throw new CustomException(400, "Null Exception");
            var adDto = _mapper.Map<AdDto>(ad);
            return adDto;
        }

        public async Task<int> Update(AdUpdateDto adUpdateDto)
        {
            if (adUpdateDto.Id <= 0) throw new CustomException(400, "Id is not right");
            var ad = await _unitOfWork.AdRepository.GetEntity(x => x.Id == adUpdateDto.Id);
            if (ad == null) throw new CustomException(400, "Null Exception");

            if (string.IsNullOrWhiteSpace(adUpdateDto.Image)) throw new CustomException(400, "Image is null");
            string image = null;
            if (!string.IsNullOrEmpty(adUpdateDto.Image))
            {
                if (!adUpdateDto.Image.CheckContentType("image"))
                    throw new CustomException(400, "The file has to be img");

                if (adUpdateDto.Image.CheckSize(25600))
                    throw new CustomException(400, "The file is too large");

                image = await adUpdateDto.Image.SaveFile("adImages", _httpContextAccessor);

            }
            ad.Image = image;
            await _unitOfWork.AdRepository.Update(ad);
            _unitOfWork.Commit();
            return ad.Id;
        }
    }
}
