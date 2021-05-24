//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LearnIT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Note
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Field Title is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters allowed")]
        [RegularExpression(@"^[A-Z�ӥ������a-z�󹜳����A-Z�ӥ������ , ' \- &quot;]*$", ErrorMessage = "Only letters, spaces, dashes, commas and quotes allowed")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field Category is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters allowed")]
        [RegularExpression(@"^[A-Z�ӥ������a-z�󹜳����A-Z�ӥ������ \-]*$", ErrorMessage = "Only letters, spaces and dashes allowed")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Field KeyWords is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters allowed")]
        [RegularExpression(@"^[A-Z�ӥ������a-z�󹜳����A-Z�ӥ������ # , . \-]*$", ErrorMessage = "Only letters, spaces, dashes, commas, dots and hashes allowed")]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = "Field Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field Link is required")]
        [StringLength(2000, ErrorMessage = "Max 2000 characters allowed")]
        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "Invalid link format")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Field Date is required")]
        public System.DateTime Date { get; set; }

        [Required(ErrorMessage = "Field Author is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters allowed")]
        [RegularExpression(@"^[A-Z�ӥ������]+[a-z�󹜳����A-Z�ӥ������ \-]*[A-Z�ӥ������]+[a-z�󹜳����A-Z�ӥ������ ]*$",
        ErrorMessage = "Author must start with uppercase letter and must contain only letters and dashes")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Field University is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters allowed")]
        [RegularExpression(@"^[A-Z�ӥ������]+[a-z�󹜳����A-Z�ӥ������ \-]*[A-Z�ӥ������]+[a-z�󹜳����A-Z�ӥ������ ]*$",
        ErrorMessage = "University must start with uppercase letter and must contain only letters and dashes")]
        public string University { get; set; }

        [Required(ErrorMessage = "Field Email is required")]
        [StringLength(320, ErrorMessage = "Max 320 characters allowed")]
        [RegularExpression(@"[a-z0-9!#$%&'\*\\\\+\\/=?^`{}|]{1}[a-z0-9!#$%&'\*\.\\\+\-\/=?^_`{}|]+@[a-z0-9.-]+\.[a-z]{2,4}$",
        ErrorMessage = "Email must contain only lowercase letters, @ and . ")]
        public string Email { get; set; }
    }
}
