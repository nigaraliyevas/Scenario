using AutoMapper;
using Scenario.Application.Dtos.CategoryDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null) throw new CustomException(404, "Null Exception");
            var isExist = await _unitOfWork.CategoryRepository.IsExist(x => x.CategoryName.ToLower() == categoryCreateDto.CategoryName.ToLower());
            if (isExist) throw new CustomException(400, "The Category Is Exist");
            var newCategory = _mapper.Map<Category>(categoryCreateDto);
            await _unitOfWork.CategoryRepository.Create(newCategory);
            _unitOfWork.Commit();
            return newCategory.Id;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var category = await _unitOfWork.CategoryRepository.GetEntity(x => x.Id == id);
            if (category == null) throw new CustomException(404, "Not Found");
            await _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();
            return category.Id;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll(null, "PlotCategories");
            if (categories == null) throw new CustomException(404, "Not Found");
            var categoryDto = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDto;
        }

        public async Task<CategoryDto> GetById(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var caregory = await _unitOfWork.CategoryRepository.GetEntity(x => x.Id == id, "PlotCategories");

            if (caregory == null) throw new CustomException(404, "Not Found");
            var caregoryDto = _mapper.Map<CategoryDto>(caregory);
            return caregoryDto;
        }

        public async Task<int> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null) throw new CustomException(404, "Null Exception");
            var existCategory = await _unitOfWork.CategoryRepository.GetEntity(x => x.Id == categoryUpdateDto.Id && !(x.CategoryName.ToLower() == categoryUpdateDto.CategoryName));
            if (existCategory == null) throw new CustomException(404, "Not Found");
            existCategory.CategoryName = categoryUpdateDto.CategoryName;
            existCategory.UpdatedDate = DateTime.Now;
            await _unitOfWork.CategoryRepository.Update(existCategory);
            _unitOfWork.Commit();
            return existCategory.Id;
        }
    }
}
