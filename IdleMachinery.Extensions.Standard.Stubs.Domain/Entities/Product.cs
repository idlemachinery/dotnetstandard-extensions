using System.Collections.Generic;

namespace IdleMachinery.Extensions.Standard.Stubs.Domain
{
    public partial class Product : IReadOnly
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProducts { get; } = new HashSet<OrderProduct>();
    }
}
