using System;
using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class ChessBoard
    {
        private int posX, posY;
        private ChessPiece[,] board = new ChessPiece[8, 8];
        private MultiLineText boardGraphic;
        private ChessPosition lastSelected = new ChessPosition(0, 0);

        public ChessBoard(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;

            string boardText =
            @"-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
8|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
7|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
6|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
5|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
4|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
3|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
2|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |      |      |      |      |      |      |      |      |
1|      |      |      |      |      |      |      |      |
-+------+------+------+------+------+------+------+------+
 |  A   |  B   |  C   |  D   |  E   |  F   |  G   |  H   |";

            boardGraphic = new MultiLineText(PosX, PosY, boardText);
            boardGraphic.BackgroundColor = ConsoleColor.Black;

            ValidMoves = new List<ChessPosition>();

            FirstPlayerColor = ConsoleColor.White;
            SecondPlayerColor = ConsoleColor.Red;
        }

        public ConsoleColor FirstPlayerColor { get; set; }
        public ConsoleColor SecondPlayerColor { get; set; }

        public int PosX
        {
            get { return posX; }
            set { if (value >= 0) posX = value; }
        }
        public int PosY
        {
            get { return posY; }
            set { if (value >= 0) posY = value; }
        }

        public List<ChessPosition> ValidMoves { get; set; }

        public ChessPiece[,] Board
        {
            get { return (ChessPiece[,])board.Clone(); }
        }

        public void SetPiece(ChessPiece piece, ChessPosition pos)
        {
            if (pos.Row < 0 || pos.Col < 0)
            {
                return;
            }

            board[pos.Row, pos.Col] = piece;
        }

        public ChessPiece GetPiece(ChessPosition pos)
        {
            if (pos.Row < 0 || pos.Col < 0)
            {
                return null;
            }

            return board[pos.Row, pos.Col];
        }

        public void ClearBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    this.SetPiece(null, new ChessPosition(row, col));
                }
            }
        }

        public ChessPosition Draw()
        {
            ChessPosition selected = new ChessPosition(lastSelected.Row, lastSelected.Col);

            ConsoleColor currentFgColor = Console.ForegroundColor;
            ConsoleColor currentBgColor = Console.BackgroundColor;
            Console.CursorVisible = false;

            ConsoleKey input;
            do
            {
                boardGraphic.Draw();
                Console.CursorVisible = false;

                DrawChessPieces();
                DrawValidMoves(ConsoleColor.DarkBlue);
                DrawSelectedField(selected, ConsoleColor.Blue);
                Console.BackgroundColor = currentBgColor;


                input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.LeftArrow && selected.Col > 0)
                {
                    selected.Col--;
                }
                else if (input == ConsoleKey.RightArrow && selected.Col < 7)
                {
                    selected.Col++;
                }
                if (input == ConsoleKey.UpArrow && selected.Row > 0)
                {
                    selected.Row--;
                }
                else if (input == ConsoleKey.DownArrow && selected.Row < 7)
                {
                    selected.Row++;
                }
            } while (input != ConsoleKey.Enter && input != ConsoleKey.Escape);

            lastSelected.Row = selected.Row;
            lastSelected.Col = selected.Col;

            Console.ForegroundColor = currentFgColor;
            Console.BackgroundColor = currentBgColor;

            Console.CursorVisible = true;
            if (input == ConsoleKey.Escape)
            {
                selected.Row = -1;
                selected.Col = -1;
                return selected;
            }

            return selected;
        }

        public void DrawReadOnly()
        {
            ChessPosition selected = new ChessPosition(lastSelected.Row, lastSelected.Col);

            ConsoleColor currentFgColor = Console.ForegroundColor;
            ConsoleColor currentBgColor = Console.BackgroundColor;

            boardGraphic.Draw();
            Console.CursorVisible = false;

            DrawChessPieces();
            DrawValidMoves(ConsoleColor.DarkBlue);
            DrawSelectedField(selected, ConsoleColor.Blue);
            Console.BackgroundColor = currentBgColor;

            Console.ForegroundColor = currentFgColor;
            Console.BackgroundColor = currentBgColor;

            Console.CursorVisible = true;
        }

        private void DrawSelectedField(ChessPosition position, ConsoleColor fieldColor)
        {
            int height = 1 + (3 * position.Row);
            int width = 2 + (7 * position.Col);

            Console.BackgroundColor = fieldColor;

            Console.SetCursorPosition(posX + width, PosY + height);
            Console.Write("".PadRight(6));
            Console.SetCursorPosition(posX + width, PosY + height + 1);
            Console.Write("".PadRight(6));

            if (this.board[position.Row, position.Col] == null)
            {
                return;
            }

            if (this.board[position.Row, position.Col].IsWhite)
            {
                Console.ForegroundColor = FirstPlayerColor;
            }
            else
            {
                Console.ForegroundColor = SecondPlayerColor;
            }

            Console.SetCursorPosition(posX + width, PosY + height);
            Console.Write(this.board[position.Row, position.Col]);
        }

        private void DrawValidMoves(ConsoleColor fieldColor)
        {
            if (this.ValidMoves is null)
            {
                return;
            }

            foreach (var validMove in ValidMoves)
            {
                DrawSelectedField(validMove, fieldColor);
            }
        }

        private void DrawChessPieces()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (this.board[row, col] == null)
                    {
                        continue;
                    }


                    if (this.board[row, col].IsWhite)
                    {
                        Console.ForegroundColor = this.FirstPlayerColor;
                    }
                    else
                    {
                        Console.ForegroundColor = this.SecondPlayerColor;
                    }

                    int height = 1 + (3 * row);
                    int width = 2 + (7 * col);

                    Console.SetCursorPosition(posX + width, PosY + height);
                    Console.Write(this.board[row, col]);
                }
            }
        }
        public void Clear()
        {
            boardGraphic.Clear();
        }
    }
}
