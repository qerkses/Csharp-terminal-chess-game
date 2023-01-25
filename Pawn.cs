using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class Pawn : ChessPiece
    {
        public Pawn(bool isWhite) : base(isWhite)
        {
            Name = "Pawn";
        }

        public override List<ChessPosition> GetValidMoves(ChessPiece[,] board, ChessPosition position)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();
            int direction = -1;

            if (!(this.IsWhite))
            {
                direction = 1;
            }

            if (position.Row + (1 * direction) < 0 || position.Row + (1 * direction) > 7)
            {
                return validMoves;
            }

            if (board[position.Row + (1 * direction), position.Col] == null)
            {
                validMoves.Add(new ChessPosition(position.Row + (1 * direction), position.Col));
            }


            if (position.Row + (2 * direction) > -1 && position.Row + (2 * direction) < 8)
            {
                if (this.FirstMove && validMoves.Count > 0)
                {
                    if ((board[position.Row + (2 * direction), position.Col] == null))
                    {
                        validMoves.Add(new ChessPosition(position.Row + (2 * direction), position.Col));
                    }
                }
            }

            if ((position.Col - 1 > -1 && position.Col - 1 < 8))
            {
                if (board[position.Row + (1 * direction), position.Col - 1] != null)
                {
                    if (board[position.Row + (1 * direction), position.Col - 1].IsWhite != this.IsWhite)
                    {
                        validMoves.Add(new ChessPosition(position.Row + (1 * direction), position.Col - 1));
                    }
                }
            }

            if (position.Col + 1 > -1 && position.Col + 1 < 8)
            {
                if (board[position.Row + (1 * direction), position.Col + 1] != null)
                {
                    if (board[position.Row + (1 * direction), position.Col + 1].IsWhite != this.IsWhite)
                    {
                        validMoves.Add(new ChessPosition(position.Row + (1 * direction), position.Col + 1));
                    }
                }
            }

            return validMoves;
        }
    }
}
