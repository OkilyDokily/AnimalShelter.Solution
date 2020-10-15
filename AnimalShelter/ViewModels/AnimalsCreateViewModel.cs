using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AnimalShelter.ViewModels
{
  public class AnimalsCreateViewModel
  {
    public string Name { get; set; }
    public string Gender { get; set; }
    public string Breed { get; set; }
    public string Type { get; set; }
    public List<SelectListItem> BreedList { get; set; }
    public List<SelectListItem> TypeList { get; set; }
  }

}