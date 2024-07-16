using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;

public class PaymentTypeConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(PaymentType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = JToken.Load(reader).ToString();
        if (Enum.TryParse(typeof(PaymentType), value, true, out var enumValue))
        {
            return (PaymentType)enumValue;
        }

        throw new JsonSerializationException($"Unable to convert '{value}' to PaymentType.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}
