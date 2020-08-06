using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddComment(Comment comment, int advertId)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            var adverts = _unitOfWork.Adverts.GetAll();
            var advert = adverts.FirstOrDefault(
                a => a.Id == advertId);

            var newComment = new Comment()
            {
                AdvertComment = comment.AdvertComment,
            };

            if (advert != null)
            {
                newComment.AdvertId = advert.Id;
                newComment.Advert = advert;
            }

            _unitOfWork.Comments.Create(newComment);

            await _unitOfWork.SaveChangesAsync();
        }

        public IEnumerable<Comment> GetUserComments(int advertId)
        {
            var advertComments = _unitOfWork.Comments.GetByCondition(a => a.AdvertId == advertId);

            return advertComments.ToList();
        }
    }
}
