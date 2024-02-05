using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plane
{
    class Plane
    {
        public List<Dispatcher> dispatchers; // список диспетчеров

        public int Speed { set; get; } // текущая скорость

        public int Height { set; get; } // текущая высота

        public int Total_penalty_points { set; get; } // общее количество штрафных баллов

        delegate void Management(int speed, int hight); // делегат 

        event Management Event_Management; // событие

        public Plane() // конструктор
        {
            dispatchers = new List<Dispatcher>();
        }
        void Add_Dispatcher(string _name) // добавление диспетчера
        {
            Dispatcher dispatcher = new Dispatcher(_name);
            dispatcher.Number = dispatchers.Count;
            Event_Management += dispatcher.Recommended_Height;
            dispatchers.Add(dispatcher);
            Console.WriteLine($"Диспетчер {_name} добавлен!\n");
        }
        void Change_Dispatcher(int _position) // удаление диспетчера
        {
            if (_position >= 0 && _position <= dispatchers.Count - 1)
            {
                Event_Management -= dispatchers[_position].Recommended_Height;
                Console.WriteLine($"Диспетчер {dispatchers[_position].Name} удален!\a");
                Total_penalty_points += dispatchers[_position].Penalty_points;
                dispatchers.RemoveAt(_position);
                for (int i = 0; i < dispatchers.Count; i++)
                {
                    dispatchers[i].Number = i;
                }
            }
            else
            {
                Console.WriteLine("Некорректный номер!");
                Console.Beep();
            }
        }
        void Print_Plane(int _x, int _y) // отрисовка самолета
        {
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(_x, _y + i);
                for (int j = 0; j < 3; j++)
                {
                    if ((i == 0 && j == 0) || (i == 2 && j == 0) || (i == 0 && j == 2) || (i == 2 && j == 2))
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("0");
                    }
                    else if (i == 1) Console.Write("-");
                    else if (i == 0 && j == 1) Console.Write("\\");
                    else if (i == 2 && j == 1) Console.Write("/");
                    Console.ResetColor();
                }
                Console.Write("\n");
            }
        }
        void Del_Plane(int _x, int _y) // затирка самолета
        {
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(_x, _y + i);
                for (int j = 0; j < 3; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("0");
                }
                Console.Write("\n");
            }
            Console.ResetColor();
        }
        void Print_Dispatchers() // вывод списка диспетчеров
        {
            if (dispatchers.Count > 0)
            {
                foreach (Dispatcher i in dispatchers)
                    Console.WriteLine(i.Name);
            }
        }
        void Del_Dispatchers() // затирка списка диспетчеров
        {
            if (dispatchers.Count > 0)
            {
                for (int i = 0; i <= dispatchers.Count; i++)
                    Console.WriteLine("*************************");
            }
        }
        public void Fly() // полет
        {
            string delimeter = "__________________________________________________________________________________________________";

            // управление
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Скорость изменяется клавишами-стрелками Left и Right:\n(Right: +50 км/ч, Left: –50 км/ч, Shift-Right: +150 км/ч, Shift-Left: –150 км/ч)");
            Console.WriteLine("\nВысота изменяется клавишами-стрелками Up и Down: \n(Up: +250 м, Down: –250 м, Shift-Up: +500 м, Shift-Down: –500 м");
            Console.WriteLine("\nЗадача: взлететь на самолете, набрать максимальную (1000 км/ч.) скорость, а затем посадить самолет");

            ConsoleKeyInfo push;

            // создаем минимальный список диспетчеров
            Console.SetCursorPosition(0, 13);
            Console.Write($"Введите имя первого диспетчера: \n");
            Console.SetCursorPosition(0, 15);
            Add_Dispatcher(Console.ReadLine());
            Console.SetCursorPosition(0, 18);
            Console.Write($"Введите имя второго диспетчера: \n");
            Console.SetCursorPosition(0, 20);
            Add_Dispatcher(Console.ReadLine());
            Console.SetCursorPosition(0, 23);
            Console.WriteLine("Начинаем полет!");
            Thread.Sleep(1000);
            Console.Clear();

            Console.WriteLine("Скорость изменяется клавишами-стрелками Left и Right:\n(Right: +50 км/ч, Left: –50 км/ч, Shift-Right: +150 км/ч, Shift-Left: –150 км/ч)");
            Console.WriteLine("\nВысота изменяется клавишами-стрелками Up и Down: \n(Up: +250 м, Down: –250 м, Shift-Up: +500 м, Shift-Down: –500 м");
            Console.WriteLine("\nЗадача: взлететь на самолете, набрать максимальную (1000 км/ч.) скорость, а затем посадить самолет");

            int x_fly = 0, y_fly = 26, x = 0, y = 32, temp_hight = 0, temp_speed = 0, index = -1, temp_penalty_points = 0;
            bool IsMaxSpeed = false, IsFly = false, begin = false, end = false;

            Console.SetCursorPosition(0, 29);
            Console.WriteLine(delimeter);

            while (end == false)
            {
                Print_Plane(x_fly, y_fly); // отрисовка самолета
               
                push = Console.ReadKey();

                temp_hight = Height; // сохраняем текущее положение самолета
                temp_speed = Speed;  // сохраняем текущую скорость самолета

                // считываем нажатие клавиши и меняем значение переменной
                if ((push.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    if (push.Key == ConsoleKey.RightArrow) Speed += 150;
                    else if (push.Key == ConsoleKey.LeftArrow) Speed -= 150;
                    else if (push.Key == ConsoleKey.UpArrow) Height += 500;
                    else if (push.Key == ConsoleKey.DownArrow) Height -= 500;
                }
                else
                {
                    if (push.Key == ConsoleKey.RightArrow) Speed += 50;
                    else if (push.Key == ConsoleKey.LeftArrow) Speed -= 50;
                    else if (push.Key == ConsoleKey.UpArrow) Height += 250;
                    else if (push.Key == ConsoleKey.DownArrow) Height -= 250;
                    else if (push.Key == ConsoleKey.OemPlus || push.Key == ConsoleKey.Add)
                    {
                        // добавление нового диспетчера 
                        Console.SetCursorPosition(x, dispatchers.Count + y + 4);
                        Console.Write($"Введите имя диспетчера: ");
                        Add_Dispatcher(Console.ReadLine());
                        Thread.Sleep(2000);
                        Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 2);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(x, dispatchers.Count + y + 1);
                        Console.Write(delimeter);
                        Console.SetCursorPosition(x, dispatchers.Count + y + 3);
                        Console.Write(delimeter);
                        Console.SetCursorPosition(x, dispatchers.Count + y + 4);
                        Console.Write(delimeter);
                        Console.ResetColor();
                    }
                    else if (push.Key == ConsoleKey.OemMinus || push.Key == ConsoleKey.Subtract)
                    {
                        Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 4);
                        if (dispatchers.Count == 2)
                        {
                            Console.WriteLine("У вас не может быть меньше 2-ух диспетчеров!");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 4);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(delimeter);
                        }
                        else
                        {
                            // удаление существующего диспетчера
                            Print_Dispatchers();
                            Console.SetCursorPosition(x, dispatchers.Count * 2 + y + 3);
                            Console.Write($"\nВведите номер диспетчера, которого хотите заменить: ");
                            index = Convert.ToInt32(Console.ReadLine()) - 1;
                            Change_Dispatcher(index);
                            Thread.Sleep(2000);
                            Console.ForegroundColor = ConsoleColor.Black;

                            if (index == 0) Console.SetCursorPosition(x, y);
                            if (index == dispatchers.Count - 1) Console.SetCursorPosition(x, dispatchers.Count + y);

                            Console.Write(delimeter);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(x, dispatchers.Count + y + 3);
                            Console.WriteLine(delimeter);
                            Console.SetCursorPosition(x, dispatchers.Count + y + 4);
                            Del_Dispatchers();
                            Console.SetCursorPosition(x, dispatchers[0].Number + 32);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(delimeter);
                            Console.WriteLine(delimeter);
                            Console.WriteLine(delimeter);
                            Console.WriteLine(delimeter);
                            Console.SetCursorPosition(x, dispatchers.Count * 2 + y + 6);
                            Console.WriteLine(delimeter);
                            Console.WriteLine(delimeter);
                            Console.ResetColor();
                        }
                    }
                }

                // затирка самолета со старых координат
                Del_Plane(x_fly, y_fly);

                // изменение положение самолета 
                if (Height % 750 == 0)
                {
                    if (temp_hight > Height) y_fly++;
                    else if (temp_hight < Height) y_fly--;
                }
                   
                x_fly++;

                if (Speed >= 50)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 3);
                    Console.WriteLine(delimeter);
                    Console.ResetColor();
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 3);

                    temp_penalty_points = 0; // обнуление штрафных баллов

                    // подсчет штрафных баллов
                    foreach (Dispatcher i in dispatchers)
                        temp_penalty_points += i.Penalty_points;

                    // вывод информации о высоте, скорости и штрафных баллах
                    Console.WriteLine($"Скорость: {Speed} км/ч Высота: {Height} м Штрафные очки: {temp_penalty_points+Total_penalty_points}\n");

                    if (begin == false && IsMaxSpeed == false)
                    {
                        // подключаются диспетчеры
                        Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y);
                        Console.WriteLine("Начинается управление диспетчерами!\a");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 1);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(delimeter);
                        Console.ResetColor();
                        begin = true;
                    }

                    Event_Management(Speed, Height); // вызов события, взаимодействие самолета и диспетчеров

                    if (Speed == 1000)
                    {
                        // набрали максим. скорость
                        IsMaxSpeed = true;
                        Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 2);
                        Console.WriteLine("Вы набрали максимальную скорость. Ваша задача - посадить самолет!\a");
                        Thread.Sleep(1500);
                    }
                }
                if (IsMaxSpeed == true && Speed < 50)
                {
                    // отключаются диспетчеры
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 1);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(delimeter);
                    Console.ResetColor();
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 2);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(delimeter);
                    Console.ResetColor();
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 2);
                    Console.WriteLine("Закончилось управление диспетчерами!");
                    begin = false;

                    temp_penalty_points = 0; // обнуление штрафных баллов

                    // подсчет штрафных баллов
                    foreach (Dispatcher i in dispatchers)
                        temp_penalty_points += i.Penalty_points;

                    // вывод информации о высоте, скорости и штрафных баллах
                    Console.WriteLine($"Скорость: {Speed} км/ч Высота: {Height} м Штрафные очки: {temp_penalty_points+Total_penalty_points}\n");

                    // отписка от события
                    foreach (Dispatcher item in dispatchers)
                        Event_Management -= item.Recommended_Height;
                }
                if (begin == true && Height > 0) IsFly = true; // оторвались от земли, больше не касаемся
                if (begin == true && IsFly == true && (Speed == 0 || Height == 0))
                {
                    Console.SetCursorPosition(x, dispatchers.Count + y + 6);
                    throw Crashed("Самолет разбился!");
                }
                if (begin == false && IsFly == true && Height <= 0 && Speed <= 0)
                {
                    // экзамен сдан
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 2);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(delimeter);
                    Console.ResetColor();
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 2);
                    Console.WriteLine("Ура! Вы посадили самолет!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(delimeter);
                    Console.ResetColor();
                    Console.SetCursorPosition(x, dispatchers[dispatchers.Count - 1].Number + y + 3);
                    Total_penalty_points += temp_penalty_points;
                    Console.WriteLine($"Общая сумма штрафных очков: {Total_penalty_points}");
                    end = true;
                    break;
                }
            }
        }
    }
}






