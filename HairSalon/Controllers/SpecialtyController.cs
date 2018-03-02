using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {

    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      return View("Index", Specialty.GetAll());
    }
    
    [HttpPost("/specialties")]
    public ActionResult Create()
    {
      string name = Request.Form["name"];
      
      if (name.Length == 0)
        return RedirectToAction("CreateForm");
      
      Specialty newSpecialty = new Specialty(name);
      newSpecialty.Save();
      return View("Index", Specialty.GetAll());
    }
    
    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      return View("CreateForm");
    }
    
    [HttpGet("/specialties/{id}/edit")]
    public ActionResult Edit(int id)
    {
      return View("Edit", Specialty.Find(id));
    }
    
    [HttpPost("/specialties/{id}/update")]
    public ActionResult Update(int id)
    {
      string name = Request.Form["name"];
      
      if (name.Length > 0)
      {
        Specialty specialty = Specialty.Find(id);
        specialty.EditName(name);
      }
      
      return RedirectToAction("Index");
    }
    
    [HttpGet("/specialties/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Specialty.Find(id).Delete();
      return RedirectToAction("Index");
    }
    
    [HttpGet("/specialties/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty specialty = Specialty.Find(id);
      model.Add("specialty", specialty);
      model.Add("stylists", specialty.GetStylists());
      return View("Details", model);
    }
    
    [HttpGet("/specialties/{id}/stylists/{stylistId}/remove")]
    public ActionResult RemoveStylist(int id, int stylistId)
    {
      Specialty.Find(id).RemoveStylist(stylistId);
      return RedirectToAction("Details", id);
    }
    
    [HttpGet("/specialties/{id}/stylists/{stylistId}/add")]
    public ActionResult AddStylist(int id, int stylistId)
    {
      Specialty.Find(id).AddStylist(stylistId);
      return RedirectToAction("Details", id);
    }
    
    [HttpGet("/specialties/clear")]
    public ActionResult DeleteAll(int id)
    {
      Specialty.DeleteAll();
      return RedirectToAction("Index", Specialty.GetAll());
    }
  }
}