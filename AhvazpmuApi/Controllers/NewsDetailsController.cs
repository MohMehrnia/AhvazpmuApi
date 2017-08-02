using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AhvazpmuApi.Repositories;
using AhvazpmuApi.Entities;
using AutoMapper;
using AhvazpmuApi.Dtos;

namespace AhvazpmuApi.Controllers
{
    [Produces("application/json")]
    [Route("NewsDetails")]
    public class NewsDetailsController : Controller
    {
        private IRepository<News> _Repository;
        public NewsDetailsController(IRepository<News> Repository)
        {
            _Repository = Repository;
        }

        [HttpGet]
        [Route("{id}", Name = "GetSingleNewsDetails")]
        public IActionResult GetSingleNews(Guid id)
        {
            News newsRepo = _Repository.GetSingle(c => c.tbNewsID == id);
            if (newsRepo == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<NewsDetailDto>(newsRepo));
        }
    }
}