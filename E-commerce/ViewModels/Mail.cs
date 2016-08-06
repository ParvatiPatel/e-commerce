using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/*
 * @File name : Mail Model
 * @Author : Ritesh, Rutvik, Parvati and Himanshu
 * @Website name :E-commerece()
 * @File description : This model use for contact us
 */
namespace E_commerce.ViewModels
{
    public class Mail
    {
        //property of mail model
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}