using System;

namespace missiontest.MODAL
{
    public class Mission
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime missionDate { get; set; }
        public int priority { get; set; }
        public bool isActive { get; set; }
        public bool isDone { get; set; }
    }
}