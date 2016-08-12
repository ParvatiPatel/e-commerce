namespace E_commerce.Models
{
 /**
 * Authors: Rutvik Patel, Ritesh Patel, Himanshu Patel and  Parvati Patel
 * Name: Item.cs
 * Description: This file is for every item.
 */
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IID { get; set; }
        [Display(Name ="Product Name")]
        public int PDID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public decimal Price { get; set; }

       
        [StringLength(100)]
        [Display(Name = "Product Image")]
        public string ThumbUrl { get; set; }

       
        [StringLength(100)]
        [Display(Name = "Product Image")]
        public string OriginalUrl { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Porm. Type")]
        public string Tag { get; set; }

        public virtual Product Product { get; set; }
    }
}
