using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ServiceRequestApp
{
    public partial class Request
    {
        public int IdRequest { get; set; }
        public int UserId { get; set; }
        public int TemaId { get; set; }
        public int ImportanceId { get; set; }
        public int StatusId { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateFinish { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public bool? DoneDuring { get; set; }

        public virtual Importance Importance { get; set; }
        public virtual Status Status { get; set; }
        public virtual Tema Tema { get; set; }
        public virtual User User { get; set; }
    }
}
