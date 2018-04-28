using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainSimulator
{
    abstract class APlain
    {
        
        public abstract void flite();
        public abstract void start();
        public abstract void landing();
        public List<Dispatcher> Dispatchers { get; set; }

        public List<KeyValuePair<int, int>> stepsHeight { get; set; }

        public bool Vzlet { get; set; }
        public bool MaxSpeed { get; set; }
        public bool Posadka { get; set; }

        public int Steps { get; set; }
        public Pilot Pilot { get; set; }

        public Random Rnd { get; set; }
        public string Model { get; set; }

        

        protected FlightParamsEventArgs args;

        public event EventHandler<KeyEventArgs> KeyPress;

        public event EventHandler<FlightParamsEventArgs>  changeFlightParams;
        //public event EventHandler<FlightParamsEventArgs> changeHeight;

        protected int speed;
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (value > speed)
                {
                    
                    speed = value;
                    
                }
                else if(value < speed)
                {
                    if (speed > 0 && value > 0)
                    {
                        speed = value;
                    }
                    else
                    {
                        speed = 0;
                    }
                }
                this.OnChangeFlightparams(this.args);
            }
        }
        protected int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                this.stepsHeight.Add(new KeyValuePair<int, int>(this.Speed, this.Height));
                this.Steps++;
                this.OnChangeFlightparams(this.args);
            }
        }

        //methods
        public bool checkDispatchers()
        {
            if(Dispatchers.Count() >= 2)
            {
                return true;
            }
            return false;
        }


        public void OnKeyPress(ConsoleKeyInfo key)
        {
            KeyEventArgs k = new KeyEventArgs();
            if (KeyPress != null)
            {
                k.key = key;
                KeyPress(this, k);
            }
        }

        public void OnChangeFlightparams(FlightParamsEventArgs param)
        {
            if(this.Speed > 0 && this.Height > 0 && !this.Vzlet)
            {
                this.Vzlet = true;
            }
            if(this.Speed >= 1000 && !this.MaxSpeed && this.Vzlet)
            {
                this.MaxSpeed = true;
            }
            if(this.Speed == 0 && this.Height == 0 && !this.Posadka && this.Vzlet)
            {
                this.Posadka = true;
            }
            if (changeFlightParams != null)
            {
                changeFlightParams(this, param);
            }
        }
    }
}
