namespace Catalog.Application.Common.MessageProducer;

public interface IMessageProducerService
{
    void SendMessage<TMessageModel>(TMessageModel messageModel);
}
