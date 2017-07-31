using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AhvazpmuApi.Entities;
using AhvazpmuApi.Repositories;
using AhvazpmuApi.Dtos;
using AutoMapper;

namespace AhvazpmuApi.Controllers
{
    [Produces("application/json")]
    [Route("api/NewsImage")]
    public class NewsImageController : Controller
    {
        private IRepository<NewsImage> _Repository;
        public NewsImageController(IRepository<NewsImage> Repository)
        {
            _Repository = Repository;
        }

        [HttpGet]
        [Route("{id}", Name = "GetSingleNewsImage")]
        public IActionResult GetNewsImage(Guid id)
        {
            NewsImageDto newsImageRepo = Mapper.Map<NewsImageDto>(_Repository.GetSingle(q => q.tbNewsID == id));
            if (newsImageRepo == null)
            {
                return NotFound();
            }
            return File(newsImageRepo.fldSmallImage,"image/jpeg");
        }

        [HttpPost]
        public IActionResult AddNewsImage([FromBody] NewsImageDto newsImage)
        {
            NewsImage toAdd = Mapper.Map<NewsImage>(newsImage);
            _Repository.Add(toAdd);
            bool result = _Repository.Save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }
            //return Ok(Mapper.Map<NewsImageDto>(toAdd));
            return CreatedAtRoute("GetSingleNewsImage", new { id = toAdd.tbNewsID }, Mapper.Map<NewsImageDto>(toAdd));
        }
    }
}