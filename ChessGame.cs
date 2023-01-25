using System;

namespace projektZaliczeniowy
{
    class ChessGame
    {
        private int posX, posY;
        private Border gameBorder;
        private MultiLineText gameLogo;
        private Menu startMenu = new Menu();
        public ChessGame(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
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

        public void Start()
        {
            startMenu.Options = new string[] {
                "<< Start Game >>",
                "<< Help >>",
                "<< Exit >>",
            };

            bool gameIsRunning = true;
            while (gameIsRunning)
            {
                switch (StartScreen())
                {
                    case 0:
                        ChessGameStandard game = new ChessGameStandard(PosX, PosY);
                        int winner = game.StartGame();

                        game.DrawBoardReadOnly();
                        if (winner == 0)
                        {
                            StaleMateScreen();
                        }
                        else if (winner > 0)
                        {
                            GameResultScreen(winner);
                        }
                        game.Clear();
                        break;
                    case 1:
                        HelpScreen();
                        break;
                    default:
                        gameBorder.Clear();
                        gameIsRunning = false;
                        break;
                }
            }
        }

        protected virtual void HelpScreen()
        {
            Menu helpMenu = new Menu(this.PosX + 10, this.PosY + 19);

            helpMenu.Options = new string[] {
                "<< Left/Right/Down/Up Arrrow - Change Selected Element >>",
                "<< Escape - Quit/Cancel >>",
                "<< Enter - Confirm Choice >>",
            };

            Border helpMenuBorder = new Border(helpMenu.PosX - 2,
                helpMenu.PosY - 2,
                helpMenu.Width + 4,
                helpMenu.Height + 4,
                '=');

            helpMenuBorder.BackgroundColor = ConsoleColor.Black;

            helpMenuBorder.Draw();
            gameLogo.Draw();
            helpMenu.Draw();

            gameLogo.Clear();
            helpMenuBorder.Clear();
            helpMenu.Clear();
        }

        protected virtual int StartScreen()
        {
            gameBorder = new Border(this.PosX, this.PosY, 94, 28, '*');
            gameBorder.BackgroundColor = ConsoleColor.Black;
            gameBorder.Draw();

            startMenu.PosX = this.PosX + 10;
            startMenu.PosY = this.PosY + 19;

            Border menuBorder = new Border(startMenu.PosX - 2,
                startMenu.PosY - 2,
                startMenu.Width + 4,
                startMenu.Height + 4, '=');
            menuBorder.BackgroundColor = ConsoleColor.Black;
            menuBorder.Draw();

            string consoleChessText =
@"                           _             _                   
                          | |           | |                  
  ___ ___  _ __  ___  ___ | | ___    ___| |__   ___  ___ ___ 
 / __/ _ \| '_ \/ __|/ _ \| |/ _ \  / __| '_ \ / _ \/ __/ __|
| (_| (_) | | | \__ \ (_) | |  __/ | (__| | | |  __/\__ \__ \
 \___\___/|_| |_|___/\___/|_|\___|  \___|_| |_|\___||___/___/";

            gameLogo = new MultiLineText(this.PosX + 15, this.PosY + 2, consoleChessText);
            gameLogo.BackgroundColor = ConsoleColor.Black;
            gameLogo.Draw();

            int selected = startMenu.Draw();

            menuBorder.Clear();
            gameLogo.Clear();
            startMenu.Clear();

            return selected;
        }
        protected virtual void GameResultScreen(int winner)
        {
            Menu gameResult = new Menu(this.posX + 63, this.posY + 4);

            gameResult.Options = new string[]
            {
                "<< Checkmate! >>",
                "<< First Player Wins >>",
                "<< Press: >>",
                "<< Enter/Escape to leave >>",
            };

            if (winner == 1)
            {
                gameResult.BackgroundColor = ConsoleColor.DarkGray;
                gameResult.SelectedElementColor = ConsoleColor.Gray;
            }
            else if (winner == 2)
            {
                gameResult.Options[1] = "<< Second Player Wins >>";
                gameResult.BackgroundColor = ConsoleColor.DarkRed;
                gameResult.SelectedElementColor = ConsoleColor.Red;
            }

            Border gameResultBorder = new Border(
                gameResult.PosX - 2,
                gameResult.PosY - 2,
                gameResult.Width + 4,
                gameResult.Height + 4,
                '=');

            gameResultBorder.BackgroundColor = ConsoleColor.Black;

            gameResultBorder.Draw();
            gameResult.Draw();
            gameResult.Clear();
            gameResultBorder.Clear();
        }
        protected virtual void StaleMateScreen()
        {
            Menu stalemate = new Menu(this.posX + 63, this.posY + 4);

            stalemate.Options = new string[]
            {
                "<< Stalemate! >>",
                "<< Press: >>",
                "<< Enter/Escape to leave >>",
            };

            Border stalemateBorder = new Border(
                stalemate.PosX - 2,
                stalemate.PosY - 2,
                stalemate.Width + 4,
                stalemate.Height + 4,
                '=');

            stalemateBorder.BackgroundColor = ConsoleColor.Black;
            stalemateBorder.Draw();
            stalemate.Draw();
            stalemateBorder.Clear();
            stalemate.Clear();
        }
    }
}
