﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
