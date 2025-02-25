﻿using Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ExtendedModels {
    public class BookExtended : Book {
        public string UserName { get; set; }
        public IEnumerable<BookExtended> MoreUserBooks { get; set; }
    }
}
