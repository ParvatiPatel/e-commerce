using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**
 * Authors: Rutvik Patel, Ritesh Patel, Himanshu Patel and  Parvati Patel
 * Name: orderDetail.cs
 * Description: This file OrderDetails for displaying all the details in the complete page.
 */
namespace E_commerce.Models
{
    public class OrderDetail
    {
        public virtual int OrderDetailId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int IID { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}