﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Entity.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 10;
        //Auto-implementent property
        public int PageNumber { get; set; }

        //Full-property
        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        public string? OrderBy { get; set; }

        public string? Fields { get; set; }
    }
}
