using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name="Cheese Name")]
        public string Name { get; set; }

        [Required (ErrorMessage="Please provide description")]
        [StringLength(30,MinimumLength =5,ErrorMessage = "Enter between 5-30 characters")]
        public string Description { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public AddCheeseViewModel(IEnumerable <CheeseCategory> categories)
        {
            //CheeseTypes = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
            /*Categories.Add(new SelectListItem(){
                Value = "Default",
                Text="Default"
            });*/
            foreach (CheeseCategory cat in categories)
            {
             
                Categories.Add(new SelectListItem()
                {
                    Value = (cat.ID).ToString(),
                    Text = cat.Name
                });
            }
 
        }

        public AddCheeseViewModel()
        {

        }

        public Cheese CreateCheesse()
        {
            return new Cheese
            {
                Name = this.Name,
                Description = this.Description,
                CategoryID=this.CategoryID,
                Rating = this.Rating
            };
}
    }
}
