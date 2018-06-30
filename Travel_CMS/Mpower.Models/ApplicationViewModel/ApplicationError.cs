using System;


namespace Mpower.Models.ApplicationViewModel
{
    public class ApplicationError
    {
        public Int64 id { get; set; }

        public Int64 applicationID { get; set; }

        public Int64 pageID { get; set; }

        public string errorType { get; set; }

        public string errorDescription { get; set; }

        public DateTime logDate { get; set; }
    }
}