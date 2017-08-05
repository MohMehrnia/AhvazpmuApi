using AhvazpmuApi.Dtos;
using AhvazpmuApi.Entities;
using AhvazpmuApi.QueryParameters;
using AhvazpmuApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Controllers
{
    [Produces("application/json")]
    [Route("News")]
    public class NewsController : Controller
    {
        private IRepository<News> _Repository;
        public NewsController(IRepository<News> Repository)
        {
            _Repository = Repository;
        }

        [HttpGet]
        public IActionResult GetAllNews(NewsQueryParameters newsQueryParameters)
        {
            Guid[] NewsGroupIDs = new Guid[] { new Guid("90AF0708-3307-42FC-99A4-666127BAD951"),
                                               new Guid("E5945A67-094F-4270-A601-ABB790D38C7B"),
                                               new Guid("95F3AF38-19D5-4042-902B-C0127AE9DE89") };
            Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(new { totalCount = _Repository.Count() }));
            return Ok(_Repository.GetAll(newsQueryParameters, 
                                         q=>Convert.ToDateTime(q.fldRegisterDate), 
                                         q=>NewsGroupIDs.Contains(q.tbNewsGroupID)).ToList().Select(x => Mapper.Map<NewsDto>(x)));
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
            if (newsDto==null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            newsDtoPatchDoc.ApplyTo(newsToPatch, ModelState);

            TryValidateModel(newsDtoPatchDoc);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
