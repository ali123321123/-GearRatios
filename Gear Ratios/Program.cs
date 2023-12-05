

public class GearRatios
{
    private static readonly (int, int)[] Neighbors = {
        (-1, -1), (-1, 0), (-1, 1),
        (1, -1), (1, 0), (1, 1),
        (0, -1), (0, 1)
    };

    private static bool IsNumber(char x)
    {
        return (x >= '0' && x <= '9');
    }

    private static int Part1(List<string> engine)
    {
        int sum = 0;

        for (int row = 0; row < engine.Count; row++)
        {
            int numStart = -1;
            bool found = false;

            for (int col = 0; col < engine[row].Length; col++)
            {
                if (IsNumber(engine[row][col]))
                {
                    if (numStart == -1)
                    {
                        numStart = col;
                    }

                    if (!found)
                    {
                        foreach (var neigh in Neighbors)
                        {
                            int y = row + neigh.Item1;
                            int x = col + neigh.Item2;

                            if (x < 0 || x >= engine[row].Length || y < 0 || y >= engine.Count)
                            {
                                continue;
                            }

                            if (engine[y][x] != '.' && !IsNumber(engine[y][x]))
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
                else if (numStart != -1)
                {
                    if (found)
                    {
                        sum += int.Parse(engine[row].Substring(numStart, col - numStart));
                    }

                    found = false;
                    numStart = -1;
                }
            }

            if (numStart != -1)
            {
                if (found)
                {
                    sum += int.Parse(engine[row].Substring(numStart));
                }
            }
        }

        return sum;
    }

    private static int Part2(List<string> engine)
    {
        int sum = 0;

        for (int row = 0; row < engine.Count; row++)
        {
            for (int col = 0; col < engine[row].Length; col++)
            {
                if (engine[row][col] != '*')
                {
                    continue;
                }

                List<int> numbers = new List<int>();
                List<(int, int)> numberPos = new List<(int, int)>();

                foreach (var neigh in Neighbors)
                {
                    int y = row + neigh.Item1;
                    int x = col + neigh.Item2;

                    if (x < 0 || x >= engine[row].Length || y < 0 || y >= engine.Count)
                    {
                        continue;
                    }

                    if (IsNumber(engine[y][x]))
                    {
                        while (x >= 0 && IsNumber(engine[y][x]))
                        {
                            x--;
                        }

                        if (x < 0 || !IsNumber(engine[y][x]))
                        {
                            x++;
                        }

                        var pos = (y, x);

                        if (!numberPos.Contains(pos))
                        {
                            numberPos.Add(pos);

                            int start = x;
                            while (x < engine[y].Length && IsNumber(engine[y][x]))
                            {
                                x++;
                            }

                            if (x == engine[y].Length || !IsNumber(engine[y][x]))
                            {
                                x--;
                            }

                            numbers.Add(int.Parse(engine[y].Substring(start, x - start + 1)));
                        }
                    }
                }

                if (numbers.Count == 2)
                {
                    sum += numbers[0] * numbers[1];
                }
            }
        }

        return sum;
    }

    public static void Solve()
    {

        string text = @"C:\testutvikling\Gear Ratios\Gear Ratios\input.txt";
        
        var input = new List<string>(System.IO.File.ReadAllLines(text));

        int resultPart1 = Part1(input);
        int resultPart2 = Part2(input);

        Console.WriteLine($"Part 1: {resultPart1}");
        Console.WriteLine($"Part 2: {resultPart2}");
    }
    
    class Program
    {
        static void Main()
        {
            GearRatios.Solve();
        }
    }
}
