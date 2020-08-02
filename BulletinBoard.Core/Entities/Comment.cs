using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Entities
{
    public class Comment : Entity
    {
        public string AdvertComment { get; set; }

        public Advert Advert { get; set; }

        public int AdvertId { get; set; }
    }
}
