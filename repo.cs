using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPIClient
{
    public class Response
    {
        [JsonPropertyName("ReasonPhrase")]
        public string? ReasonPhrase { get; set; }

        [JsonPropertyName("StatusCode")]
        public string? StatusCode { get; set; }

        [JsonPropertyName("Version")]
        public string? Version { get; set; }

        [JsonPropertyName("Content")]
        public string? Content { get; set; }

        [JsonPropertyName("Headers")]
        public string? Headers { get; set; }

        [JsonPropertyName("Request-Context")]
        public string? RequestContext { get; set; }

        [JsonPropertyName("Content-Length")]
        public string? ContentLength { get; set; }

    }
}