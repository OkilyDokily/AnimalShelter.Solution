using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using AnimalShelter.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;
    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }
    public ActionResult Index(string order)
    {
      List<Animal> animals = _db.Animals.OrderBy(a => a.GetType().GetProperty(order).GetValue(a, null)).ToList();

      Dictionary<string, List<Animal>> d = new Dictionary<string, List<Animal>>();

      d.Add(order, animals);
      return View(d);
    }

    public ActionResult Create()
    {
      AnimalsCreateViewModel animalsCreateViewModel = new AnimalsCreateViewModel();
      List<string> breeds = _db.Animals.Select(animal => animal.Breed).Distinct().ToList();
      List<SelectListItem> breedssli = breeds.Select(breed => new SelectListItem { Text = breed, Value = breed }).ToList();

      List<string> types = _db.Animals.Select(animal => animal.Type).Distinct().ToList();
      List<SelectListItem> typessli = types.Select(breed => new SelectListItem { Text = breed, Value = breed }).ToList();

      animalsCreateViewModel.BreedList = breedssli;
      animalsCreateViewModel.TypeList = typessli;

      return View(animalsCreateViewModel);
    }

    [HttpPost]
    public ActionResult Create(AnimalsCreateViewModel t, string newbreed, string newtype)
    {
      Animal animal = new Animal
      {
        Name = t.Name,
        Gender = t.Gender,
        Breed = (String.IsNullOrEmpty(newbreed)) ? t.Breed : newbreed,
        Type = (String.IsNullOrEmpty(newtype)) ? t.Type : newtype
      };

      _db.Animals.Add(animal);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    public ActionResult Details(int id)
    {
      Animal animal = _db.Animals.FirstOrDefault(animals => animals.AnimalId == id);
      return View(animal);
    }
  }
}