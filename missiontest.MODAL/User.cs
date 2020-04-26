using System.Collections.Generic;
namespace missiontest.MODAL
{
    public class User
    {
         public int Id{ get; set;}
        public string Username { get; set; }
         public byte[] PasswordHash{ get; set;}
        public byte[] PasswordSalt{ get; set;}
        public string firstName{ get; set;}
        public string lastName{ get; set;}
        public string address{ get; set;}
        public string city{ get; set;}
        public int Age{ get; set;}
        public string phone{ get; set;}
        public string email{ get; set;}
        public string sex{ get; set;}
        public string token{ get; set;}
        public ICollection<Mission> missionList{ get; set;}
    }
}