﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Note
    {
        public int SequentianNumber { get; set; }
        public string Autor { get; set; }
        public int DateRelease { get; set; }
        

        public override string ToString()
        {
            return $"({Autor}, {DateRelease})";
        }
    }
}
