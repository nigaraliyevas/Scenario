﻿using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.CategoryDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _categoryService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            return Ok(await _categoryService.Create(categoryCreateDto));
        }

        //[Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            return Ok(await _categoryService.Update(categoryUpdateDto));
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _categoryService.Delete(id));
        }
    }
}
