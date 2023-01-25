using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projektZaliczeniowy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessGame game = new ChessGame(11, 3);
            game.Start();
            Console.ReadKey(true);
        }
    }
}