using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      return View("StylistIndex", Stylist.GetAll());
    }
    
    [HttpPost("/stylists")]
    public ActionResult Create()
    {
      string first = Request.Form["first"];
      string last = Request.Form["last"];
      Stylist newStylist = new Stylist(last, first);
      newStylist.Save();
      return View("StylistIndex", Stylist.GetAll());
    }
    
    [HttpGet("/stylists/new")]
    public ActionResult StylistCreateForm()
    {
      return View("StylistCreateForm");
    }
    
    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      return View("StylistDetails");
    }
  }
}
