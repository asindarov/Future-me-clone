using System.Text.Json;
using Confluent.Kafka;
using Domain.Models;

namespace Shared.EmailDetailsExtensions;

public static class EmailDetailSerializerExtensions
{
    public static Message<string, string> BuildEmailDetailMessage(this EmailDetail emailDetail)
    {
        var serializedEmail = JsonSerializer.Serialize(emailDetail);
        var message = new Message<string, string>()
        {
            Value = serializedEmail,
        };

        return message;
    }
}