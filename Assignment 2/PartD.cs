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

    /// <summary>
    /// Gets the list of water stops and finds the max number of water stops
    /// </summary>
    private void GetListAndFindMax()
    {
        String line = File.ReadAllText("../../../input4.txt");
        //Split string into an array of ints and assign that array as the list of runner stops
        list = line.Split(',')?.Select((num) => 
        {
            int numStops = int.Parse(num);
            if (numStops > max) // if the number of stops is greater than the current highest num stops, assign this num as max
            {
                max = numStops;
            }
            return numStops;

        })?.ToList();



    }

    public void FindWaterStops(int l, int r)
    {
        int mid = l + (r - l) / 2;
        int start = mid;
        int stop = mid;

        if (l > r)
            return;
        // if mid is greater than the min num stops we are looking for, look for the left and right edges of the contiguous runners who
        // have stopped at least numStops times
        if (list[mid] >= numStops)
        {
            for (int i = mid - 1; i >= l; i--) // look for left edge
            {
                if (list[i] < this.numStops)
                {
                    start = i + 1; break;
                }
                if (i == 0)
                {
                    start = i; break;
                }
            }
            for (int i = mid + 1; i <= r; i++) // look for right edge
            {
                if (list[i] < this.numStops)
                {
                    stop = i - 1; break;
                }
                if (i == list.Count - 1)
                {
                    stop = i; break;
                }
            }
            // if calculated value of this contiguous group of runners is greater than the last known largest calculation, 
            // set this contiguous group and calculation as the best
            if ((stop - start) * numStops > bestVal)
            {
                bestStart = start;
                bestStop = stop;
                bestVal = ((stop - start) + 1) * numStops;
                bestNumStops = numStops;
            }

            return;
            
        }
        else // recurse on the left and right sides of mid
        {
            FindWaterStops(l, mid - 1);
            FindWaterStops(mid + 1, r);
        }

    }

    public void FindWaterStopsHelper()
    {
        for (int i = 1; i < max; i++) // check calculations for each number of water stops up to the highest number of stops for this race
        {
            numStops = i;
            FindWaterStops(0, list.Count - 1);
        }

        Console.WriteLine($"{bestVal} {bestStart} {bestStop}");
    }

    public void BruteForce()
    {
        int largest = 0;
        int bestStart = -1;
        int bestStop = -1;

        for (int i = 0; i < list.Count; i++)
        {
            for (int stops = 1; stops <= list[i]; stops++)
            {
                int curTotal = stops;
                int endPoint = list.Count - 1;

                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j] >= stops)
                    {
                        // person j stopped at least stops times
                        curTotal += stops;
                    }
                    else
                    {
                        // current person did not stop "stops" times
                        // all runners from i to j-1 stopped "stops" times
                        endPoint = j - 1;
                        break;
                    }
                }

                if (curTotal > largest)
                {
                    largest = curTotal;
                    bestStart = i;
                    bestStop = endPoint;
                }
            }
        }

        Console.WriteLine($"{largest} {bestStart} {bestStop}");
    }
    

}
