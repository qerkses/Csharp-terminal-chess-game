using System.Collections.Generic;

namespace projektZaliczeniowy
{
    abstract class ChessPiece
    {
        private string name;
        public string Name
        {
            get { return name; }
            protected set { if (value.Length <= 6) name = value; }
        }
        public bool IsWhite { get; set; }
        public bool FirstMove { get; set; }

        public ChessPiece(bool isWhite)
        {
            IsWhite = isWhite;
            FirstMove = true;
            Name = "CPiece";
        }

        public abstract List<ChessPosition> GetValidMoves(ChessPiece[,] board, ChessPosition position);

        public override string ToString()
        {
            return Name;
        }
    }
}
