using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace Assignment_2;

class PartB
{
    public static void Run()
    {
        List<String> times = File.ReadAllLines("../../../input2.txt").ToList();

        BinarySearchTime(times, 0, times.Count - 1);
    }
    public static int CompareTimes(String time1, String time2)
    {
        String[] time1Full = time1.Split(':');
        String[] time2Full = time2.Split(":");

        for (int i = 0; i < 3; i++)
        {
            int time1Time = int.Parse(time1Full[i]);
            int time2Time = int.Parse(time2Full[i]);

            if (time1Time < time2Time)
                return -1;
            else if (time1Time >= time2Time)
                return 1;
        }

        return -1;
    }

    public static void BinarySearchTime(List<String> list, int start, int end)
    {
        int mid = (end + start) / 2;
        
        if (mid == end)
        {
            if ((end - 2) == int.Parse(list[0]))
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Problem");
            }

            return;
        }
        else if (mid == start)
        {
            Console.WriteLine("Problem");
            return;
        }
        else if (CompareTimes(list[mid], list[1]) == -1 && CompareTimes(list[mid + 1], list[1]) == 1)
        {
            if ((mid - 2) == int.Parse(list[0]))
            {
                Console.WriteLine("OK");
            }

            Console.WriteLine("Problem");
            return;
        }
        else if (CompareTimes(list[mid], list[1]) == 1)
        {
            BinarySearchTime(list, start, mid - 1);
        }
        else
        {
            BinarySearchTime(list, mid, end);
        }
    }



}
