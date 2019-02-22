using System.Collections.Generic;

namespace IdleMachinery.Extensions.Standard.Stubs.Domain
{
    public partial class Order : IAudited
    {
        public int Id { get; set; }
        public string Reference { get; set; }

        public Audit Audit { get; set; } = new Audit();

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
    }
}