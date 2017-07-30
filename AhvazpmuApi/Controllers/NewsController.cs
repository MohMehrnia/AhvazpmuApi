using AhvazpmuApi.Dtos;
using AhvazpmuApi.Entities;
using AhvazpmuApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Controllers
{
    [Produces("application/json")]
    [Route("api/News")]
    public class NewsController:Controller
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
    }
}
