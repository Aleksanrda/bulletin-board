using BulletinBoard.Api.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoard.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentsService _commentsService;

        public CommentController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

    }
}