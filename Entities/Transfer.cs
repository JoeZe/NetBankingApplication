using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transfer: ITransaction
    {
        public double amount { get; set; }
        public int tranId { get; set; }
        public Account account { get; set; }


    }

}
