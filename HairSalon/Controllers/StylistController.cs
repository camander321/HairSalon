using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      return View("Index", Stylist.GetAll());
    }
    
    [HttpPost("/stylists")]
    public ActionResult Create()
    {
      string first = Request.Form["first"];
      string last = Request.Form["last"];
      
      if (first.Length == 0 || last.Length == 0)
        return RedirectToAction("CreateForm");
      
      Stylist newStylist = new Stylist(last, first);
      newStylist.Save();
      return View("Index", Stylist.GetAll());
    }
    
    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {
      return View("CreateForm");
    }
    
    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(id);
      model.Add("stylist", stylist);
      model.Add("clients", stylist.GetClients());
      return View("Details", model);
    }
    
    [HttpPost("/stylists/search")]
    public ActionResult Search(int id)
    {
      List<Stylist> model = Stylist.Search(Request.Form["search"]);
      return View("Index", model);
    }
  }
}
