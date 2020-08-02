using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Entities
{
    public class Advert : Entity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string ContactEmail { get; set; }

        public byte[] Photo { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
