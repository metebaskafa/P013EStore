﻿using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P013EStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<Category> GetAsync(int id)
        {
            return await _service.GetCategoryByIncludeAsync(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<int> PostAsync([FromBody] Category value) // from query ?query string
        {
            await _service.AddAsync(value);
            return await _service.SaveAsync();
           
        }

        // PUT api/<CategoriesController>/5
        [HttpPut] //  [HttpPut("{id}")] orjinali
        public async Task<int> PutAsync([FromBody] Category value)
        {
            _service.Update(value);
            return await _service.SaveAsync();
           
        }

		// DELETE api/<CategoriesController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var kayit = _service.Find(id);
			if (kayit == null)
			{
				return NotFound();
			}
			_service.Delete(kayit);
			_service.Save();
			return Ok(kayit);
		}
	}
}
