﻿namespace api_sylvainbreton.Services.Utilities
{
    public class PaginationDetails
    {
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

}
