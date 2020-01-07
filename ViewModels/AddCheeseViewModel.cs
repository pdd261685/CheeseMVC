﻿using CheeseMVC.Models;
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

        public CheeseType Type{ get; set; }
        public List<SelectListItem> CheeseTypes { get; set; }
        public AddCheeseViewModel()
        {
            CheeseTypes = new List<SelectListItem>();
            CheeseTypes.Add(new SelectListItem()
            {
                Value=((int) CheeseType.Hard).ToString() ,
                Text= CheeseType.Hard.ToString()
            });
            CheeseTypes.Add(new SelectListItem()
            {
                Value = ((int)CheeseType.Soft).ToString(),
                Text = CheeseType.Soft.ToString()
            });
            CheeseTypes.Add(new SelectListItem()
            {
                Value = ((int)CheeseType.Fake).ToString(),
                Text = CheeseType.Fake.ToString()
            });
        }

        public Cheese CreateCheesse()
        {
            return new Cheese
            {
                Name = this.Name,
                Description = this.Description,
                Type = this.Type,
                Rating = this.Rating
            }                
            ;
}
    }
}