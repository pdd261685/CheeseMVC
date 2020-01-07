using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class CheeseData
    {
        static private List<Cheese> cheeses = new List<Cheese>();

        //Getall method
        public static List<Cheese> GetAll()
        {
            return cheeses;
        }

        //add method
        public static void Add(Cheese newCheese)
        {
            cheeses.Add(newCheese);
        }

        //remove method
        public static void Remove(int id)
        {
            Cheese cheeseToRemove = GetById(id);
            cheeses.Remove(cheeseToRemove);

        }

        //GetbyId

        public static Cheese GetById(int id)
        {
            return cheeses.Single(x => x.CheeseId == id);
        }

        public static Cheese Edit(int cheeseId, string name, string description)
        {
            Cheese editCheese = GetById(cheeseId);
            editCheese.Name = name;
            editCheese.Description = description;
            return editCheese;
            //cheeses.Add(editCheese);
        }
    }
}
