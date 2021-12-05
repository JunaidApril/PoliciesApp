using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Wtw.Policies.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BusinessErrorCode
    { 
        [EnumMember(Value = "ServerError")] 
        ServerError = 10000,

        [EnumMember(Value = "ValidationError")]
        ValidationError = 10001,

        [EnumMember(Value = "CreatePolicyFailed")]
        CreatePolicyFailed = 10002,
    }
}
