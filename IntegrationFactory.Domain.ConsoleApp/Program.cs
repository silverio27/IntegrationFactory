using System;
using Figgle;


namespace IntegrationFactory.Domain.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Bem-vindo ao:");
            Console.WriteLine(FiggleFonts.Standard.Render("Integration Factory"));
            Console.ForegroundColor = ConsoleColor.White;
          

            Console.ReadKey();
        }
    }
}
