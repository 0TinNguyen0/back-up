﻿using SV20T1080053.DomainModels;

namespace SV20T1080053.Web.Models
{
    public class PaginationSearchCategory : PaginationSearchBaseResult
    {
        public IList<Category> Data { get; set; }
    }
}
