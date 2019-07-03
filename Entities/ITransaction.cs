using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface ITransaction
    {
        double amount { get; set; }
        int tranId { get; set; }
        Account account { get; set; }
        //DateTime date { get; set; }
        

    }
}
