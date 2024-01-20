using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ServiceRequestApp
{
    public partial class Status
    {
        public Status()
        {
            Request = new HashSet<Request>();
        }

        public int IdStatus { get; set; }
        public string NameStatus { get; set; }

        public virtual ICollection<Request> Request { get; set; }
    }
}
