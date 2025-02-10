using AutoMapper;
using Scenario.Application.Dtos.SettingsDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.DataAccess.Implementations.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Application.Service.Implementations
{
    public class SettingsService : ISettingsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SettingsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<int> Create()
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<SettingsDto> GetSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(SettingsUpdateDto settingsUpdateDto)
        {
            var settings = await _unitOfWork.SettingsRepository.GetEntity();
            if (settings == null) throw new CustomException(404, "Not Found");


            if (settingsUpdateDto.About != null) settings.About = settingsUpdateDto.About;
            if (settingsUpdateDto.Logo != null) settings.Logo = settingsUpdateDto.Logo;
            if (settingsUpdateDto.Email != null) settings.Email = settingsUpdateDto.Email;
            if (settingsUpdateDto.PhoneNumber != null) settings.PhoneNumber = settingsUpdateDto.PhoneNumber;
            if (settingsUpdateDto.Instagram != null) settings.Instagram = settingsUpdateDto.Instagram;
            if (settingsUpdateDto.Telegram != null) settings.Telegram = settingsUpdateDto.Telegram;

            _unitOfWork.Commit();
            return settings.Id;
        }
    }
}
