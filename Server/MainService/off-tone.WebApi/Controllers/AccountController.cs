using Microsoft.AspNetCore.Mvc;
using off_tone.Application.Dtos.IdentityDtos.User;
using off_tone.Infrastructure.Responses.User;
using off_tone.Infrastructure.AsyncDataServices.Interfaces;
using AutoMapper;
using off_tone.Infrastructure.Dtos;

namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMapper _mapper;

        public AccountController(IMessageBusClient messageBusClient, IMapper mapper)
        {
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpPost]
        public string/*UserTokenResponse*/ Login(UserLoginDto userLoginDto)
        {
            PublishLoginEventDto publishLoginEventDto = _mapper.Map<PublishLoginEventDto>(userLoginDto);

            publishLoginEventDto.Event = "Login";
            publishLoginEventDto.CorrelationId = Guid.NewGuid().ToString();
            publishLoginEventDto.Timestamp = DateTime.UtcNow.ToString();
            publishLoginEventDto.UserAgent = HttpContext.Request.Headers["User-Agent"];
            if (HttpContext.Connection.RemoteIpAddress == null)
            {
                publishLoginEventDto.IPAddress = "IP-Address-Null";
            }
            else
            {
                publishLoginEventDto.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            _messageBusClient.PublishLoginEvent(publishLoginEventDto);

            //for development.
            return "Login event is published.";
        }
        /*
                 [HttpPost]
        public UserTokenResponse Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }
         */

    }
}
