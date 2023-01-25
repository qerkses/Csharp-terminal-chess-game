using System;

namespace projektZaliczeniowy
{
    class Border : ConsoleGraphic
    {
        private int width = 2, height = 2;

        public Border(int width, int height, char symbol) : this(0, 0, width, height, symbol)
        {

        }

        public Border(int posX, int posY, int width, int height, char symbol) : base(posX, posY)
        {
            Symbol = symbol;
            CornerSymbol = symbol;
            Width = width;
            Height = height;
        }

        public char Symbol { get; set; }
        public char CornerSymbol { get; set; }

        public int Width
        {
            get { return width; }
            set { if (value > 1) width = value; }
        }

        public int Height
        {
            get { return height; }
            set { if (value > 1) height = value; }
        }

        protected override int DrawImpl()
        {
            Console.SetCursorPosition(posX, posY);

            Console.Write(CornerSymbol);
            Console.Write(new string(Symbol, width - 2));
            Console.WriteLine(CornerSymbol);

            for (int i = 0; i < height - 1; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.Write(Symbol);
                Console.SetCursorPosition(posX + width - 1, posY + i);
                Console.WriteLine(Symbol);
            }

            Console.SetCursorPosition(posX, posY + height - 1);
            Console.Write(CornerSymbol);
            Console.Write(new string(Symbol, width - 2));
            Console.WriteLine(CornerSymbol);

            return 0;
        }
        protected override void ClearImpl()
        {
            Console.SetCursorPosition(posX, posY);

            Console.Write("".PadRight(width));

            for (int i = 0; i < height - 1; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.Write(" ");
                Console.SetCursorPosition(posX + width - 1, posY + i);
                Console.WriteLine(" ");
            }

            Console.SetCursorPosition(posX, posY + height - 1);
            Console.Write("".PadRight(width));
        }
    }
}
