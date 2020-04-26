using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using missiontest.REPOSITORY;
using missiontest.MODAL;
using Microsoft.AspNetCore.Authorization;


namespace missiontest.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController: ControllerBase
    {
      private readonly ILog _logger;  
        private readonly ImissionRepository _repo;
         private readonly IMailRepository _mailrepo;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userrepo;
        public MissionController( IConfiguration config,ILog logger , ImissionRepository repo, IMailRepository mailrepo,IUserRepository userrepo ){
         
            _config=config;
            _repo = repo;
            _mailrepo = mailrepo;
            _userrepo = userrepo;
             _logger = logger; 
           // _mapper = mapper;
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetMission(string email)
        {
          var user = await _userrepo.GetUserByMail(email);
          if(user == null){
             _logger.Error(" no user ");
             return BadRequest();
           }
          if(user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
              _logger.Information("Unauthorized");
                return Unauthorized();
            }
           var mission = await _repo.GetMissions(user.Id);
          if(mission == null){
             _logger.Error(" no missions ");
             return BadRequest();
           }
          _logger.Information("get mission");
           return Ok(mission);
        }
   
        [HttpGet("shareMission/{id}")]
        public async Task<IActionResult> shareMission(int id)
        {
           var user = await _userrepo.GetUser(id);
           if(user == null){
             _logger.Error(" no user ");
             return BadRequest();
           }
   
          if(user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
              _logger.Information("Unauthorized");
                return Unauthorized();
            }
           var mission = await _repo.GetMissions(id);
           if(mission == null){
             _logger.Error(" no missions to send ");
             return BadRequest();
           }
           Email mailobj = new Email{
             toname = user.firstName + " " + user.lastName,
             toemail = user.email,
             subject = " mission to share "

           };
           _mailrepo.ShareMission(mailobj, mission);
          _logger.Information("share mission");
           return Ok(mission);
        }
        [HttpPut("UpdateMission")]
        public async Task<IActionResult> UpdateMission(Mission userForIpdateDto ){
            var sender= await _userrepo.GetUser(userForIpdateDto.userId);
          if(sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
              _logger.Information("Unauthorized");
                return Unauthorized();
            }
            //userForIpdateDto.Id = id;
            var missionFreomRepo = await _repo.GetMission(userForIpdateDto.Id);
            if(missionFreomRepo == null){
             _logger.Error(" no missions to update ");
             return BadRequest();
            }
            missionFreomRepo.isActive = userForIpdateDto.isActive;
            missionFreomRepo.isDone = userForIpdateDto.isDone;
            missionFreomRepo.name = userForIpdateDto.name;
            missionFreomRepo.missionDate = userForIpdateDto.missionDate;
            missionFreomRepo.priority = userForIpdateDto.priority;
            missionFreomRepo.userId = userForIpdateDto.userId;
          //  _mapper.Map(userForIpdateDto, userFromRepo);
          var test = await _repo.SaveAll();
          if(test){
              _logger.Information("update mission");
               return NoContent();
          }
            _logger.Error("update mission unsuccesfull");
            throw new Exception($"update user {userForIpdateDto.Id} failed on save");

        }
        [HttpPost("CreateMission")]
        public async Task<IActionResult> CreateMission( Mission messageForCreationDto){
           var sender= await _userrepo.GetUser(messageForCreationDto.userId);
          if(sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
              _logger.Information("Unauthorized");
                return Unauthorized();
            }
            _repo.Add(messageForCreationDto);


            if(await _repo.SaveAll()){
                _logger.Information("create mission");
                // var messageToReturn =_mapper.Map<MessageToReturnDto>(message);
                return Ok("created mission");
            }
            
            _logger.Error("create mission failed");
            throw new Exception("failed on save");
        }
          [HttpPut("DeleteMission")]
        public async Task<IActionResult> DeleteMission(Mission messageForCreationDto){
          var sender= await _userrepo.GetUser(messageForCreationDto.userId);
          if (sender == null){
              _logger.Information("Unauthorized");
                return Unauthorized();
            }
          if(sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
              _logger.Information("Unauthorized");
                return Unauthorized();
            }
            var missionFreomRepo = await _repo.GetMission(messageForCreationDto.Id);
            if(missionFreomRepo == null){
             _logger.Error(" no missions to delet ");
             return BadRequest();
            }
            missionFreomRepo.isActive = false;
            if(await _repo.SaveAll()){
              _logger.Information("delete mission");
                return NoContent();
            }
            _logger.Error("delet mission failed");
            return BadRequest($"delete mission failed on save");
        }
        
        [HttpPut("finishMission")]
        
        public async Task<IActionResult> finishMission(Mission missionid   ){
       
            var missionFreomRepo = await _repo.GetMission(missionid.Id );
            if(missionFreomRepo == null){
             _logger.Error(" no missions to finish ");
             return BadRequest();
            }   
          
            missionFreomRepo.isDone = true;
          //  _mapper.Map(userForIpdateDto, userFromRepo);
            if(await _repo.SaveAll()){
              _logger.Information("finish mission");
               return NoContent();
            }
            _logger.Error("finish mission failed");
             return BadRequest($"delete mission failed on save");
           
            
        }
        [HttpGet("sendMissions/{id}")]
        public async  Task<IActionResult> sendMissions(int id){

          var user = await _userrepo.GetUser(id );
          var missions = await _repo.GetMissions(id);
          if(user == null){
             _logger.Error(" no user ");
             return BadRequest();
          }
          if(user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
              _logger.Information("Unauthorized");
                return Unauthorized();
          }
          if(missions == null){
             _logger.Error(" no missions to send ");
             return BadRequest();
           }
         var email = new Email{
           toname = user.Username,
           toemail = user.email,
           subject = "sending missions",
           message = "test"

         } ;
         var res=_mailrepo.SendMission(email,missions) ;
          _logger.Information("send mission");
          return Ok(res);
        
        }
    }
}