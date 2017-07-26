using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Model
{
   public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int SoldCount { get; set; }
        public string Refer { get; set; }
    }
}
