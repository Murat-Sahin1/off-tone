using System.Text.Json;
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
            // This is a demo method to see and mind map how would login event would process

            using(var scope = _scopeFactory.CreateScope()){
                var repo = scope.ServiceProvider.GetRequiredService<IAccountService>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<AppUser>>();

                var publishedLoginInfo = JsonSerializer.Deserialize<LoginPublishedDto>(loginMessage);

                try{
                    var user = await userManager.FindByEmailAsync(publishedLoginInfo.Email);

                    if(user == null){
                        // TODO: Publish a new message to rabbitmq channel
                        Console.WriteLine("--> User could not be found");
                        return; 
                    }

                    var result = await signInManager.CheckPasswordSignInAsync(user, publishedLoginInfo.Password, false); //this should be true

                    if(!result.Succeeded){
                        return;
                    }

                    // Create UserReadResponse and post it to rabbitmq channel

                }
                catch(Exception ex){
                    Console.WriteLine("--> Could not login the user.");
                    Console.WriteLine(ex.Message);
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