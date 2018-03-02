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
    
    [HttpGet("/stylists/{id}/edit")]
    public ActionResult Edit(int id)
    {
      return View("Edit", Stylist.Find(id));
    }
    
    [HttpPost("/stylists/{id}/update")]
    public ActionResult Update(int id)
    {
      string first = Request.Form["first"];
      string last = Request.Form["last"];
      
      if (first.Length > 0 && last.Length > 0)
      {
        Stylist stylist = Stylist.Find(id);
        stylist.EditName(last, first);
      }
      
      return RedirectToAction("Index", Stylist.GetAll());
    }
    
    [HttpGet("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Stylist.Find(id).Delete();
      return View("Index", Stylist.GetAll());
    }
  }
}
