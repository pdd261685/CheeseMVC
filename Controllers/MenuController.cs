﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;//private so we use it only in this class.Also, we need a way to reference the object

        //Constructor definition
        public MenuController(CheeseDbContext dbContext)//to reference the context private field 
        {
            context = dbContext;
        }
        // GET: /<controller>/
        // GET: /<controller>/
        public IActionResult Index()
        {

            List<Menu> menuList = context.Menus.ToList();
       
            return View(menuList);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenuItem = new Menu { 
                    Name=addMenuViewModel.Name
                };
                context.Menus.Add(newMenuItem);
                context.SaveChanges();
                return Redirect("/Menu/ViewMenu/" + newMenuItem.ID);
            }
            return View(addMenuViewModel);
        }

        //Get Controller
        public IActionResult ViewMenu(int id)
        {
            Menu newItem = context.Menus.Single(c => c.ID == id);

            List<CheeseMenu> items = context.CheeseMenus.Include(item => item.Cheese).Where(cm => cm.MenuID == id).ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel()
            {
                Menu = newItem,
                Items = items
            };

            return View(viewMenuViewModel);
        }

        //Get Controller

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, context.Cheeses.ToList());
            return View(addMenuItemViewModel);

        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                
                IList<CheeseMenu> existingItems = context.CheeseMenus
        .Where(cm => cm.CheeseID == addMenuItemViewModel.cheeseID)
        .Where(cm => cm.MenuID == addMenuItemViewModel.menuID).ToList();

                if (!existingItems.Any())
                {
                    CheeseMenu cheeseMenu = new CheeseMenu(){
                        CheeseID= addMenuItemViewModel.cheeseID,
                        MenuID= addMenuItemViewModel.menuID
                    };

                    context.CheeseMenus.Add(cheeseMenu);
                    context.SaveChanges();
                    return Redirect("/Menu/ViewMenu/"+cheeseMenu.MenuID);

               }
                ViewBag.error = "Duplicate cheese";
                // return View(addMenuItemViewModel);
                //return View()
                //return Redirect("/Menu/AddItem/" + addMenuItemViewModel.menuID+"?error=Error"); 
            }

            return View(addMenuItemViewModel);
        }




    }
}
