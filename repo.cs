using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPIClient
{
    public class Response
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("statusCode")]
        public string? StatusCode { get; set; }

        ////for each of the elements you want to decode, create a constructor for a nullable property.
        //[JsonPropertyName("description")]
        //public string? Description { get; set; }

        //[JsonPropertyName("html_url")]
        //public Uri? GitHubHomeUrl { get; set; }

        //[JsonPropertyName("homepage")]
        //public Uri? Homepage { get; set; }

        //[JsonPropertyName("watchers")]
        //public int? Watchers { get; set; }

        //[JsonPropertyName("pushed_at")]
        //public DateTime? LastPushUtc { get; set; }

    }
}