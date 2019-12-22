﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Model
{
    public class Requestdata
    {
        public string ParamedicName { get; set; }
        public string AuthorityLocation { get; set; }
        public Posationdata ParamedicCoordinates { get; set; }
        public double TimeInSeconds { get; set; }
        public double Distance { get; set; }
    }
}