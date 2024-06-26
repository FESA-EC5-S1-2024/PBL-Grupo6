﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using PBLprojectMVC.Models;
using PBLprojectMVC.Utils;

namespace PBLprojectMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.UserLogged = HelperController.LoginSessionVerification(HttpContext.Session);
        ViewBag.IsAdmin = HelperController.AdminSessitionVerification(HttpContext.Session);
    
        return View();
    }

    public async Task<JsonResult> Request(string lastN)
    {
        var data = await Request_D(lastN);
        return Json(data);
    }

    public async Task<JsonElement> Request_D(string lastN)
    {
        string port = "8666";
        string host = UtilsParams.Host();

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "http://" + host + ":" + port + "/STH/v2/entities/urn:ngsi-ld:Temp:003/attrs/temperature?type=Temp&lastN=" + lastN);
        request.Headers.Add("fiware-service", "smart");
        request.Headers.Add("fiware-servicepath", "/");
 
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        using var jsonDoc = JsonDocument.Parse(responseBody);
        return jsonDoc.RootElement.Clone();
    }

    public IActionResult About()
    {
        ViewBag.UserLogged = HelperController.LoginSessionVerification(HttpContext.Session);
        ViewBag.IsAdmin = HelperController.AdminSessitionVerification(HttpContext.Session);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {   
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
