using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
/**
 * Authors: Rutvik Patel, Ritesh Patel, Himanshu Patel and  Parvati Patel
 * Name: Cart.cs
 * Description: This is cart file which hold all of our items and the date they were created.
 */
namespace E_commerce.Models
{
    public class Cart
    {
        [Key]// specifics which key to be primary key
        public virtual int RecordId { get; set; }
        public virtual string CartId { get; set; }
        public virtual int IID { get; set; }
        public virtual int Count { get; set; }
        public virtual System.DateTime DateCreated { get; set; }
        public  virtual Item Item { get; set; }
    }
}