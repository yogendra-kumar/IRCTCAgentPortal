using System;
using System.Collections.Generic;


namespace Mpower.Models.ApplicationViewModel
{
    public class ApplicationErrorViewModel
    {
        public IEnumerable<ApplicationError> applicationError { get; set; }

        public Int64 totalCount { get; set; }
    }
}