using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Adverts
{
    public class AdvertsService : IAdvertsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdvertsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddEvent(Advert advert, string userId)
        {
            if (advert == null)
            {
                throw new ArgumentNullException(nameof(advert));
            }

            var users = _unitOfWork.Users.GetAll();
            var user = users.FirstOrDefault(
                u => u.Id == userId);

            var newAdvert = new Advert()
            {
                Title = advert.Title,
                Description = advert.Description,
                Place = advert.Place,
                ContactEmail = advert.ContactEmail,
                Category = advert.Category,
                ImagePath = advert.ImagePath,
                Photo = advert.Photo,
            };

            if (user != null)
            {
                newAdvert.UserId = user.Id;
                newAdvert.User = user;
            }

            _unitOfWork.Adverts.Create(newAdvert);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Advert advert, string userId)
        {
            if (advert == null)
            {
                throw new ArgumentNullException(nameof(advert));
            }

            var users = _unitOfWork.Users.GetAll();
            var user = users.FirstOrDefault(
                u => u.Id == userId);

            var adverts = _unitOfWork.Adverts.GetAll();
            var editAdvert = adverts.FirstOrDefault(
                currentAdvert => currentAdvert.Id == advert.Id);

            if (editAdvert != null)
            {
                editAdvert.Title = advert.Title;
                editAdvert.Description = advert.Description;
                editAdvert.Place = advert.Place;
                editAdvert.ContactEmail = advert.ContactEmail;
                editAdvert.Category = advert.Category;
            }

            if (user != null)
            {
                editAdvert.UserId = userId;
                editAdvert.User = user;
            }

            _unitOfWork.Adverts.Update(editAdvert);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var advert = await _unitOfWork.Adverts.GetByID(id);

            if (advert != null)
            {
                _unitOfWork.Adverts.Delete(advert);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Advert> GetAdvertById(int id)
        {
            var advert = await _unitOfWork.Adverts.GetByID(id);

            return advert;
        }

        public IEnumerable<Advert> GetAdverts()
        {
            var adverts = _unitOfWork.Adverts.GetAll();

            return adverts;
        }

        public IEnumerable<Advert> GetUserAdverts(string userId)
        {
            var userAdverts = _unitOfWork.Adverts.GetByCondition(a => a.UserId == userId);

            return userAdverts.ToList();
        }
    }
}
