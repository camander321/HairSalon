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
      return View("Index", Client.GetAll());
    }
    
    [HttpPost("/clients")]
    public ActionResult Create()
    {
      string first = Request.Form["first"];
      string last = Request.Form["last"];
      
      if (first.Length == 0 || last.Length == 0)
        return RedirectToAction("CreateForm");
      
      Client client = new Client(last, first, int.Parse(Request.Form["stylist"]));
      client.Save();
      return View("Index", Client.GetAll());
    }

    [HttpGet("/clients/new")]
    public ActionResult CreateForm()
    {
      return View("CreateForm",Stylist.GetAll());
    }
    
    [HttpGet("/clients/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("client", Client.Find(id));
      model.Add("stylists", Stylist.GetAll());
      return View("Edit", model);
    }
    
    [HttpPost("/clients/{id}/update")]
    public ActionResult Update(int id)
    {
      string first = Request.Form["first"];
      string last = Request.Form["last"];
      
      if (first.Length > 0 && last.Length > 0)
      {
        Client client = Client.Find(id);
        client.EditName(last, first);
        client.ChangeStylist(int.Parse(Request.Form["stylist"]));
      }
      
      return RedirectToAction("Index", Client.GetAll());
    }
    
    [HttpGet("/clients/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Client.Find(id).Delete();
      return View("Index", Client.GetAll());
    }
    
    [HttpPost("/clients/search")]
    public ActionResult Search(int id)
    {
      return View("Index",  Client.Search(Request.Form["search"]));
    }
  }
}