using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace TenForceConsole.Domain.DataTransferObjects.JsonObjects
{
    ///<summary>
    /// The converter is needed for the nested DTO's properties. Without the converter deserialization will fail. 
    ///</summary>
    public class JsonPathConverter : JsonConverter
    {
        /// <inheritdoc />
        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var targetObj = Activator.CreateInstance(objectType)
                ?? throw new InvalidOperationException($"Could not create an instance of {objectType}.");

            foreach (var prop in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var att = prop.GetCustomAttributes(true)
                    .OfType<JsonPropertyAttribute>()
                    .FirstOrDefault();

                var jsonPath = att?.PropertyName ?? prop.Name;

                if (serializer.ContractResolver is DefaultContractResolver resolver)
                {
                    jsonPath = resolver.GetResolvedPropertyName(jsonPath);
                }

                if (string.IsNullOrWhiteSpace(jsonPath) || !Regex.IsMatch(jsonPath, @"^[a-zA-Z0-9_.-]+$"))
                {
                    throw new InvalidOperationException(
                        $"JProperties of JsonPathConverter can have only letters, numbers, underscores, hiffens and dots but name was ${jsonPath}."); // Array operations not permitted
                }

                var token = jo.SelectToken(jsonPath);
                if (token != null && token.Type != JTokenType.Null)
                {
                    var value = token.ToObject(prop.PropertyType, serializer);
                    prop.SetValue(targetObj, value);
                }
            }

            return targetObj;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return objectType.GetCustomAttributes(true).OfType<JsonPathConverter>().Any();
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var properties = value.GetType().GetRuntimeProperties().Where(p => p.CanRead && p.CanWrite);
            var main = new JObject();
            foreach (var prop in properties)
            {
                var att = prop.GetCustomAttributes(true)
                    .OfType<JsonPropertyAttribute>()
                    .FirstOrDefault();

                var jsonPath = att?.PropertyName ?? prop.Name;

                if (serializer.ContractResolver is DefaultContractResolver resolver)
                {
                    jsonPath = resolver.GetResolvedPropertyName(jsonPath);
                }

                if (string.IsNullOrWhiteSpace(jsonPath))
                {
                    continue;
                }

                var nesting = jsonPath.Split('.');
                var lastLevel = main;

                for (var i = 0; i < nesting.Length; i++)
                {
                    var segment = nesting[i];
                    if (string.IsNullOrEmpty(segment))
                    {
                        continue;
                    }

                    if (i == nesting.Length - 1)
                    {
                        lastLevel[segment] = new JValue(prop.GetValue(value));
                    }
                    else
                    {
                        var childToken = lastLevel[segment];
                        if (childToken == null || childToken.Type == JTokenType.Null)
                        {
                            childToken = new JObject();
                            lastLevel[segment] = childToken;
                        }

                        lastLevel = (JObject)childToken;
                    }
                }
            }

            serializer.Serialize(writer, main);
        }
    }
}
