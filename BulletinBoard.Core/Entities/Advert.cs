using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BulletinBoard.Core.Entities
{
    public class Advert : Entity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Place { get; set; }

        [Required]
        public string ContactEmail { get; set; }

        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase Photo { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
