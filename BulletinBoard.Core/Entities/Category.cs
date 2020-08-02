using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Entities
{
    public class Category : Entity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public byte[] CategoryPhoto { get; set; }

        public ICollection<Advert> Adverts { get; set; }
    }
}
