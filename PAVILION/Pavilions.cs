//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAVILION
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pavilions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pavilions()
        {
            this.Rent = new HashSet<Rent>();
        }
    
        public string num_pavilion { get; set; }
        public int id_tc { get; set; }
        public int floar { get; set; }
        public string status { get; set; }
        public decimal s { get; set; }
        public decimal money { get; set; }
        public double coefficient { get; set; }
    
        public virtual TC TC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rent> Rent { get; set; }
    }
}
