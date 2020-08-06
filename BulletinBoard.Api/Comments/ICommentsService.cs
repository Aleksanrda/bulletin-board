using BulletinBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Comments
{
    public interface ICommentsService
    {
        Task AddComment(Comment comment, int advertId);

        IEnumerable<Comment> GetUserComments(int advertId);
    }
}
