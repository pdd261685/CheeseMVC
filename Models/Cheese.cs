using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CheeseId { get; set; }
        public int Rating { get; set; }

        private static int nextId = 0;

        public CheeseType Type { get; set; }

        //enumerate options for data types, so type can hold CheeseType.Mild or Sharp
        /*public enum CheeseType
        {
            Mild,
            Sharp
        }*/

        //Initialised constructor methods
        /*public Cheese(string name, string description)
        {
            Name = name;
            Description = description;
        }*/

        //Using a default or an empty constructor
        public Cheese() {
            CheeseId = ++nextId;
        }
       
    }
}
