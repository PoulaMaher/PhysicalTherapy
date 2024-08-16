using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Types;
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
        public IActionResult SendLinksToClient([FromBody] SendLinksRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ClientNumber) || request.Links == null || !request.Links.Any())
            {
                return BadRequest(new { message = "The links and clientNumber fields are required." });
            }
            TwilioClient.Init(_accountSid, _authToken);
            var sender = new PhoneNumber("whatsapp:+972722592590");
            //var sender = new PhoneNumber("whatsapp:+14155238886");
            var target = new PhoneNumber($"whatsapp:{request.ClientNumber}");

            var linksMessage = string.Join("\n", request.Links);

            var options = new CreateMessageOptions(target)
            {
                From = sender,
                Body = linksMessage
            };

            var message = MessageResource.Create(options);

            return Ok(new { message = "Links sent successfully!" });
        }
    }

    public class SendLinksRequest
    {
        public List<string> Links { get; set; }
        public string ClientNumber { get; set; }
    }
}