using AhvazpmuApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Repositories
{
    public class NewsRepository : Repository<News, AhvazpmuDbContext>
    {
        public NewsRepository(AhvazpmuDbContext context) : base(context)
        {
            
        }
    }
}
