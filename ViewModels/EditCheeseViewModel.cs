using CheeseMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class EditCheeseViewModel :AddCheeseViewModel
    {
        public int CheeseId { get; set; }
        public EditCheeseViewModel()
        {
            
        }

        public EditCheeseViewModel(Cheese ch)
        {
            //Use cheese object to intialize the viewModel properties.
            Name = ch.Name;
            Description = ch.Description;
            CheeseId = ch.CheeseId;
            Type = ch.Type;
            Rating = ch.Rating;
        }
    }
}
