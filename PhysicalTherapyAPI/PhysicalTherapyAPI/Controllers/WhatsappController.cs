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
            // Check for null or invalid data
            if (request == null || string.IsNullOrWhiteSpace(request.ClientNumber) || request.Links == null || !request.Links.Any())
            {
                return BadRequest(new { message = "The links and clientNumber fields are required." });
            }

            // Initialize Twilio client
            TwilioClient.Init(_accountSid, _authToken);

            // Create phone numbers for sender and target
            var sender = new PhoneNumber("whatsapp:+14155238886");
            var target = new PhoneNumber($"whatsapp:{request.ClientNumber}");

            // Join links into a single message
            var linksMessage = string.Join("\n", request.Links);

            // Set up the message options
            var options = new CreateMessageOptions(target)
            {
                From = sender,
                Body = linksMessage
            };

            // Send the message via Twilio
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