using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int cheeseID { get; set; }
        public int menuID { get; set; }

        public Menu Menu { get; set; }

        //[Required]
        //[Compare()]
        public List<SelectListItem> Cheeses { get; set; }


        public AddMenuItemViewModel(Menu menu,IEnumerable<Cheese> cheeses)
        {
            Menu = menu;
            Cheeses = new List<SelectListItem>();
            foreach (Cheese ch in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = ch.ID.ToString(),
                    Text = ch.Name
                }); 
            }
            
        }
        public AddMenuItemViewModel()
        {

        }
    }
}
