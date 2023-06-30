﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escrutinio.Models
{
    public enum LoginResult : int
    {
        Succeed = 1,
        InvalidUserNamePassword = 2,
        AccountLocked = 3
    }
}