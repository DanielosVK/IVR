
using IVR.prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVR.order
{
    class Pizza : IItem
    {

        private string name = "DEFAULT_PIZZA";
        private bool customCreated = false;
        private Size size;
        private Prompt prompt;

        
        List<Topping> toppings = new List<Topping>();



        public Pizza(string name)
        {
            this.name = name;
        }

        public Pizza(string name, Prompt prompt)
        {
            this.name = name;
            this.prompt = prompt;
        }
        public Pizza() { }
         
        public Pizza(List<Topping> toppings)
        {
            this.toppings = toppings;
            customCreated = true;
        }

        public void AddTopping(Topping topping)
        {
            toppings.Add(topping);
        }
        
        public void SetSize(Size size_)
        {
            size = size_;
        }

        public string GetName()
        {
            return name;
        }
        public List<Prompt> GetPrompts()
        {
            List<Prompt> list = new List<Prompt>();
            if (customCreated)
            {
                foreach (Topping t in toppings)
                {
                    list.AddRange(t.GetPrompts());
                }
            }
            else
            {
                list.Add(prompt);
            }
            return list;
        }

    }
}
