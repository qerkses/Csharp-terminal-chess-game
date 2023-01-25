using System;
using System.Collections.Generic;

namespace projektZaliczeniowy
{
    class ChessGameStandard
    {
        private int posX, posY;
        private ChessBoard gameBoard;
        public ChessGameStandard(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
            gameBoard = new ChessBoard(PosX + 2, PosY + 1);
        }

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
        public virtual int StartGame()
        {
            gameBoard.ClearBoard();
            FillBoard();

            bool gameIsRunning = true;
            bool whiteTurn = true;
            ChessPosition selectedPiecePos, move;
            ChessPiece selectedPiece;
            List<ChessPosition> validMoves;
            List<ChessPosition> castlingMoves = new List<ChessPosition>();

            int winner = -1;
            while (gameIsRunning)
            {
                if (!(CanPlayerMove(gameBoard.Board, whiteTurn)))
                {
                    winner = 0;
                    if (IsKingInCheck(gameBoard.Board, whiteTurn))
                    {
                        whiteTurn = !whiteTurn;
                        if (whiteTurn)
                        {
                            winner = 1;
                        }
                        else
                        {
                            winner = 2;
                        }
                    }
                    break;
                }

                do
                {
                    selectedPiecePos = gameBoard.Draw();
                    selectedPiece = gameBoard.GetPiece(selectedPiecePos);

                    if (selectedPiecePos.Row == -1 && selectedPiecePos.Col == -1)
                    {
                        if (PauseScreen())
                        {
                            gameBoard.Clear();
                            return winner;
                        }
                    }
                }
                while (selectedPiece == null);

                if (selectedPiece.IsWhite != whiteTurn)
                {
                    continue;
                }

                validMoves = selectedPiece.GetValidMoves(gameBoard.Board, selectedPiecePos);

                validMoves = RemoveNotValidMoves(gameBoard.Board, selectedPiecePos, validMoves);

                if (selectedPiece.GetType() == typeof(King))
                {
                    if (!(IsKingInCheck(gameBoard.Board, whiteTurn)))
                    {
                        castlingMoves = CastlingValidMoves(gameBoard.Board, selectedPiecePos);
                    }
                }

                gameBoard.ValidMoves = validMoves;
                gameBoard.ValidMoves.AddRange(castlingMoves);

                bool moveIsValid = false;
                do
                {
                    move = gameBoard.Draw();

                    if (move.Row == -1 && move.Col == -1)
                    {
                        castlingMoves.Clear();
                        gameBoard.ValidMoves.Clear();
                        break;
                    }

                    foreach (var validMove in validMoves)
                    {
                        if (move.Row == validMove.Row)
                        {
                            if (move.Col == validMove.Col)
                            {
                                moveIsValid = true;
                                break;
                            }
                        }
                    }
                }
                while (!moveIsValid);

                if (!moveIsValid)
                {
                    continue;
                }

                bool castling = false;
                foreach (ChessPosition validMove in castlingMoves)
                {
                    if (move.Row == validMove.Row)
                    {
                        if (move.Col == validMove.Col)
                        {
                            castling = true;
                        }
                    }
                }

                if (castling)
                {
                    CastlingMove(selectedPiecePos, move);
                }

                selectedPiece.FirstMove = false;
                gameBoard.SetPiece(selectedPiece, move);
                gameBoard.SetPiece(null, selectedPiecePos);
                castlingMoves.Clear();
                gameBoard.ValidMoves.Clear();

                if (selectedPiece.GetType() == typeof(Pawn))
                {
                    if (move.Row == 0 || move.Row == 7)
                    {
                        gameBoard.DrawReadOnly();
                        PawnPromotion(move);
                    }
                }

                whiteTurn = !whiteTurn;
            }
            gameBoard.Clear();
            return winner;
        }

        public void DrawBoardReadOnly()
        {
            this.gameBoard.DrawReadOnly();
        }

        public void Clear()
        {
            this.gameBoard.Clear();
        }

        private void FillBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                gameBoard.SetPiece(new Pawn(false), new ChessPosition(1, i));
                gameBoard.SetPiece(new Pawn(true), new ChessPosition(6, i));
            }

            gameBoard.SetPiece(new Rook(false), new ChessPosition(0, 0));
            gameBoard.SetPiece(new Knight(false), new ChessPosition(0, 1));
            gameBoard.SetPiece(new Bishop(false), new ChessPosition(0, 2));
            gameBoard.SetPiece(new Queen(false), new ChessPosition(0, 3));
            gameBoard.SetPiece(new King(false), new ChessPosition(0, 4));
            gameBoard.SetPiece(new Bishop(false), new ChessPosition(0, 5));
            gameBoard.SetPiece(new Knight(false), new ChessPosition(0, 6));
            gameBoard.SetPiece(new Rook(false), new ChessPosition(0, 7));

            gameBoard.SetPiece(new Rook(true), new ChessPosition(7, 0));
            gameBoard.SetPiece(new Knight(true), new ChessPosition(7, 1));
            gameBoard.SetPiece(new Bishop(true), new ChessPosition(7, 2));
            gameBoard.SetPiece(new Queen(true), new ChessPosition(7, 3));
            gameBoard.SetPiece(new King(true), new ChessPosition(7, 4));
            gameBoard.SetPiece(new Bishop(true), new ChessPosition(7, 5));
            gameBoard.SetPiece(new Knight(true), new ChessPosition(7, 6));
            gameBoard.SetPiece(new Rook(true), new ChessPosition(7, 7));
        }

        private List<ChessPosition> RemoveNotValidMoves(ChessPiece[,] board, ChessPosition position, List<ChessPosition> validMoves)
        {
            List<ChessPosition> finalValidMoves = new List<ChessPosition>();
            ChessPiece temp, temp2;

            temp = board[position.Row, position.Col];
            foreach (ChessPosition move in validMoves)
            {
                temp2 = board[move.Row, move.Col];

                board[move.Row, move.Col] = board[position.Row, position.Col];
                board[position.Row, position.Col] = null;

                if (!(IsKingInCheck(board, temp.IsWhite)))
                {
                    finalValidMoves.Add(move);
                }

                board[move.Row, move.Col] = temp2;
                board[position.Row, position.Col] = temp;
            }
            return finalValidMoves;
        }

        private bool CanPlayerMove(ChessPiece[,] board, bool isWhite)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();

            ChessPiece temp, temp2;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] == null)
                    {
                        continue;
                    }

                    if (board[row, col].IsWhite != isWhite)
                    {
                        continue;
                    }

                    validMoves = board[row, col].GetValidMoves(board, new ChessPosition(row, col));
                    temp = board[row, col];

                    foreach (ChessPosition move in validMoves)
                    {
                        temp2 = board[move.Row, move.Col];
                        board[move.Row, move.Col] = board[row, col];
                        board[row, col] = null;

                        if (!(IsKingInCheck(board, isWhite)))
                        {
                            return true;
                        }

                        board[move.Row, move.Col] = temp2;
                        board[row, col] = temp;
                    }
                }
            }
            return false;
        }

        private bool IsKingInCheck(ChessPiece[,] board, bool isWhite)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();

            ChessPosition kingPosition = new ChessPosition(-1, -1);
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] != null)
                    {
                        if (board[row, col].GetType() == typeof(King))
                        {
                            if (board[row, col].IsWhite == isWhite)
                            {
                                kingPosition = new ChessPosition(row, col);
                                break;
                            }
                        }
                    }
                }
            }

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] == null)
                    {
                        continue;
                    }

                    if (board[row, col].IsWhite != isWhite)
                    {
                        validMoves = board[row, col].GetValidMoves(board, new ChessPosition(row, col));
                    }

                    foreach (ChessPosition move in validMoves)
                    {
                        if (move.Row == kingPosition.Row)
                        {
                            if (move.Col == kingPosition.Col)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool PauseScreen()
        {
            Menu pauseMenu = new Menu(this.posX + 65, this.posY + 4);

            pauseMenu.Options = new string[]
            {
                "  Pause Menu  ",
                "<< Resume >>",
                "<< Quit >>",
            };

            Border pauseMenuBorder = new Border(
                pauseMenu.PosX - 2,
                pauseMenu.PosY - 2,
                pauseMenu.Width + 4,
                pauseMenu.Height + 4, '=');

            pauseMenuBorder.BackgroundColor = ConsoleColor.Black;

            pauseMenuBorder.Draw();
            switch (pauseMenu.Draw())
            {
                case 2:
                    pauseMenuBorder.Clear();
                    pauseMenu.Clear();
                    return true;
                default:
                    pauseMenuBorder.Clear();
                    pauseMenu.Clear();
                    return false;
            }
        }

        private void PawnPromotion(ChessPosition position)
        {
            Menu promotionMenu = new Menu(this.posX + 65, this.posY + 4);

            promotionMenu.Options = new string[]
            {
                "<< Queen >>",
                "<< Rook >>",
                "<< Bishop >>",
                "<< Knight >>",
            };

            Border promotionMenuBorder = new Border(
                promotionMenu.PosX - 2,
                promotionMenu.PosY - 2,
                promotionMenu.Width + 4,
                promotionMenu.Height + 4, '=');

            promotionMenuBorder.BackgroundColor = ConsoleColor.Black;
            bool isWhite = this.gameBoard.GetPiece(position).IsWhite;
            if (isWhite)
            {
                promotionMenu.BackgroundColor = ConsoleColor.DarkGray;
                promotionMenu.SelectedElementColor = ConsoleColor.Gray;
            }
            else
            {
                promotionMenu.BackgroundColor = ConsoleColor.DarkRed;
                promotionMenu.SelectedElementColor = ConsoleColor.Red;
            }

            promotionMenuBorder.Draw();


            int selected = -1;
            while (selected == -1)
            {
                selected = promotionMenu.Draw();
                switch (selected)
                {
                    case 0:
                        this.gameBoard.SetPiece(new Queen(isWhite), position);
                        break;
                    case 1:
                        this.gameBoard.SetPiece(new Rook(isWhite), position);
                        break;
                    case 2:
                        this.gameBoard.SetPiece(new Bishop(isWhite), position);
                        break;
                    case 3:
                        this.gameBoard.SetPiece(new Knight(isWhite), position);
                        break;
                    default:
                        break;
                }
            }
            promotionMenuBorder.Clear();
            promotionMenu.Clear();
        }

        private List<ChessPosition> CastlingValidMoves(ChessPiece[,] board, ChessPosition position)
        {
            List<ChessPosition> validMoves = new List<ChessPosition>();
            bool isWhite = board[position.Row, position.Col].IsWhite;

            if (!(board[position.Row, position.Col].FirstMove))
            {
                return validMoves;
            }

            bool longCastling = false;
            if (board[position.Row, 0] != null)
            {
                if (board[position.Row, 0].GetType() == typeof(Rook))
                {
                    if (board[position.Row, 0].IsWhite == isWhite)
                    {
                        if (board[position.Row, 0].FirstMove)
                        {
                            longCastling = true;
                            for (int col = position.Col - 1; col > 1; col--)
                            {
                                if (board[position.Row, col] != null)
                                {
                                    longCastling = false;
                                    break;
                                }

                                board[position.Row, col] = board[position.Row, col + 1];
                                board[position.Row, col + 1] = null;

                                if (IsKingInCheck(board, isWhite))
                                {
                                    longCastling = false;
                                    board[position.Row, position.Col] = board[position.Row, col];
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            bool shortCastling = false;
            if (board[position.Row, 7] != null)
            {
                if (board[position.Row, 7].GetType() == typeof(Rook))
                {
                    if (board[position.Row, 7].IsWhite == isWhite)
                    {
                        if (board[position.Row, 7].FirstMove)
                        {
                            shortCastling = true;
                            for (int col = position.Col + 1; col < 7; col++)
                            {
                                if (board[position.Row, col] != null)
                                {
                                    shortCastling = false;
                                    break;
                                }

                                board[position.Row, col] = board[position.Row, col - 1];
                                board[position.Row, col - 1] = null;

                                if (IsKingInCheck(board, isWhite))
                                {
                                    shortCastling = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (longCastling)
            {
                validMoves.Add(new ChessPosition(position.Row, 2));
            }

            if (shortCastling)
            {
                validMoves.Add(new ChessPosition(position.Row, 6));
            }

            return validMoves;
        }

        private void CastlingMove(ChessPosition kingPos, ChessPosition move)
        {
            if (kingPos.Col > move.Col)
            {
                ChessPosition rookPos = new ChessPosition(kingPos.Row, 0);
                ChessPiece rook = gameBoard.GetPiece(rookPos);
                gameBoard.SetPiece(rook, new ChessPosition(move.Row, move.Col + 1));
                gameBoard.SetPiece(null, rookPos);
                rook.FirstMove = false;
            }
            else
            {
                ChessPosition rookPos = new ChessPosition(kingPos.Row, 7);
                ChessPiece rook = gameBoard.GetPiece(rookPos);
                gameBoard.SetPiece(rook, new ChessPosition(move.Row, move.Col - 1));
                gameBoard.SetPiece(null, rookPos);
                rook.FirstMove = false;
            }
        }
    }
}
