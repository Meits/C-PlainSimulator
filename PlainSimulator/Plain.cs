using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PlainSimulator { 
    class Plain : APlain
    {
        private static Plain instance;
        public static Plain Instance {
            get
            {
                if (instance == null)
                {
                    instance = new Plain();
                }
                return instance;
            }
        }

        private Plain() {
            Dispatchers = new List<Dispatcher>();
            Rnd = new Random();
            this.stepsHeight = new List<KeyValuePair<int, int>>();
            this.Steps = 0;
        }

        public void initPlain(Pilot pilot, Random rnd, string Model)
        {
            this.Pilot = pilot;
            this.Rnd = rnd;
            this.Model = Model;

            this.args = new FlightParamsEventArgs();
            this.args.Pilot = this.Pilot;
            this.args.Plain = this;

            this.Vzlet = false;
            this.MaxSpeed = false;
            this.Posadka = false;
        }

        public void addDispatcher(Dispatcher dispatcher)
        {
            this.Dispatchers.Add(dispatcher);
            this.changeFlightParams += dispatcher.ControlFligth;
        }

        public void printInterface()
        {
            short ii = 1;

            Console.Clear();
            Console.WriteLine("******************************************************************");
            Console.WriteLine("******************************************************************");
            Console.WriteLine();
            
            Console.WriteLine("      +++++++++++++++++++++++Летим!!!+++++++++++++++++++++++");
            Console.Write("      Стадия полета - ");
            if(!this.Vzlet && ! this.MaxSpeed && !this.Posadka)
            {
                Console.Write(" Влет ");
            }
            else if(this.Vzlet && !this.MaxSpeed && !this.Posadka) {
                Console.Write(" Набор высоты и скорости");
            }
            else if (this.Vzlet && this.MaxSpeed && !this.Posadka)
            {
                Console.Write(" Максимальная скорость набрана, идем на посадку");
            }
            else if (this.Vzlet && this.MaxSpeed && this.Posadka)
            {
                Console.Write("Успешная посадка");
            }
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("    Меню:");
            Console.WriteLine("1. Добавить диспетчера.");
            if(this.Dispatchers.Count > 2)
            {
                Console.WriteLine("2. Удалить диспетчера.");
            }
            
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("---Пилот---                 ---Самолет---");
            Console.WriteLine(this.Pilot.Name);
            Console.WriteLine("                        Модель - " + this.Model);
            Console.WriteLine("                        Текущая скорость - " + this.Speed);
            Console.WriteLine("                        Текущая высота - " + this.Height);
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("---Итого по очкам:---");
            foreach (var points in this.Pilot.points)
            {
                Console.WriteLine(points.Key + " - " + points.Value);
            }
            Console.WriteLine();

            Console.WriteLine("---Диспетчеры---");
            Console.WriteLine();

            foreach (Dispatcher dispatcher in this.Dispatchers)
            {
                Console.WriteLine("Диспетчер №"+ ii);
                Console.WriteLine("Имя  - " + dispatcher.Name);
                Console.WriteLine("Рекомендуемая высота " + dispatcher.recomendHeight + "       Штрафные баллы  " + dispatcher.Points);

                if (dispatcher.slow)
                {
                    Console.WriteLine("!!!! Необходимо снизить скорость !!!!");
                }
                Console.WriteLine();
                ii++;
            }

            string[] map =
                {

                    "9|                                                                                  ",
                    "8|                                                                                  ",
                    "7|                                                                                  ",
                    "6|                                                                                  ",
                    "5|                                                                                  ",
                    "4|                                                                                  ",
                    "3|                                                                                  ",
                    "2|                                                                                  ",
                    "1|                                                                                  ",
                    "0|                                                                                  ",
                    };


            int st = 3;
            foreach (var step in this.stepsHeight)
            {
                // 250 500 750 1000 1250 1500 1750 2000

                //if(Convert.ToInt32(step.Value/1000) > 0)
                //{
                    StringBuilder sb = new StringBuilder(map[(map.Length-1) - Convert.ToInt32(step.Value / 1000)]);
                    //Console.WriteLine(step.Key);
                    //if((Convert.ToInt32(step.Key / 50))  > 0)
                   // {
                        //sb[(Convert.ToInt32(step.Key / 50)) +2] = 'X';
                    //}
                   // else
                   // {
                        sb[st] = 'X';
                   // }
                    
                    map[(map.Length - 1) - Convert.ToInt32(step.Value / 1000)] = sb.ToString();
                    st++;
               // }


                //StringBuilder sb = new StringBuilder(map[i]);
                //sb[j] = 'X';
                //map[i] = sb.ToString();
                
            }


            //for (int i = map.Length - 2, k = 0; i > 0; i--, k++)
            //{

            //    for (int j = 2; j < map[i].Length; j++)
            //    {



            //        if (k <= this.stepsHeight.Count - 1 && Convert.ToInt32(this.stepsHeight[k].Value/1000) == k)
            //        {

            //            Console.WriteLine(Convert.ToInt32(this.stepsHeight[k].Value / 1000));
            //            Console.WriteLine(this.stepsHeight.Count);
            //            StringBuilder sb = new StringBuilder(map[i]);
            //            sb[j] = 'X';
            //            map[i] = sb.ToString();
            //            break;
            //        }
            //        //this.stepsHeight[j].Key;



            //    }
            //}

            for (int i = 0; i < map.Length; ++i)
            {
                Console.WriteLine(map[i]);
            }
        }

        public override void flite() {
            Console.Clear();
            Speed = 0;

            ConsoleKeyInfo key;

            // Использовать лямбда-выражение для отображения факта нажатия клавиши.
            this.KeyPress += (sender, e) =>
            {
                
                if(e.key.Key == ConsoleKey.LeftArrow && (e.key.Modifiers & ConsoleModifiers.Shift) == 0)
                {
                    this.Speed -= 50;
                }
                else if(e.key.Key == ConsoleKey.RightArrow && (e.key.Modifiers & ConsoleModifiers.Shift) == 0)
                {
                    this.Speed += 50;
                }
                else if (e.key.Key == ConsoleKey.UpArrow && (e.key.Modifiers & ConsoleModifiers.Shift) == 0)
                {
                    this.Height += 250;
                }
                else if (e.key.Key == ConsoleKey.DownArrow && (e.key.Modifiers & ConsoleModifiers.Shift) == 0)
                {
                    this.Height -= 250;
                }

                else if (e.key.Key == ConsoleKey.UpArrow && (e.key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    this.Height += 500;
                }

                else if (e.key.Key == ConsoleKey.DownArrow && (e.key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    this.Height -= 500;
                }

                else if (e.key.Key == ConsoleKey.LeftArrow && (e.key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    this.Speed -= 150;
                }

                else if (e.key.Key == ConsoleKey.RightArrow && (e.key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    this.Speed += 150;
                }
                else if(e.key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    Console.WriteLine("******************************************************************");
                    Console.WriteLine("******************************************************************");
                    Console.WriteLine("    Введите имя диспетчера");
                    Console.WriteLine();
                    string tmpString = Console.ReadLine();
                    this.addDispatcher(new Dispatcher(tmpString, this.Rnd.Next(-200, 200)));
                }

                else if (e.key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    Console.WriteLine("******************************************************************");
                    Console.WriteLine("******************************************************************");
                    Console.WriteLine("    Выберите диспетчера для удаления");
                    Console.WriteLine();
                    short ic = 1;
                    foreach(Dispatcher dispatcher in this.Dispatchers)
                    {
                        Console.WriteLine(ic + ". "+dispatcher.Name);
                        ic++;
                    }

                    string tmpString = Console.ReadLine();
                    if(tmpString != "")
                    {
                        this.deleteDispatcher(Convert.ToInt32(tmpString) - 1);
                    }
                    
                    
                }


            };
            

            while (!this.Vzlet || !this.MaxSpeed || !this.Posadka)
            {
                this.printInterface();
                key = Console.ReadKey();
                this.OnKeyPress(key);
            }
        }

        public void deleteDispatcher(int index)
        {
            if(index <= (this.Dispatchers.Count - 1) && this.Dispatchers.Count > 2)
            {
                this.changeFlightParams -= this.Dispatchers[Convert.ToInt32(index)].ControlFligth;
                this.Dispatchers.RemoveAt(Convert.ToInt32(index));
                
            }
            
        }



        public override void start()
        {
            Speed = 50;
        }
        public override void landing()
        {

        }
    }
}
