namespace E_commerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IID { get; set; }

        public int PDID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string ThumbUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string OriginalUrl { get; set; }

        [Required]
        [StringLength(10)]
        public string Tag { get; set; }

        public virtual Product Product { get; set; }
    }
}
