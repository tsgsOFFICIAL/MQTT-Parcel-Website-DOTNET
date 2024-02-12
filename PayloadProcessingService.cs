using MQTT_Parcel_Website.Controllers;

namespace MQTT_Parcel_Website
{
    public class PayloadProcessingService : IPayloadProcessingService
    {
        // Define a delegate type for the event
        public delegate Task PayloadUpdatedEventHandler(string payload);

        // Define the event using the delegate type
        public event PayloadUpdatedEventHandler PayloadUpdated;

        public async Task SetPayload(string payload)
        {
            // Update the payload value
            // Invoke the event
            await PayloadUpdated?.Invoke(payload);
        }
    }
}
