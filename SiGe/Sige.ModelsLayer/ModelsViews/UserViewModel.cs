using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SiGe
{
    public class UserViewModel
    {
        public List<PersonModel> personModels { get; set; }
        public int UserId { get; set; }
        public int personId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "El password no coincide")]
        public string ConfirmPassword { get; set; }
    }
}
