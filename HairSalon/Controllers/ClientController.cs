using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
    
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      return View("ClientIndex", Client.GetAll());
    }

    [HttpGet("/clients/new")]
    public ActionResult ClientCreateForm()
    {
      return View("ClientCreateForm", Stylist.GetAll());
    }
    
    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Index(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("client", Client.Find(clientId));
      model.Add("stylist", Stylist.Find(stylistId));
      return View("ClientDetails", model);
    }
    
    [HttpPost("/clients/search")]
    public ActionResult Search(int id)
    {
      List<Client> model = Client.Search(Request.Form["search"]);
      return View("ClientIndex", model);
    }
  }
}