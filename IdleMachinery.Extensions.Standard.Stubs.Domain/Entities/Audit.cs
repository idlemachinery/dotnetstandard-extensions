using System;

namespace IdleMachinery.Extensions.Standard.Stubs.Domain
{
    public partial class Audit
    {
        public Audit()
        {
        }
        public Audit(string user, DateTime created, DateTime updated)
        {
            User = user;
            Created = created;
            Updated = updated;
        }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string User { get; set; }
    }
}
