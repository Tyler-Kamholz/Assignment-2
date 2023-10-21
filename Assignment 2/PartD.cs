using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2;

public class PartD
{
    int max = 0;
    int numStops = 1;
    int bestStart = 0;
    int bestStop;
    int bestNumStops = 1;
    int bestVal = int.MinValue;
    List<int> list = new();

    public PartD()
    {
        GetListAndFindMax();
        bestStop = list.Count - 1;
    }

    public void Run()
    {
        FindWaterStopsHelper();
    }

    private void GetListAndFindMax()
    {
        var read = new StreamReader("../../../input4.txt");

        String line;
        while ((line = read.ReadLine()) != null)
        {
            int num = int.Parse(line);
            if (num > max)
            {
                max = num;
            }

            list.Add(num);
        }
    }

    public void FindWaterStops(int l, int r)
    {
        int mid = l + (r - l) / 2;
        int start = mid;
        int stop = mid;

        if (l > r)
            return;
        if (list[mid] >= numStops)
        {
            for (int i = mid - 1; i >= l; i--)
            {
                if (list[i] < this.numStops || i == 0)
                {
                    start = i; break;
                }
            }
            for (int i = mid + 1; i <= r; i++)
            {
                if (list[i] < this.numStops || i == list.Count - 1)
                {
                    stop = i; break;
                }
            }

            if ((stop - start) * numStops > bestVal)
            {
                bestStart = start;
                bestStop = stop;
                bestVal = (stop - start) * numStops;
                bestNumStops = numStops + 1;
            }

            return;
            
        }
        else
        {
            FindWaterStops(l, mid - 1);
            FindWaterStops(mid + 1, r);
        }

    }

    public void FindWaterStopsHelper()
    {
        for (int i = 1; i < max; i++)
        {
            numStops = i;
            FindWaterStops(0, list.Count - 1);
        }

        Console.WriteLine($"{bestNumStops} {bestStart} {bestStop}");
    }
    

}
