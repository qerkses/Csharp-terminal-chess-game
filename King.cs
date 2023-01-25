using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class King : ChessPiece
    {
        public King(bool isWhite) : base(isWhite)
        {
            Name = "King";
        }

        public override List<ChessPosition> GetValidMoves(ChessPiece[,] board, ChessPosition position)
        {
            List<ChessPosition> finalValidMoves = new List<ChessPosition>();
            List<ChessPosition> almostValidMoves = new List<ChessPosition>();

            ChessPosition[] possible_moves =
            {
                new ChessPosition(position.Row - 1, position.Col),
                new ChessPosition(position.Row - 1, position.Col - 1),
                new ChessPosition(position.Row - 1, position.Col + 1),
                new ChessPosition(position.Row + 1, position.Col),
                new ChessPosition(position.Row + 1, position.Col - 1),
                new ChessPosition(position.Row + 1, position.Col + 1),
                new ChessPosition(position.Row, position.Col - 1),
                new ChessPosition(position.Row, position.Col + 1),
            };

            foreach (ChessPosition move in possible_moves)
            {
                if (move.Row < 0 || move.Row > 7)
                {
                    continue;
                }

                if (move.Col < 0 || move.Col > 7)
                {
                    continue;
                }

                if (board[move.Row, move.Col] == null)
                {
                    almostValidMoves.Add(move);
                }
                else if (board[move.Row, move.Col].IsWhite != this.IsWhite)
                {
                    almostValidMoves.Add(move);
                }
            }

            ChessPosition secondKingPosition = new ChessPosition(-1, -1);
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] == null)
                    {
                        continue;
                    }

                    if (board[row, col].GetType() != this.GetType())
                    {
                        continue;
                    }

                    if (board[row, col].IsWhite != this.IsWhite)
                    {
                        secondKingPosition = new ChessPosition(row, col);
                        break;
                    }
                }
            }

            if (secondKingPosition.Col == -1 && secondKingPosition.Row == -1)
            {
                return almostValidMoves;
            }

            foreach (ChessPosition move in almostValidMoves)
            {
                if (System.Math.Abs(move.Row - secondKingPosition.Row) > 1)
                {
                    finalValidMoves.Add(move);
                }
                else if (System.Math.Abs(move.Col - secondKingPosition.Col) > 1)
                {
                    finalValidMoves.Add(move);
                }
            }

            return finalValidMoves;
        }
    }
}
