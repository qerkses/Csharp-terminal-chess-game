using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class Queen : ChessPiece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            Name = "Queen";
        }

        public override List<ChessPosition> GetValidMoves(ChessPiece[,] board, ChessPosition position)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();

            Rook rook = new Rook(this.IsWhite);
            Bishop bishop = new Bishop(this.IsWhite);

            validMoves.AddRange(rook.GetValidMoves(board, position));
            validMoves.AddRange(bishop.GetValidMoves(board, position));

            return validMoves;
        }
    }
}
