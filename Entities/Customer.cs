using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Customer
    {
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string DOB { set; get; }
        public string Email { set; get; }
        public static int GenerateID=100;
        public List <Account> account { set; get; }

        public Customer()
        {
            this.Id = GenerateID++;
        }

        public Customer(string fn, string ln, string dob, string email)
        {
            this.Id = GenerateID++;
            this.FirstName = fn;
            this.LastName = ln;
            this.DOB = dob;
            this.Email = email;
        }
    }
}
