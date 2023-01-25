using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class Bishop : ChessPiece
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            Name = "Bishop";
        }

        public override List<ChessPosition> GetValidMoves(ChessPiece[,] board, ChessPosition position)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();

            int i = 1;
            while (position.Row + i < 8 && position.Col + i < 8)
            {
                if (board[position.Row + i, position.Col + i] == null)
                {
                    validMoves.Add(new ChessPosition(position.Row + i, position.Col + i));
                    i++;
                    continue;
                }

                if (board[position.Row + i, position.Col + i].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(position.Row + i, position.Col + i));
                }

                break;
            }

            i = 1;
            while (position.Row + i < 8 && position.Col - i > -1)
            {
                if (board[position.Row + i, position.Col - i] == null)
                {
                    validMoves.Add(new ChessPosition(position.Row + i, position.Col - i));
                    i++;
                    continue;
                }

                if (board[position.Row + i, position.Col - i].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(position.Row + i, position.Col - i));
                }

                break;
            }

            i = 1;
            while (position.Row - i > -1 && position.Col + i < 8)
            {
                if (board[position.Row - i, position.Col + i] == null)
                {
                    validMoves.Add(new ChessPosition(position.Row - i, position.Col + i));
                    i++;
                    continue;
                }

                if (board[position.Row - i, position.Col + i].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(position.Row - i, position.Col + i));
                }

                break;
            }

            i = 1;
            while (position.Row - i > -1 && position.Col - i > -1)
            {
                if (board[position.Row - i, position.Col - i] == null)
                {
                    validMoves.Add(new ChessPosition(position.Row - i, position.Col - i));
                    i++;
                    continue;
                }

                if (board[position.Row - i, position.Col - i].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(position.Row - i, position.Col - i));
                }

                break;
            }

            return validMoves;
        }
    }
}
