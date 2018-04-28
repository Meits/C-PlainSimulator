using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainSimulator
{
    class Dispatcher : AHuman
    {
        const int maxSpeed = 1000;
        public int Adjustment { get; set; }
        public int currentHeight { get; set; }
        public int currentSpeed { get; set; }
        public int currentControlHeight { get; set; }
        public int prevControlHeight { get; set; }

        public int recomendHeight { get; set; }

        public Pilot Pilot { get; set; }

        public bool  slow = false;

        protected int points;
        public int Points
        {
            get
            {
                return points;
            }

            set
            {
                
                if (points < 1000)
                {
                    points = value;
                    
                    if(Pilot.points.ContainsKey(this.Name))
                    {
                        
                        Pilot.points[this.Name] = this.points;
                    }
                    else
                    {
                       
                        Pilot.points.Add(this.Name, this.points);
                    }
                    
                }
                else
                {
                    throw (new PlainException("Не пригоден к полетам"));
                }
            }
        }
        public Dispatcher(string name, int adjustment)
        {
            Adjustment = adjustment;
            
            Name = name;
            this.points = 0;
        }
        public void ControlFligth(object sender, FlightParamsEventArgs e)
        {

            if (e.Plain.Speed >= 50 )
            {
                Pilot = e.Pilot;
                recomendHeight = 7 * e.Plain.Speed - this.Adjustment;
                if(e.Plain.Speed > maxSpeed)
                {
                    this.Points += 100;
                    this.slow = true;
                }
                else
                {
                    this.slow = false;
                }
                if(Math.Abs(recomendHeight - e.Plain.Height) >= 300 && Math.Abs(recomendHeight - e.Plain.Height) <= 600)
                {
                    //25
                    this.Points += 25;
                }
                else if(Math.Abs(recomendHeight - e.Plain.Height) >= 600 && Math.Abs(recomendHeight - e.Plain.Height) <= 1000)
                {
                    //50
                    this.Points += 50;
                }
                else if(Math.Abs(recomendHeight - e.Plain.Height) >= 1000)
                {
                    ///crash
                    throw (new PlainException("Разбился!!!"));
                }
            }
        }
    }
}
