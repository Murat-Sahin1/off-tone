using System.Text.Json;
using AuthService.Features.Responses.Event;
using AuthService.Infrastructure.Data.Event.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AuthService.Infrastructure.Services.Identity;
using AutoMapper;

namespace AuthService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            Console.WriteLine("--> Recieved event, detecting the event type...");
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.Login:
                    Login(message);
                    break;
                case EventType.Register:
                    Register(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string message)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);

            switch (eventType.Event)
            {
                case "Login":
                    Console.WriteLine("--> Login event is detected.");
                    return EventType.Login;
                case "Register":
                    Console.WriteLine("--> Register event is detected");
                    return EventType.Register;
                default:
                    Console.WriteLine("--> Couldn't determine the event type.");
                    return EventType.Undetermined;
            }
        }

        private async void Login(string loginMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();

                var loginPublishedDto = JsonSerializer.Deserialize<LoginPublishedDto>(loginMessage);

                try
                {
                    var userLoginDto = _mapper.Map<UserLoginDto>(loginPublishedDto);
                    var response = await accountService.LoginAsync(userLoginDto);

                    if (!response.IsSucceessful)
                    {
                        var unauthorizedEvent = new UnauthorizedEvent
                        {
                            CorrelationId = loginPublishedDto.CorrelationId,
                            Timestamp = DateTime.Now.Date.ToString(),
                            Email = loginPublishedDto.Email,
                            IPAddress = loginPublishedDto.IPAddress,
                            UserAgent = loginPublishedDto.UserAgent,
                            Event = "Unauthorized",
                            IsSuccessful = false,
                            FailureReason = response.ErrorList.ToString()
                        };

                        Console.WriteLine($"--> Login event is rejected, correlation id: {unauthorizedEvent.CorrelationId}");

                        // TO-DO: Publish the unauthorized event to the message bus
                    }

                    var successfulLoginEvent = new SuccessfulLoginEvent
                    {
                        CorrelationId = loginPublishedDto.CorrelationId,
                        Timestamp = DateTime.Now.Date.ToString(),
                        Email = userLoginDto.Email,
                        IPAddress = loginPublishedDto.IPAddress,
                        UserAgent = loginPublishedDto.UserAgent,
                        Event = "Successful_Login",
                        IsSucceessful = true,
                    };

                    // TO-DO: Logged in succesfully, publish the SuccessfulLoginEvent to the message bus
                }
                catch (Exception ex)
                {
                    Console.WriteLine("--> Login operation is failed: ", ex.Message);
                }
            }
        }

        private async void Register(string registerMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();

                var registerPublishedDto = JsonSerializer.Deserialize<RegisterPublishedDto>(registerMessage);

                try
                {
                    var userRegisterDto = _mapper.Map<UserRegisterDto>(registerPublishedDto);
                    var response = await accountService.RegisterAsync(userRegisterDto);

                    if (response.IsSuccessful == false)
                    {
                        var registerFailedEvent = new RegisterFailedEvent
                        {
                            CorrelationId = registerPublishedDto.CorrelationId,
                            Timestamp = DateTime.Now.Date,
                            Email = registerPublishedDto.Email,
                            IPAddress = registerPublishedDto.IPAddress,
                            UserAgent = registerPublishedDto.UserAgent,
                            Event = registerPublishedDto.Event,
                            IsSuccessful = false,
                            FailureReason = response.ErrorList.ToString()
                        };

                        Console.WriteLine($"--> Register event is rejected, correlation id: {registerFailedEvent.CorrelationId}");

                        // TO-DO: Publish the register failed event to the message bus
                    }

                    var successfulLoginEvent = new SuccessfulRegisterEvent
                    {
                        CorrelationId = registerPublishedDto.CorrelationId,
                        Timestamp = DateTime.Now.Date,
                        Email = userRegisterDto.Email,
                        IPAddress = registerPublishedDto.IPAddress,
                        UserAgent = registerPublishedDto.UserAgent,
                        Event = "Successful_Register",
                        IsSucceessful = true,
                    };

                    // TO-DO: Registered succesfully, publish the "response" to the message bus
                }
                catch (Exception ex)
                {
                    Console.WriteLine("--> Register operation is failed: ", ex.Message);
                }
            }
        }

        protected enum EventType
        {
            Login,
            Register,
            Undetermined,
            /*TO-DO*/
        }
    }
}