using EncryptionChallengeBT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionChallengeBT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public OTPController (IPasswordService password)
        {
            _passwordService = password;
        }

        [HttpGet(Name = "GetOTP")]
        public ActionResult<string> GetOTP(string userid)
        {
            try
            {
                var hashedPassword = _passwordService.CreatePassword(userid);
                return Ok(hashedPassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "CheckOTP")]
        public ActionResult<string> CheckPassword(string hash, string userid)
        {
            try
            {
                if (_passwordService.CheckPassword(userid, hash) == true)
                    return Ok("Accesul este permis");
                return Unauthorized("Acces interzis");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
