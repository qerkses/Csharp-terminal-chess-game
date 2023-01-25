using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class Rook : ChessPiece
    {

        public Rook(bool isWhite) : base(isWhite)
        {
            Name = "Rook";
        }

        public override List<ChessPosition> GetValidMoves(ChessPiece[,] board, ChessPosition position)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();

            for (int row = position.Row + 1; row < 8; row++)
            {
                if (board[row, position.Col] == null)
                {
                    validMoves.Add(new ChessPosition(row, position.Col));
                    continue;
                }

                if (board[row, position.Col].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(row, position.Col));
                }

                break;
            }

            for (int row = position.Row - 1; row > -1; row--)
            {
                if (board[row, position.Col] == null)
                {
                    validMoves.Add(new ChessPosition(row, position.Col));
                    continue;
                }

                if (board[row, position.Col].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(row, position.Col));
                }

                break;
            }

            for (int col = position.Col + 1; col < 8; col++)
            {
                if (board[position.Row, col] == null)
                {
                    validMoves.Add(new ChessPosition(position.Row, col));
                    continue;
                }

                if (board[position.Row, col].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(position.Row, col));
                }

                break;
            }

            for (int col = position.Col - 1; col > -1; col--)
            {
                if (board[position.Row, col] == null)
                {
                    validMoves.Add(new ChessPosition(position.Row, col));
                    continue;
                }

                if (board[position.Row, col].IsWhite != this.IsWhite)
                {
                    validMoves.Add(new ChessPosition(position.Row, col));
                }

                break;
            }

            return validMoves;
        }
    }
}
