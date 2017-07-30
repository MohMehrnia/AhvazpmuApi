using AhvazpmuApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Repositories
{
    public class NewsImageRepository : Repository<NewsImage, AhvazpmuDbContext>
    {
        public NewsImageRepository(AhvazpmuDbContext context) : base(context)
        {
        }
    }
}
