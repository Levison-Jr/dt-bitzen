﻿using System.Text.Json.Serialization;

namespace DTBitzen.Dtos
{
    public record BaseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<LinkRef>? Links { get; set; }
    }

    public record LinkRef
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public LinkRef(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
