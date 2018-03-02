using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View("Index");
    }
    
    [HttpPost("/")]
    public ActionResult NewClient()
    {
      Client client = new Client(Request.Form["last"], Request.Form["first"], int.Parse(Request.Form["stylist"]));
      client.Save();
      return View("Index");
    }
  }
}
