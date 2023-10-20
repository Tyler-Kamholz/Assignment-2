using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment_2;

class Compound
{
    public int ID { get; set; }
    public int Carbon { get; set; }
    public int Nitrogen { get; set; }
    public int Oxygen { get; set; }
}

class PartC
{
    //Dictionary to get 2 compounds and the distance between them
    private static Dictionary<Tuple<Compound, Compound>, double> compDist = new();

    //main program
    public void Run()
    {
        //reads and sorts the file by carbon
        List<Compound> compounds = ReadCompoundsFromFile("../../../input3.txt");
        compounds.Sort((c1, c2) => c1.Carbon.CompareTo(c2.Carbon));
        CreateDictionary(compounds);

        //this runs the DnC
        Tuple<Compound, Compound> closestCompounds = FindMinimumDistance(compDist, 0, compDist.Count - 1);

        // Print the pair of compounds with the minimum distance.
        Console.WriteLine($"Minimum Distance between Compounds: {compDist[closestCompounds]}");
        Console.WriteLine($"Compound 1: ID={closestCompounds.Item1.ID}, Carbon={closestCompounds.Item1.Carbon}, Nitrogen={closestCompounds.Item1.Nitrogen}, Oxygen={closestCompounds.Item1.Oxygen}");
        Console.WriteLine($"Compound 2: ID={closestCompounds.Item2.ID}, Carbon={closestCompounds.Item2.Carbon}, Nitrogen={closestCompounds.Item2.Nitrogen}, Oxygen={closestCompounds.Item2.Oxygen}");


    }

    //DnC - this is O(Nlog(N))
    private Tuple<Compound, Compound> FindMinimumDistance(Dictionary<Tuple<Compound, Compound>, double> compounds, int left, int right)
    {
        // Base case: If there is only one pair of compounds, return its key (tuple).
        if (left == right)
        {
            return compounds.Keys.ElementAt(left);
        }

        // Divide the problem into two halves.
        int mid = (left + right) / 2;

        // Recursively find the minimum distance in the left and right halves.
        Tuple<Compound, Compound> leftMin = FindMinimumDistance(compounds, left, mid);
        Tuple<Compound, Compound> rightMin = FindMinimumDistance(compounds, mid + 1, right);

        // Compare the distances between the left and right halves and return the pair with the smaller distance.
        return compounds[leftMin] < compounds[rightMin] ? leftMin : rightMin;
    }

    //creates the dictionary - O(N)
    private void CreateDictionary(List<Compound> compounds)
    {
        Dictionary<Tuple<Compound, Compound>, double> newDictionary = new();

        for (int i = 0; i < compounds.Count - 1; i++)
        {
            // Calculate the differences in Nitrogen and Oxygen values between adjacent compounds.
            double carbonDifference = compounds[i + 1].Carbon - compounds[i].Carbon;
            double nitrogenDifference = compounds[i + 1].Nitrogen - compounds[i].Nitrogen;
            double oxygenDifference = compounds[i + 1].Oxygen - compounds[i].Oxygen;

            // Calculate the distance using the Pythagorean theorem (Euclidean distance).
            double distance = (double)Math.Sqrt((nitrogenDifference * nitrogenDifference) + (oxygenDifference * oxygenDifference) + (carbonDifference * carbonDifference));

            // Add the pair of adjacent compounds and their calculated distance to the dictionary.
            newDictionary.Add(new Tuple<Compound, Compound>(compounds[i], compounds[i + 1]), distance);
        }

        // Assign the newly created dictionary to the compDist variable.
        compDist = newDictionary;
    }

    //reads file and puts it into a compound object - O(N)
    static List<Compound> ReadCompoundsFromFile(string fileName)
    {
        List<Compound> compounds = new List<Compound>();
        string[] lines = File.ReadAllLines(fileName);
        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            Compound compound = new Compound
            {
                ID = int.Parse(data[0]),
                Carbon = int.Parse(data[1]),
                Nitrogen = int.Parse(data[2]),
                Oxygen = int.Parse(data[3])
            };
            compounds.Add(compound);
        }
        return compounds;
    }



}






//brute force
/*
 
public void Run()
        {
            List<Compound> compounds = ReadCompoundsFromFile("../../../input3.txt");
            Tuple<int, int> result = FindMinimumEnergyPair(compounds);
            Console.WriteLine($"Compound IDs with Minimum Energy Score: {result.Item1} and {result.Item2}");
        }

        static List<Compound> ReadCompoundsFromFile(string fileName)
        {
            List<Compound> compounds = new List<Compound>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                Compound compound = new Compound
                {
                    ID = int.Parse(data[0]),
                    Carbon = int.Parse(data[1]),
                    Nitrogen = int.Parse(data[2]),
                    Oxygen = int.Parse(data[3])
                };
                compounds.Add(compound);
            }
            return compounds;
        }

        static double CalculateEnergy(Compound compound1, Compound compound2)
        {
            return Math.Sqrt(Math.Pow(compound1.Carbon - compound2.Carbon, 2) +
                             Math.Pow(compound1.Nitrogen - compound2.Nitrogen, 2) +
                             Math.Pow(compound1.Oxygen - compound2.Oxygen, 2));
        }

        static Tuple<int, int> FindMinimumEnergyPair(List<Compound> compounds)
        {
            if (compounds.Count < 2)
                throw new ArgumentException("There must be at least two compounds to find a pair with minimum energy.");

            compounds.Sort((c1, c2) => c1.Carbon.CompareTo(c2.Carbon));
            Tuple<int, int> result = new Tuple<int, int>(compounds[0].ID, compounds[1].ID);
            double minEnergy = CalculateEnergy(compounds[0], compounds[1]);

            for (int i = 0; i < compounds.Count - 1; i++)
            {
                for (int j = i + 1; j < compounds.Count; j++)
                {
                    double energy = CalculateEnergy(compounds[i], compounds[j]);
                    if (energy < minEnergy)
                    {
                        minEnergy = energy;
                        result = new Tuple<int, int>(compounds[i].ID, compounds[j].ID);
                    }
                }
            }
            return result;
        }

 */