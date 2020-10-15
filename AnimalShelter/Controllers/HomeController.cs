using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Collections.Generic;
using AnimalShelter.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      Type type = typeof(Animal);
      PropertyInfo[] properties = type.GetProperties();
      List<String> pl = properties.Select(property => property.Name).ToList();
      List<SelectListItem> sl = pl.Select(p => new SelectListItem { Text = p, Value = p }).ToList();
     
      return View(sl);
    }
  }
}