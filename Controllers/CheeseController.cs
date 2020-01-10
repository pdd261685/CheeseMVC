using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private readonly CheeseDbContext context;//private so we use it only in this class.Also, we need a way to reference the object

        //Constructor definition
        public CheeseController(CheeseDbContext dbContext)//to reference the context private field 
        {
            context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
   
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category ).ToList();
            return View(cheeses);
        }

        public IActionResult Add()
        {
          
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
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
                CheeseCategory newCat = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
                Cheese chObj = addCheeseViewModel.CreateCheesse();
               /* if (newCat==null)
                {
                    newCat.ID = 0;
                    newCat.Name="Default";
                }*/
                chObj.Category = newCat;
                
                context.Cheeses.Add(chObj);
                context.SaveChanges();

                return Redirect("/Cheese");

            }

            return View(addCheeseViewModel);
            
        }
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
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
                
                Cheese theCheese = context.Cheeses.Single(c => c.ID==ch);//object to pass to Remove method
                context.Cheeses.Remove(theCheese);// Remove methods takes object argument and removes that object from databases
 
            }

            context.SaveChanges();

            return Redirect("/Cheese");
        }

       
        [Route("/Cheese/Remove1")]
        [HttpPost]
        public IActionResult RemoveEach(int cheese1)
        {
            Cheese theCheese = context.Cheeses.Single(c => c.ID == cheese1);//object to pass to Remove method
            context.Cheeses.Remove(theCheese);
            context.SaveChanges();//Save changes to the database
            //CheeseData.Remove(cheese1);// compact way using lambda
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
            //Cheese ch = CheeseData.GetById(cheeseId);

            Cheese ch = context.Cheeses.Single(c => c.ID == cheeseId);
            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel(ch, context.Categories.ToList());
            
            return View(editCheeseViewModel);
        }

        [Route ("/Cheese/Edit")]
        [HttpPost]
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese ch = context.Cheeses.Single(c => c.ID == editCheeseViewModel.CheeseId);
                //CheeseCategory cat= context.Categories.Single(c => c.ID ==editCheeseViewModel.CategoryID);
                //Cheese ch = CheeseData.GetById(editCheeseViewModel.CheeseId);
                ch.Name = editCheeseViewModel.Name;
                ch.Description = editCheeseViewModel.Description;
                ch.CategoryID = editCheeseViewModel.CategoryID;
                ch.Rating = editCheeseViewModel.Rating;
                
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(editCheeseViewModel);

        }


    }
}
