using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System;

using System.Data.SqlClient;
using missiontest.REPOSITORY;
using missiontest.MODAL;
using Microsoft.AspNetCore.Http;

namespace missiontest.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
     
        private readonly ILog _logger;  
 private readonly IUserRepository _userrepo;
        private readonly IAuthRepository _repo;
         private readonly IConfiguration _config;
         //private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config,ILog logger,IUserRepository userrepo  ){
            _repo= repo;
            _config=config;
             _logger = logger; 
             _userrepo = userrepo; 
           // _mapper = mapper;
        
           
        } 

        [HttpPost("register")]
        public async Task<IActionResult> Register(userForRegister userForRegisterDto  ){
            if(!ModelState.IsValid){
                _logger.Error("invalid user data");
                return BadRequest(ModelState);
            }

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if(await _repo.UserExist(userForRegisterDto.Username)){
                 _logger.Error("user exist");
                return BadRequest("user name exists");
            }
            
            var userToCreate = new User{
                Username=userForRegisterDto.Username,
                firstName = userForRegisterDto.firstName,
                lastName = userForRegisterDto.lastName,
                Age = userForRegisterDto.Age,
                phone = userForRegisterDto.phone,
                email = userForRegisterDto.email,
                sex = userForRegisterDto.sex
            };

            var createUser = await _repo.Register(userToCreate,userForRegisterDto.Password);
            _logger.Information("user created");
            return StatusCode(201);
        }
   
        [HttpPost("login")]
        public async Task<IActionResult> Login(userForLogin userForLoginDto){


               var userFromRepo= await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password );
       
           
            if(userFromRepo == null){
                 _logger.Error("user Unauthorized");
                return Unauthorized();
            }
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Username)
                
            };
         
              var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));    
           // var key = new SymmetricSecurityKey(Encoding.UTF8
           // .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokentosession = tokenHandler.WriteToken(token);
            userFromRepo.token = tokentosession;
            var res = await _userrepo.SaveAll();
          // HttpContext.Session.GetString("User");
            //var user = _mapper.Map<UserForListDto>(userFromRepo);
           // HttpContext.Session.SetString("JWToken", tokentosession); 
           _logger.Information("user loged");  
            return Ok(new {
                token= tokenHandler.WriteToken(token),
                userFromRepo
            });
        
        }
    }
     
}