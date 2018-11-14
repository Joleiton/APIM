#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

const double revenuePerkW = 0.12; 
const double technicianCost = 250; 
const double turbineCost = 100;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{   
    //Get request body
    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    int hours = data.hours;
    int capacity = data.capacity;

    //Formulas to calculate revenue and cost
    double revenueOpportunity = capacity * revenuePerkW * 24;  
    double costToFix = (hours * technicianCost) +  turbineCost;
    string repairTurbine;

    if (revenueOpportunity > costToFix){
        repairTurbine = "Yes";
    }
    else {
        repairTurbine = "No";
    }

    return (ActionResult) new OkObjectResult(new{
        message = repairTurbine,
        revenueOpportunity = "$"+ revenueOpportunity,
        costToFix = "$"+ costToFix         
    }); 
}
