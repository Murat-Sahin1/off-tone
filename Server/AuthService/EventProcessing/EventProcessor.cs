using System.Text.Json;
using AuthService.Features.Responses.Event;
using AuthService.Infrastructure.Data.Event.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AuthService.Infrastructure.Data.Identity.Entities;
using AuthService.Infrastructure.Services.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthService.EventProcessing{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper){
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        
        public void ProcessEvent(string message)
        {
            Console.WriteLine("--> Recieved event, detecting the event type...");
            var eventType = DetermineEvent(message);

            switch (eventType){
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

        private EventType DetermineEvent(string message){
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
            
            switch(eventType.Event){
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

        private async void Login(string loginMessage){
            using(var scope = _scopeFactory.CreateScope()){
                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                
                var loginPublishedDto = JsonSerializer.Deserialize<LoginPublishedDto>(loginMessage);

                try{
                    var userLoginDto = _mapper.Map<UserLoginDto>(loginPublishedDto);
                    var response = await accountService.LoginAsync(userLoginDto);
                    if(response == null){
                        var unauthorizedEvent = _mapper.Map<UnauthorizedEvent>(loginPublishedDto);
                        Console.WriteLine($"--> Login event is rejected, correlation id: {unauthorizedEvent.CorrelationId}");
                    }
                    // TO-DO: Logged in succesfully, publish the user details to the message bus
                } catch(Exception ex){
                    Console.WriteLine("--> Login operation")
                }
            }
        }

        private void Register(string registerMessage){

        }

        protected enum EventType{
            Login,
            Register,
            Undetermined,
            /*TO-DO*/
        }
    }
}