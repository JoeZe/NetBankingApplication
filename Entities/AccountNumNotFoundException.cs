﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AccountNumNotFoundException: Exception
    {
        public AccountNumNotFoundException(string message): base(message)
        {

        }
    }
}
