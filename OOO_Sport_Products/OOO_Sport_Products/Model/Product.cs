//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOO_Sport_Products.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }
    
        public string ProductArticle { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitId { get; set; }
        public double ProductCost { get; set; }
        public int ProductDiscountMax { get; set; }
        public int ProductManufacturerId { get; set; }
        public int ProductProviderId { get; set; }
        public int ProductCutegoryId { get; set; }
        public int ProductDiscountCurrent { get; set; }
        public int ProductCountStock { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPhoto { get; set; }
    
        public virtual Cutegory Cutegory { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Unit Unit { get; set; }
    }
}