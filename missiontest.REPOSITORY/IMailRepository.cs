using System.Collections.Generic;
using missiontest.MODAL;

namespace missiontest.REPOSITORY
{
    public interface IMailRepository
    {
         bool ShareMission(Email mail, IList<Mission> missions);
         bool SendMission(Email mail, IList<Mission> missions);
    }
}