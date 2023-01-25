namespace projektZaliczeniowy
{
    class ChessPosition
    {
        private int row, col;

        public ChessPosition(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get { return row; }
            set { if (value >= -1 && value < 8) row = value; }
        }

        public int Col
        {
            get { return col; }
            set { if (value >= -1 && value < 8) col = value; }
        }
    }
}
