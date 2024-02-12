namespace MQTT_Parcel_Website
{
    public interface IPayloadProcessingService
    {
        Task SetPayload(string payload);
    }
}
