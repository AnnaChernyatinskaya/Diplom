using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ServiceRequestApp
{
    public partial class Importance
    {
        public Importance()
        {
            Request = new HashSet<Request>();
        }

        public int IdImportance { get; set; }
        public string NameImportance { get; set; }

        public virtual ICollection<Request> Request { get; set; }
    }
}
