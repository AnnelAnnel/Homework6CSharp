using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKB.BLL.model
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public string fullname { get; set; }

        public DateTime dob { get; set; }

        public string iin { get; set; }

        public string phonenumber { get; set; }
        public List<Account> Accounts = new List<Account>();
    }
}
