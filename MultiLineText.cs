using System;

namespace projektZaliczeniowy
{
    class MultiLineText : ConsoleGraphic
    {
        private string text;
        private string[] textLines;

        public MultiLineText(string text) : base(0, 0)
        {
            Text = text;
        }

        public MultiLineText(int posX, int posY, string text) : base(posX, posY)
        {
            Text = text;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                textLines = text.Split('\n');

                foreach (string line in textLines)
                {
                    if (line.Length - 1 > Width)
                    {
                        Width = line.Length - 1;
                    }
                }

                Height = textLines.Length;
            }
        }

        protected override int DrawImpl()
        {
            Console.SetCursorPosition(posX, posY);

            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.WriteLine("".PadRight(Width));
            }

            int lineCount = 0;
            foreach (string line in textLines)
            {
                Console.SetCursorPosition(posX, posY + lineCount);
                lineCount++;
                Console.Write(line);
            }
            return 0;
        }

        protected override void ClearImpl()
        {
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.WriteLine("".PadRight(Width));
            }
        }
    }
}
