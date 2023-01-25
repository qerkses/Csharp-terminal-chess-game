using System;

namespace projektZaliczeniowy
{
    abstract class ConsoleGraphic
    {
        protected int posX, posY;

        public ConsoleGraphic(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;

            BackgroundColor = ConsoleColor.DarkBlue;
            ForegroundColor = ConsoleColor.White;
        }

        public int PosX
        {
            get { return posX; }
            set { if (value >= 0) { posX = value; } }
        }

        public int PosY
        {
            get { return posY; }
            set { if (value >= 0) { posY = value; } }
        }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public virtual int Draw()
        {
            ConsoleColor currentBgColor = Console.BackgroundColor;
            ConsoleColor currentFgColor = Console.ForegroundColor;

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;

            Console.CursorVisible = false;
            int temp = DrawImpl();
            Console.CursorVisible = true;

            Console.BackgroundColor = currentBgColor;
            Console.ForegroundColor = currentFgColor;

            return temp;
        }

        protected abstract int DrawImpl();

        public virtual void Clear()
        {
            ConsoleColor currentBgColor = Console.BackgroundColor;
            ConsoleColor currentFgColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            ClearImpl();

            Console.BackgroundColor = currentBgColor;
            Console.ForegroundColor = currentFgColor;
        }
        protected abstract void ClearImpl();
    }
}
