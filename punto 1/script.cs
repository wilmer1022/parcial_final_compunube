#r "Newtonsoft.Json"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{

    string requestBody = String.Empty;
    using (StreamReader streamReader =  new  StreamReader(req.Body))
    {
        requestBody = await streamReader.ReadToEndAsync();
    }

    dynamic confidenceScores = JsonConvert.DeserializeObject(requestBody);
	var scores = confidenceScores[0]["sentiment"];
    string value = "Positive";

    if(scores == "Negative")
    {
        value = "Negative";
    }
    else if (scores == "Neutral") 
    {
        value = "Neutral";
    }

    return requestBody != null
        ? (ActionResult)new OkObjectResult(value)
       : new BadRequestObjectResult("Pass a sentiment score in the request body.");
}