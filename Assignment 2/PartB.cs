using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment_2;

class PartB
{
    public static void Run()
    {
        List<String> times = File.ReadAllLines("../../../input2.txt").ToList();

        BinarySearchTime(times, 0, times.Count - 1);
    }
    public static int CompareTimes(String goal, String time)
    {
        String[] splitGoal = goal.Split(':');
        String[] splitTime = time.Split(":");

        for (int i = 0; i < 3; i++)
        {
            int goalTime = int.Parse(splitGoal[i]);
            int timeTime = int.Parse(splitTime[i]);

            if (timeTime < goalTime)
                return -1;
            else if (timeTime >= goalTime)
                return 1;
        }

        return 1;
    }

    public static void BinarySearchTime(List<String> list, int start, int end)
    {
        int mid = start + (end - start) / 2;
        if (start == end)
        {
            if (CompareTimes(list[1], list[mid]) == -1)
            {
                if ((mid - 2) == int.Parse(list[0]))
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("Problem");
                }
            }
        }
        else if (CompareTimes(list[1], list[mid]) == -1)
        {
            BinarySearchTime(list, start, mid);
        }
        else
        {
            BinarySearchTime(list, mid, end);
        }
    }


    
}
