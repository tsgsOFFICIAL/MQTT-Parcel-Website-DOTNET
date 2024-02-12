using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using MQTTnet;
using System.Text;

namespace MQTT_Parcel_Website.Services
{
    public class MqttService
    {
        private string _payload;
        private IMqttClient _mqttClient;

        public async Task ConnectClient()
        {
            string broker = "192.168.1.45";
            int port = 1883;
            string clientId = Guid.NewGuid().ToString();
            string topic = "demo";

            MqttFactory mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            MqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(broker, port)
                .WithClientId(clientId)
                .WithCleanSession()
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(10))
                .WithProtocolVersion(MqttProtocolVersion.V311)
                .Build();

            await _mqttClient.ConnectAsync(options);

            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce).Build());

            _mqttClient.ApplicationMessageReceivedAsync += (e) => {
                e.AutoAcknowledge = true;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                OnMessageReceived(payload);
                return Task.CompletedTask;
            };
        }

        private void OnMessageReceived(string payload)
        {
            _payload = payload;
        }

        public string GetPayload()
        {
            return _payload;
        }
    }
}
