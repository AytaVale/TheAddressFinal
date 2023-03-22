using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAddress.DAL.DBModel
{
    public class Basket : BaseEntity
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public int UserId { get; set; }
    }
}
