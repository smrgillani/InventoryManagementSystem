﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Controllers
{
   
    
        public static class ExtensionMethod
        {
            public static string ToJson(this object value)
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> { new StringEnumConverter() }
                };

                return JsonConvert.SerializeObject(value, settings);
            }
        }
    }
