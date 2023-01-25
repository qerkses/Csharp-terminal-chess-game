using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class Knight : ChessPiece
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            Name = "Knight";
        }

        public override List<ChessPosition> GetValidMoves(ChessPiece[,] board, ChessPosition position)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();

            ChessPosition[] possible_moves =
            {
                new ChessPosition(position.Row - 2, position.Col - 1),
                new ChessPosition(position.Row - 2, position.Col + 1),
                new ChessPosition(position.Row + 2, position.Col - 1),
                new ChessPosition(position.Row + 2, position.Col + 1),
                new ChessPosition(position.Row - 1, position.Col - 2),
                new ChessPosition(position.Row + 1, position.Col - 2),
                new ChessPosition(position.Row - 1, position.Col + 2),
                new ChessPosition(position.Row + 1, position.Col + 2),
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
                    validMoves.Add(move);
                }
                else if (board[move.Row, move.Col].IsWhite != this.IsWhite)
                {
                    validMoves.Add(move);
                }
            }

            return validMoves;
        }
    }
}
