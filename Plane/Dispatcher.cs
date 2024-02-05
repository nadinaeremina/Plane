using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane
{
    class Dispatcher
    {
        public string Name { get; set; } // имя
        public int Penalty_points { get; set; } // штрафные баллы
        public int Weather_correction { get; set; } // корректировка погодных условий
        public int Number { get; set; } // порядковый номер
        public Dispatcher(string _name) // конструктор
        {
            Name = _name;
            Random rand = new Random();
            Weather_correction = rand.Next(-200, 200);
        }
        public void Recommended_Height(int _speed, int _height) // рекомендуемая высота
        {
            int x = 0, y = Number + 32; // координаты вывода сообщений

            int rec_height = 7 * _speed - Weather_correction; // формула нахождения реком. высоты

            int difference; // разница м/у реальной высотой и реком.

            if (_height > rec_height) difference = _height - rec_height; // находение разницы
            else difference = rec_height - _height;

            // затирка предыдущего сообщения
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("*********************************************************************************************************");
            Console.ResetColor();
            // вывод сообщения диспетчера
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"Диспетчер {Name}: Рекомендуемая высота полета: {rec_height} м. Кол-во штрафных балоов: {Penalty_points}");

            // предупреждение
            if (_speed > 1000)
            {
                Penalty_points += 100;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("*********************************************************************************************************");
                Console.ResetColor();
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"Диспетчер {Name}: Немедленно снизьте скорость!");
                Console.Beep();
            }

            // начисление штрафных баллов
            if (difference >= 300 && difference < 600) Penalty_points += 25;
            if (difference >= 600 && difference < 1000) Penalty_points += 50;
            if (difference > 1000)
            {
                Console.SetCursorPosition(x, this.Number + y + 6);
                throw Crashed("Самолет разбился!");
            }
            if (Penalty_points >= 1000)
            {
                Console.SetCursorPosition(x, this.Number + y + 5);
                throw Unsuitable("Непригоден к полетам!");
            }
        }
    }
}
