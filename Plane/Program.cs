using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Plane
{
    class Crashed : Exception
    {
        public Crashed(string message)
            : base(message)
        { }
    }
    class Unsuitable : Exception
    {
        public Unsuitable(string message)
            : base(message)
        { }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // приветствие, регистрация
            Console.SetCursorPosition(35, 17);
            Console.WriteLine("Введите имя:");
            Console.SetCursorPosition(38, 19);
            string name = Console.ReadLine();
            Console.SetCursorPosition(32, 21);
            Console.WriteLine($"Давай начинать, {name}!");
            Thread.Sleep(2000);
            Console.Clear();
            string path = @"C:\Users\Nadya\Desktop\Data.txt", penalty_points, total_penalty_points;

            try
            {
                Plane plane = new Plane();
                plane.Fly();

                // сохранение имени пилота и его штрафных баллов
                using (StreamWriter st = new StreamWriter(File.Open(path, FileMode.Append)))
                {
                    st.Write($"Pilot name: {name}, ");
                    foreach (Dispatcher item in plane.dispatchers)
                    {
                        st.Write($"Dispatcher name: {item.Name}, ");
                        penalty_points = Convert.ToString(item.Penalty_points);
                        st.Write($"Penalty points: {penalty_points}, ");
                    }
                    total_penalty_points = Convert.ToString(plane.Total_penalty_points);
                    st.Write($"Total penalty points: {total_penalty_points}");
                    st.WriteLine();
                }
            }
            catch (Crashed ex)
            {
                Console.WriteLine(ex.Message);
                Console.Beep();
            }
            catch (Unsuitable ex)
            {
                Console.WriteLine(ex.Message);
                Console.Beep();
            }
        }
    }
}

