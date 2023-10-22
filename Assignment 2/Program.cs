using System;
using System.IO;

namespace Assignment_2;

class Program
{

    static void Main(string[] args)
    {
        PartC partC = new();
        PartB partB = new();
        PartD partD = new();


        //Option menu
        prints();

        char option;

        //makes an input a char
        char.TryParse(Console.ReadLine(), out option);
        option = char.ToLower(option);

        //this starts the option program
        while (option != 'q')
        {
            switch (option)
            {

                case 'b':
                    Console.WriteLine();
                    partB.Run();
                    prints();
                    char.TryParse(Console.ReadLine(), out option);
                    option = char.ToLower(option);
                    break;

                case 'c':
                    Console.WriteLine();
                    partC.Run();
                    prints();
                    char.TryParse(Console.ReadLine(), out option);
                    option = char.ToLower(option);
                    break;

                case 'd':
                    Console.WriteLine();
                    partD.Run();
                    prints();
                    char.TryParse(Console.ReadLine(), out option);
                    option = char.ToLower(option);
                    break;
            }
        }

    }

    static void prints()
    {
        Console.WriteLine();
        Console.WriteLine("Enter what part of the assignment to test: 'B', 'C', or 'D'. (Q to quit)");
        Console.Write("Choice: ");
    }

}