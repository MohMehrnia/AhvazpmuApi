using AhvazpmuApi.Dtos;
using AhvazpmuApi.Entities;
using AhvazpmuApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Controllers
{
    [Produces("application/json")]
    [Route("api/News")]
    public class NewsController : Controller
    {
        private IRepository<News> _Repository;
        public NewsController(IRepository<News> Repository)
        {
            _Repository = Repository;
        }

        [HttpGet]
        public IActionResult GetAllNews()
        {
            return Ok(_Repository.GetAll().ToList().Select(x => Mapper.Map<NewsDto>(x)));
        }

        [HttpGet]
        [Route("{id}", Name = "GetSingleNews")]
        public IActionResult GetSingleNews(Guid id)
        {
            News newsRepo = _Repository.GetSingle(c => c.tbNewsID == id);
            if (newsRepo == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<NewsDto>(newsRepo));
        }

        [HttpPost]
        public IActionResult AddNews([FromBody] NewsDto newsDto)
        {
            News toAdd = Mapper.Map<News>(newsDto);
            _Repository.Add(toAdd);
            bool result = _Repository.Save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }
            return CreatedAtRoute("GetSingleNews", new { id = toAdd.tbNewsID }, Mapper.Map<NewsDto>(toAdd));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateNews(Guid id, [FromBody] NewsDto newsDto)
        {
            var existingNews = _Repository.GetSingle(q => q.tbNewsID == id);
            if (existingNews == null)
            {
                return NotFound();
            }

            Mapper.Map(newsDto, existingNews);
            _Repository.Update(existingNews);
            bool result = _Repository.Save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }
            return Ok(Mapper.Map<NewsDto>(existingNews));
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult PartialUpdateNews(Guid id,[FromBody] JsonPatchDocument<NewsDto> newsDtoPatchDoc)
        {
            if (newsDtoPatchDoc == null)
            {
                return BadRequest();
            }

            var existingNews = _Repository.GetSingle(q => q.tbNewsID == id);
            if (existingNews == null)
            {
                return NotFound();
            }

            var newsToPatch = Mapper.Map<NewsDto>(existingNews);
            newsDtoPatchDoc.ApplyTo(newsToPatch);
            Mapper.Map(newsToPatch, existingNews);
            _Repository.Update(existingNews);
            bool result = _Repository.Save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }
            return Ok(Mapper.Map<NewsDto>(existingNews));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveNews(Guid id)
        {
            var existingNews = _Repository.GetSingle(q => q.tbNewsID == id);
            if (existingNews == null)
            {
                return NotFound();
            }

            _Repository.Delete(existingNews);
            bool result = _Repository.Save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return NoContent();
        }
    }
}
