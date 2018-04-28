using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string tmpString;
            string modeMenu;
            int result = 0;

            bool fail = false;

            Random rnd = new Random();
           

            welcome();

            
            Console.WriteLine("******************************************************************");
            Console.WriteLine("******************************************************************");
            Console.WriteLine("                          Регистрация");
            Console.WriteLine("                       Введите  имя  пилота");
            Console.WriteLine("******************************************************************");
            tmpString =  Console.ReadLine();
            Pilot Pilot1 = new Pilot(tmpString);

            Plain Boing = Plain.Instance;
            Boing.initPlain(Pilot1,rnd, "747");
            


            while(!fail)
            {
                Console.Clear();
                Console.WriteLine("******************************************************************");
                Console.WriteLine("******************************************************************");
                Console.WriteLine("                          Меню:");
                Console.WriteLine("     1. Добавить диспетчера.");
                if(Boing.Dispatchers.Count > 1)
                {
                    Console.WriteLine("     2. Полетели.");
                }
                Console.WriteLine();
                
                modeMenu = Console.ReadLine();

                switch (modeMenu)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("******************************************************************");
                        Console.WriteLine("******************************************************************");
                        Console.WriteLine("    Введите имя диспетчера");
                        Console.WriteLine();
                        tmpString = Console.ReadLine();

                        Boing.addDispatcher(new Dispatcher(tmpString, rnd.Next(-200,200)));
                        break;


                    case "2":
                        if(Boing.checkDispatchers())
                        {
                            try
                            {
                                Boing.flite();
                                

                                endProgram("", Pilot1, Boing.Vzlet, Boing.MaxSpeed, Boing.Posadka);
                                fail = true;
                            }
                            catch(PlainException e)
                            {
                                endProgram(e.Message, Pilot1, Boing.Vzlet, Boing.MaxSpeed, Boing.Posadka);
                                fail = true;
                            }
                        }
                        break;
                }

            }
            


            //Dispatcher Dispatcher1 = new Dispatcher();


        }

        static void endProgram(string message, Pilot Pilot, bool Vzlet, bool MaxSpeed, bool Posadka)
        {
            int result = 0;
            Console.Clear();
            Console.WriteLine("******************************************************************");
            Console.WriteLine("******************************************************************");
            if(message != "")
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Полет завершен!!!");
            }
           
            if (Vzlet && MaxSpeed && Posadka)
            {
                Console.Write("Успешная посадка");
            }
            Console.WriteLine();
            Console.WriteLine("---Итого по очкам:---");


            foreach (var points in Pilot.points)
            {
                Console.WriteLine(points.Key + " - " + points.Value);
                result += points.Value;
            }
            Console.WriteLine("Общая сумма штрафных очков по всем инструкторам - " + result);
            Console.WriteLine();
        }

        

        static void welcome()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("              ___¶¶¶____________________________________________");
            Console.WriteLine("              ____¶¶¶¶¶_________________________________________");
            Console.WriteLine("              ______¶¶¶¶¶_______________________________________");
            Console.WriteLine("              ________¶¶¶¶¶_____________________________________");
            Console.WriteLine("              __________¶¶¶¶¶¶¶¶¶_______________________________");
            Console.WriteLine("              ___________¶¶¶¶¶¶¶¶¶¶_________¶¶______¶___________");
            Console.WriteLine("              ___________¶¶¶¶¶¶¶¶¶¶_________¶¶______¶___________");
            Console.WriteLine("              _____________¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶___¶¶___________");
            Console.WriteLine("              ____________¶¶¶___¶¶¶¶¶¶¶¶¶¶_¶¶¶¶¶__¶¶____________");
            Console.WriteLine("              ____________¶¶¶__¶¶¶¶¶¶¶¶¶¶¶__¶¶¶__¶_¶¶___________");
            Console.WriteLine("              _____________¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶________¶¶¶________");
            Console.WriteLine("              ________________¶¶¶¶¶¶¶¶¶¶¶¶¶¶____________________");
            Console.WriteLine("              ________________________¶¶¶¶¶¶¶¶__________________");
            Console.WriteLine("              ___________________________¶¶¶¶¶¶¶¶_______________");
            Console.WriteLine("              ______________________________¶¶¶¶¶¶¶¶____________");
            Console.WriteLine("              ________________________________¶¶¶¶¶¶¶¶¶_________");
            Console.WriteLine("              ___________________________________¶¶¶¶¶¶¶¶_______");
            Console.WriteLine("              _______________________________________¶¶¶¶¶______");
            Console.WriteLine("              _________________________________________¶¶¶¶¶¶_¶_");
            Console.WriteLine("              __________________________________________________");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("******************************************************************");
            Console.WriteLine("         Добро пожаловать в «Тренажер пилота самолета»");
            Console.WriteLine("******************************************************************");
            Console.WriteLine("");
            Console.WriteLine("Для продолжения нажмите  клавишу Enter......");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
