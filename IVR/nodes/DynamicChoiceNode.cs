using IVR.prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVR.nodes
{
    class DynamicChoiceNode : Node
    {
        private Prompt static_in;
        private Prompt static_out;
        private bool mode;
        private string message;
        public DynamicChoiceNode(string nodeName, string message, Call callOwner, bool mode, Prompt static_in, Prompt static_out)
            : base(nodeName, callOwner)
        {
            this.nodeName = nodeName;
            this.callOwner = callOwner;
            this.message = message;
            this.mode = mode;
            this.static_in = static_in;
            this.static_out = static_out;
        }



        protected override void OnEntry()
        {

            CreatePrompt();
            player.Play();
            Console.WriteLine(message);

        }

        public override void OnKeyboard(char input)
        {
            int i = (input - '0');


            if (i < 1 || i > this.GetChildren().Count())
            {
                Console.WriteLine("podano nieporpawn znak sporboj jeszcze raz");
                Finish(this.GetNodeName());
            }
            else
            {
                Finish(GetChildren()[i - 1].GetNodeName());
            }
        }

        private void CreatePrompt()
        {

            List<prompts.Prompt> p = new List<prompts.Prompt>();
            if (mode) //current pizza
            {
                p.Add(static_in);
                p.AddRange(callOwner.GetPizza().GetPrompts());
                p.Add(static_out);
            }
            else if (!mode) //order
            {
                int i = 0;
                p.Add(static_in); //twoje zamuwienie zawiera
                foreach (Pizza pitca in callOwner.items)
                {
                    p.Add(new Prompt("pizze.wav"));
                    if (pitca.customCreated)
                    {
                        p.Add(new Prompt("ze składnikami.wav"));
                        p.AddRange(pitca.GetPrompts());
                        p.Add(new Prompt("o rozmiarze.wav"));
                        p.AddRange(pitca.size.GetPrompts());
                    }
                    else
                    {
                        p.AddRange(pitca.GetPrompts());
                        p.Add(new Prompt("o rozmiarze.wav"));
                        p.AddRange(pitca.size.GetPrompts());
                    }
                    i++;
                    if (i < callOwner.items.Count())
                        p.Add(new Prompt("oraz.wav"));
                }
                p.Add(static_out);
            }
            else Console.WriteLine("ehh :/");
            player.SetPrompts(p);
        }
    }
}
