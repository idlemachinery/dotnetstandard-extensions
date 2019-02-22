using System.Collections.Generic;

namespace IdleMachinery.Extensions.Standard.Stubs.Domain
{
    public partial class Customer : IAudited
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Audit Audit { get; set; } = new Audit();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}