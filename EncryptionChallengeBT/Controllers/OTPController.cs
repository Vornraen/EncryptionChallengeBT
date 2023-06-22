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
        public ActionResult<string> GetOTP(string userId)
        {
            try
            {
                var hashedPassword = _passwordService.CreatePassword(userId);
                return Ok(hashedPassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "CheckOTP")]
        public ActionResult<string> CheckPassword(string hash, string userId)
        {
            try
            {
                if (_passwordService.PasswordIsValid(userId, hash))
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
