using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using MQTTnet;
using System.Text;

namespace MQTT_Parcel_Website.Services
{
    public class MqttService
    {
        private static string? _payload;
        private static IMqttClient? _mqttClient;
        private static string? _lastSubscribedTopic;

        public async Task ConnectClient()
        {
            string broker = "192.168.50.24";
            int port = 1883;
            string clientId = Guid.NewGuid().ToString();

            MqttFactory mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            MqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(broker, port)
                .WithClientId(clientId)
                .WithCleanSession()
                .WithKeepAlivePeriod(TimeSpan.FromMinutes(15))
                .WithProtocolVersion(MqttProtocolVersion.V311)
                .Build();

            await _mqttClient.ConnectAsync(options);
        }

        private void OnMessageReceived(string payload)
        {
            _payload = payload;
        }

        public string GetPayload()
        {
            return _payload ?? "";
        }

        public async Task UpdateSubscribtion(string topic)
        {
            if (_mqttClient != null && _mqttClient.IsConnected)
            {

                // Remove existing event handler if it exists
                _mqttClient.ApplicationMessageReceivedAsync -= HandleApplicationMessageReceived;

                // Unsubscribe from the last subscribed topic if it exists
                if (!string.IsNullOrEmpty(_lastSubscribedTopic))
                {
                    _payload = "";
                    await _mqttClient.UnsubscribeAsync(_lastSubscribedTopic);
                    _lastSubscribedTopic = string.Empty;
                }
            }
            else
            {
                await ConnectClient();
            }

            _lastSubscribedTopic = topic;
            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce).Build());

            // Add the new event handler for the current subscription
            _mqttClient!.ApplicationMessageReceivedAsync += HandleApplicationMessageReceived;
        }



        // Define the event handler for the ApplicationMessageReceived event
        private async Task HandleApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
             e.AutoAcknowledge = true;
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            OnMessageReceived(payload);
            await Task.CompletedTask;
        }
    }
}
