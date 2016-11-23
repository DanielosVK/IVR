using IVR.order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;

namespace IVR.prompts
{
    class PromptPlayer : IPlayer
    {
        private List<Prompt> prompt_list = new List<Prompt>();
        private SoundPlayer player;
        private Thread thread;

        public PromptPlayer(List<Prompt> prompt_list_)
        {
            this.prompt_list = prompt_list_;
            player = new SoundPlayer();
            thread = new Thread(PlayList);

        }

        public PromptPlayer(Prompt prompt_)
        {
            this.prompt_list.Add(prompt_);
            player = new SoundPlayer();
            thread = new Thread(PlayList);
        }

        public PromptPlayer()
        {
            player = new SoundPlayer();
            thread = new Thread(PlayList);
        }

        public void SetPrompt(Prompt prompt_)
        {
            this.prompt_list.Add(prompt_);
        }

        public void SetPrompts(List<Prompt> prompts_)
        {
            this.prompt_list = prompts_;
        }

        public void PlayList()
        {
            foreach (Prompt p in prompt_list)
            {
                player.SoundLocation = p.path;
                player.Load();
                player.PlaySync();

            }
        }

        private void StaticPlay()
        {
            foreach (Prompt p in prompt_list)
            {
                player.SoundLocation = p.path;
                player.Load();
                player.Play();

            }
        }

        public void Play()
        {
            if (prompt_list.Count() == 1)
                StaticPlay();
            else
                ThreadPlay();
        }

        private void ThreadPlay()
        {
            thread.Start();
        }

        private void ThreadStop()
        {
            thread.Abort();
        }
        private void StaticStop()
        {
            this.player.Stop();
        }
        public void Stop()
        {
            if (prompt_list.Count() == 1)
                StaticStop();
            else
                ThreadStop();
        }

    }
}