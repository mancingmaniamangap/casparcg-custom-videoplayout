using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPlayer
{
    public class Information_media
    {
        private string Name;
        private string Layer;
        private string Delay;
        private string Server;
        
        public void setName(string name)
        {
            Name = name;
        }

        public string getName()
        {
            return Name;
        }

        public void setLayer(string layer)
        {
            Layer = layer;

            


        }

        public string getLayer()
        { 
            return Layer;   
        }

        public void setDelay(string delay)
        {
            Delay = delay;
        }

        public string getDelay()
        {
            return Delay;
        }

        public void setServer(string server)
        {
            Server = server;
        }

        public string getServer()
        {
            return Server;
        }
    }
}
