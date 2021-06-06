namespace LearnIT.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Note
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Field Title is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field Category is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters allowed")]
        [RegularExpression(@"^[A-ZÊÓ¥Œ£¯ÆÑa-zêó¹œ³¿ŸæñA-ZÊÓ¥Œ£¯ÆÑ \-]*$", ErrorMessage = "Only letters, spaces and dashes allowed")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Field KeyWords is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters allowed")]
        [RegularExpression(@"^[A-ZÊÓ¥Œ£¯ÆÑa-zêó¹œ³¿ŸæñA-ZÊÓ¥Œ£¯ÆÑ # , . + \-]*$", ErrorMessage = "Only letters, spaces, dashes, commas, dots and hashes allowed")]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = "Field Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field Link is required")]
        [StringLength(2000, ErrorMessage = "Max 2000 characters allowed")]
        [RegularExpression(@"^http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*\(\),]|(?:%[0-9a-fA-F][0-9a-fA-F]))+$", ErrorMessage = "Invalid link format - it must start with http(s)://")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Field Date is required")]
        public System.DateTime Date { get; set; }

        [Required(ErrorMessage = "Field Author is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters allowed")]
        [RegularExpression(@"^[A-ZÊÓ¥Œ£¯ÆÑ]{1}[a-zêó¹œ³¿Ÿæñ ]+[A-ZÊÓ¥Œ£¯ÆÑ]+[a-zêó¹œ³¿Ÿæñ \-]+[A-ZÊÓ¥Œ£¯ÆÑ]*[a-zêó¹œ³¿ŸæñA-ZÊÓ¥Œ£¯ÆÑ]*$",
        ErrorMessage = "Author must start with uppercase letter and must contain only letters and optionally dash")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Field University is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters allowed")]
        [RegularExpression(@"^[A-ZÊÓ¥Œ£¯ÆÑ]+[a-zêó¹œ³¿ŸæñA-ZÊÓ¥Œ£¯ÆÑ \- . ,]*$",
        ErrorMessage = "University must start with uppercase letter and must contain only letters, dashes, dots and commas")]
        public string University { get; set; }

        [Required(ErrorMessage = "Field Email is required")]
        [StringLength(320, ErrorMessage = "Max 320 characters allowed")]
        [RegularExpression(@"[a-z0-9!#$%&'\*\\\\+\\/=?^`{}|]{1}[a-z0-9!#$%&'\*\.\\\+\-\/=?^_`{}|]+@[a-z0-9.-]+\.[a-z]{2,4}$",
        ErrorMessage = "Email must contain only lowercase letters, @ and . ")]
        public string Email { get; set; }
    }
}
