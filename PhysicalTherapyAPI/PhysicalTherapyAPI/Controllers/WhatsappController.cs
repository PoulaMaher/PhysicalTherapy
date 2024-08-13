using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010;
using Twilio.Rest.Api.V2010.Account;

namespace PhysicalTherapyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsappController : ControllerBase
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        public WhatsappController(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"]; 
            _authToken = configuration["Twilio:AuthToken"]; 
        }
        [HttpPost("SendLinksToClient")]
        public IActionResult SendLinksToClient([FromBody]string links , string clientNumber)
        {
            TwilioClient.Init(this._accountSid, this._authToken);
            PhoneNumber sender = new PhoneNumber("whatsapp:+14155238886");
            PhoneNumber target = new PhoneNumber("whatsapp:+201096265303");
            CreateMessageOptions options = new CreateMessageOptions(target);
            options.From = sender;
            options.Body = links;
            MessageResource.Create(options);
            return Ok("Links Send Successfully!");
        }
    }
}
