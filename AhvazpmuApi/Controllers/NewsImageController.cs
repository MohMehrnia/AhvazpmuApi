using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AhvazpmuApi.Entities;
using AhvazpmuApi.Repositories;

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
        [HttpGet("{id}")]
        public IActionResult GetNewsImage(Guid id)
        {
            return Ok(_Repository.GetSingle(q=>q.tbNewsID==id).fldSmallImage);
        }
    }
}