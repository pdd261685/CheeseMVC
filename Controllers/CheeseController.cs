using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
   
            List<Cheese> cheeses = CheeseData.GetAll();
            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }


        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            //using initialised constructor in cheese.cs where Cheese class is defined
            /*Cheese chObj = new Cheese(name,desc);
            Cheeses.Add(chObj);*/

            /*using empty constructor in cheese.cs where Cheese class is defined
            chObj = new Cheese{
                Description = desc,
                Name = name
            };*/
            if (ModelState.IsValid)
            {
                Cheese chObj = addCheeseViewModel.CreateCheesse();
                
                CheeseData.Add(chObj);


                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
            
        }
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }
        [Route("/Cheese/Remove2")]
       //[Route("/Cheese")]
       [HttpPost]
        public IActionResult RemoveCheeses(int[] cheeseIds)
        {
            //Loop to get each value selected passed through string[] list naed cheese
            foreach (int ch in cheeseIds)
            {
                //Loop through the list of objects to find the object with respective id, using ch
                CheeseData.Remove(ch);
 
            }

            return Redirect("/Cheese");
        }

       
        [Route("/Cheese/Remove1")]
        [HttpPost]
        public IActionResult RemoveEach(int cheese1)
        {
            CheeseData.Remove(cheese1);// compact way using lambda
            // Cheeses.Remove(Cheeses.SingleOrDefault(x => x.CheeseId == cheese1)); // same as above statement
           
            //Earlier code without use of lambda
            /*foreach (Cheese singleCheese in Cheeses)
            {
                           
                if (singleCheese.CheeseId==cheese1)
                {
                    Cheeses.Remove(singleCheese);
                    break;
                }
            }*/

            return Redirect("/Cheese");
        }


        [Route("/Cheese/Edit")]
        public IActionResult Edit(int cheeseId)
        {
            Cheese ch = CheeseData.GetById(cheeseId);
            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel(ch);
            
            return View(editCheeseViewModel);
        }

        [Route ("/Cheese/Edit")]
        [HttpPost]
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese ch = CheeseData.GetById(editCheeseViewModel.CheeseId);
                ch.Name = editCheeseViewModel.Name;
                ch.Description = editCheeseViewModel.Description;
                ch.Type = editCheeseViewModel.Type;
                ch.Rating = editCheeseViewModel.Rating;
                return Redirect("/Cheese");
            }

            return View(editCheeseViewModel);

        }


    }
}
