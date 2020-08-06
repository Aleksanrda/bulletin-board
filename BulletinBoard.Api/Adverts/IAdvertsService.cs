using BulletinBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Adverts
{
    public interface IAdvertsService
    {
        Task AddEvent(Advert advert, string userId);

        Task Update(Advert advert, string userId);

        Task Delete(int id);

        Task<Advert> GetAdvertById(int id);

        IEnumerable<Advert> GetAdverts();

        IEnumerable<Advert> GetUserAdverts(string userId);
    }
}
