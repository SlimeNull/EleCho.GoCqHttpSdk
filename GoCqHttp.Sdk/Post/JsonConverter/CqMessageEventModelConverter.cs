﻿using NullLib.GoCqHttpSdk.Post.Model;
using NullLib.GoCqHttpSdk.Util;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NullLib.GoCqHttpSdk.Post.JsonConverter
{
    internal class CqMessageEventModelConverter : JsonConverter<CqMessagePostModel>
    {
        public override CqMessagePostModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonDocument doc = JsonDocument.ParseValue(ref reader);

            string? messageType = doc.RootElement.GetProperty("message_type").GetString();

            return messageType switch
            {
                "private" => JsonSerializer.Deserialize<CqPrivateMessagePostModel>(doc, options),
                "group" => JsonSerializer.Deserialize<CqGroupMessagePostModel>(doc, options),

                _ => null
            };
        }

        public override void Write(Utf8JsonWriter writer, CqMessagePostModel value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException();
        }
    }
}