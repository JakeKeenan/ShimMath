﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShimMath.DTO
{
    public class ReturnStatus
    {
        public bool IsSuccessful { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
