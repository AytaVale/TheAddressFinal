﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAddress.DAL.Dtos
{
    public class PropertyDocumentDto : BaseDto
    {
        public int ProductId { get; set; }
        public string DocumentUrl { get; set; }
    }
}
