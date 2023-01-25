using System;

namespace projektZaliczeniowy
{
    class Menu : ConsoleGraphic
    {
        private int longestElement = 0;
        private string[] options;

        public Menu() : base(0, 0)
        {
            SelectedElementColor = ConsoleColor.Blue;
        }

        public Menu(int posX, int posY) : base(posX, posY)
        {
            SelectedElementColor = ConsoleColor.Blue;
        }

        public string[] Options
        {
            get { return options; }

            set
            {
                foreach (string element in value)
                {
                    if (element.Length > longestElement)
                    {
                        longestElement = element.Length;
                    }
                }
                options = value;
            }
        }

        public int Width { get { return longestElement; } }
        public int Height { get { return options.Length; } }

        public ConsoleColor SelectedElementColor { get; set; }

        protected override int DrawImpl()
        {
            int selected = 0;

            ConsoleKey input;
            do
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    if (i == selected)
                    {
                        Console.BackgroundColor = this.SelectedElementColor;
                        Console.WriteLine(options[i].PadRight(longestElement));
                    }
                    else
                    {
                        Console.BackgroundColor = this.BackgroundColor;
                        Console.WriteLine(options[i].PadRight(longestElement));
                    }
                }

                input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.UpArrow && selected > 0)
                {
                    selected--;
                }
                else if (input == ConsoleKey.DownArrow && selected < options.Length - 1)
                {
                    selected++;
                }

            } while (input != ConsoleKey.Enter && input != ConsoleKey.Escape);

            if (input == ConsoleKey.Escape)
            {
                selected = -1;
            }

            return selected;
        }

        protected override void ClearImpl()
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.WriteLine("".PadRight(longestElement));
            }
        }
    }
}
