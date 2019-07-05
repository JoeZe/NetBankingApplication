using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transaction
    {
        public double amount { get; set; }
        public static int tranId = 1;
        public Account account { get; set; }
        public DateTime date { get; set; }
        public string TranscationType{ get; set; }

        public Transaction()
        {
            tranId++;
        }
    }
}
