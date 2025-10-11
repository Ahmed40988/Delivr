using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deliver.Entities.Enums
{
    [JsonConverter(typeof(CaseInsensitiveEnumConverter<UserType>))]
    public enum UserType
    {
        Customer = 1,
        Delivery = 2,
        Supplier = 3
    }
    public class CaseInsensitiveEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (Enum.TryParse<T>(value, ignoreCase: true, out var result))
            {
                return result;
            }
            throw new JsonException($"Unable to convert '{value}' to enum {typeof(T)}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}
