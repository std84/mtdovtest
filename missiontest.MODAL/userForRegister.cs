using System.ComponentModel.DataAnnotations;
namespace missiontest.MODAL
{
    public class userForRegister
    {
        [Required]
        public string Username {get; set;}
        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="password must be between 4 to 8 chars")]

        public string Password {get; set;}
        [Required]
        public string firstName {get; set;}
        [Required]
        public string lastName {get; set;}
        [Required]
        public int Age {get; set;}
        [Required]
        public string phone {get; set;}
        [Required]
        public string email {get; set;}
        [Required]
        public string sex {get; set;}

    }
}