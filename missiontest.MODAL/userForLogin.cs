using System.ComponentModel.DataAnnotations;

namespace missiontest.MODAL
{
    public class userForLogin
    {
                 [Required]
        public string Username{get; set;}
        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="password must be between 4 to 8 chars")]
        public string Password{get; set;}
    }
}