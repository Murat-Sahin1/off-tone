using off_tone.Infrastructure.Dtos;

namespace off_tone.Infrastructure.AsyncDataServices.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishLoginEvent(PublishLoginEventDto publishLoginEventDto);
        void PublishRegisterEvent(PublishRegisterEventDto publishRegisterEventDto);
    }
}
