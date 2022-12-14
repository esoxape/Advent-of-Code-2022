using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Xml.XPath;

namespace Advent_of_Code_2022
{
    public class Dir
    {
        public int dirNo;
        public int pDir;
        public string dir = "";
        public List<int> upDir = new List<int>();
        public List<Fil> files = new List<Fil>();
        public void Print()
        {
            for (int i = 0; i < files.Count(); i++)
            {
                Console.WriteLine(files[i].name + " " + files[i].size);
            }
        }
    }
    public class Fil
    {
        public int size;
        public string name = "";
    }
    internal class Program
    {
        public static void Day1()
        {
            string readText = File.ReadAllText("Day1.txt");
            String[] backpacks = readText.Split("\n");
            List<int> elvesTotal = new List<int>();
            elvesTotal.Add(0);
            int k = 0;

            for (int i = 0; i < backpacks.Length; i++)
            {
                if (backpacks[i].Length == 1)
                {
                    k++;
                    elvesTotal.Add(0);
                }
                else
                {
                    elvesTotal[k] = elvesTotal[k] + Convert.ToInt32(backpacks[i]);
                }
            }
            elvesTotal.Sort();
            elvesTotal.Reverse();
            Console.WriteLine("Day1:");
            Console.WriteLine($"Step1: {elvesTotal.Max()} pounds Step2 {elvesTotal[0] + elvesTotal[1] + elvesTotal[2]} pounds");

        }
        public static void Day2()
        {
            string readText = File.ReadAllText("Day2.txt");
            String[] choices = readText.Split("\n");
            int scoreMe = 0;
            for (int i = 0; i < choices.Length; i++)
            {
                if (choices[i][2] == 'Y') scoreMe = scoreMe + 2;
                if (choices[i][2] == 'X') scoreMe = scoreMe + 1;
                if (choices[i][2] == 'Z') scoreMe = scoreMe + 3;
                if (choices[i][0] == 'A' && choices[i][2] == 'Y') scoreMe = scoreMe + 6;
                if (choices[i][0] == 'B' && choices[i][2] == 'Z') scoreMe = scoreMe + 6;
                if (choices[i][0] == 'C' && choices[i][2] == 'X') scoreMe = scoreMe + 6;
                if (choices[i][0] == 'A' && choices[i][2] == 'X') scoreMe = scoreMe + 3;
                if (choices[i][0] == 'B' && choices[i][2] == 'Y') scoreMe = scoreMe + 3;
                if (choices[i][0] == 'C' && choices[i][2] == 'Z') scoreMe = scoreMe + 3;
            }
            int scoreMe2 = scoreMe;
            scoreMe = 0;
            for (int i = 0; i < choices.Length; i++)
            {
                if (choices[i][0] == 'A' && choices[i][2] == 'Y') scoreMe = scoreMe + 4;
                if (choices[i][0] == 'B' && choices[i][2] == 'Y') scoreMe = scoreMe + 5;
                if (choices[i][0] == 'C' && choices[i][2] == 'Y') scoreMe = scoreMe + 6;
                if (choices[i][0] == 'A' && choices[i][2] == 'X') scoreMe = scoreMe + 3;
                if (choices[i][0] == 'B' && choices[i][2] == 'X') scoreMe = scoreMe + 1;
                if (choices[i][0] == 'C' && choices[i][2] == 'X') scoreMe = scoreMe + 2;
                if (choices[i][0] == 'A' && choices[i][2] == 'Z') scoreMe = scoreMe + 8;
                if (choices[i][0] == 'B' && choices[i][2] == 'Z') scoreMe = scoreMe + 9;
                if (choices[i][0] == 'C' && choices[i][2] == 'Z') scoreMe = scoreMe + 7;

            }
            Console.WriteLine();
            Console.WriteLine("Day2:");
            Console.WriteLine("My Score step 1 " + scoreMe2 + " step 2: " + scoreMe);
        }
        public static void Day3()
        {
            String[] rucksacks = File.ReadAllLines("Day3.txt");
            int sum = 0;
            for (int i = 0; i < rucksacks.Length; i++)
            {
                char[] chars = rucksacks[i].ToCharArray();
                bool[] checkDouble = new bool[100];
                bool[] checkDouble2 = new bool[100];
                for (int j = 0; j < (chars.Length / 2); j++)
                {
                    for (int k = chars.Length - 1; k > (chars.Length / 2) - 1; k--)
                    {
                        if (chars[j] == chars[k] && checkDouble[j] != true && checkDouble2[k] != true)
                        {
                            checkDouble[j] = true;
                            checkDouble2[k] = true;
                            if (Char.IsUpper(chars[j]) == true)
                            {
                                sum = sum + chars[j] - 38;
                            }
                            else
                            {
                                sum = sum + chars[j] - 96;
                            }
                        }
                        else if (chars[j] == chars[k] && checkDouble[j] == true) checkDouble2[k] = true;
                    }
                }
            }
            int sum1 = sum;
            sum = 0;
            for (int i = 0; i < rucksacks.Length; i = i + 3)
            {
                char[] chars = rucksacks[i].ToCharArray();
                char[] chars2 = rucksacks[i + 1].ToCharArray();
                char[] chars3 = rucksacks[i + 2].ToCharArray();
                bool[] checkDouble = new bool[300];
                bool[] checkTripple = new bool[300];
                string contains = "";
                char result = ' ';
                for (int j = 0; j < chars.Length; j++)
                {
                    for (int k = 0; k < chars2.Length; k++)
                    {
                        if (chars[j] == chars2[k]) contains = contains + chars[j];
                    }
                    for (int k = 0; k < chars3.Length; k++)
                    {
                        if (chars[j] == chars3[k] && contains.Contains(chars[j])) result = chars[j];
                    }
                }
                if (Char.IsUpper(result) == true)
                {
                    sum = sum + result - 38;
                }
                else
                {
                    sum = sum + result - 96;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day3:");
            Console.WriteLine("My Score step1 " + sum1 + " step2: " + sum);
        }
        public static void Day4()
        {
            String[] pairs = File.ReadAllLines("Day4.txt");
            int result = 0;
            for (int i = 0; i < pairs.Length; i++)
            {
                string[] split = pairs[i].Split(new char[] { '-', ',' });
                if (int.Parse(split[0]) <= int.Parse(split[2]) && int.Parse(split[1]) >= int.Parse(split[3])) result++;
                else if (int.Parse(split[0]) >= int.Parse(split[2]) && int.Parse(split[1]) <= int.Parse(split[3])) result++;
            }
            int result1 = result;
            result = 0;
            for (int i = 0; i < pairs.Length; i++)
            {
                string[] split = pairs[i].Split(new char[] { '-', ',' });
                int[] overlap = new int[100];
                int[] overlap2 = new int[100];
                for (int j = 0; j < overlap.Length; j++)
                {
                    if (int.Parse(split[1]) >= int.Parse(split[0]) + j) overlap[j] = int.Parse(split[0]) + j;
                    if (int.Parse(split[3]) >= int.Parse(split[2]) + j) overlap2[j] = int.Parse(split[2]) + j;
                }
                for (int j = 0; j < overlap.Length; j++)
                {
                    if (overlap[j] == int.Parse(split[2]) || overlap[j] == int.Parse(split[3]))
                    {
                        result++;
                        break;
                    }
                    else if (overlap2[j] == int.Parse(split[0]) || overlap2[j] == int.Parse(split[1]))
                    {
                        result++;
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day4:");
            Console.WriteLine("Step1 result " + result1 + " step2: " + result);
        }
        public static void Day5()
        {
            String[] data = File.ReadAllLines("Day5.txt");
            char[,] stacks = new char[9, 100];
            for (int i = 0; i < stacks.GetLength(0); i++)
            {
                for (int j = 0; j < stacks.GetLength(1); j++)
                {
                    stacks[i, j] = ' ';
                }
            }


            int koll = 0;
            for (int j = 7; j > -1; j--)
            {
                if (data[j][1] != ' ') stacks[0, koll] = data[j][1];
                else stacks[0, koll] = ' ';
                if (data[j][5] != ' ') stacks[1, koll] = data[j][5];
                else stacks[1, koll] = ' ';
                if (data[j][9] != ' ') stacks[2, koll] = data[j][9];
                else stacks[2, koll] = ' ';
                if (data[j][13] != ' ') stacks[3, koll] = data[j][13];
                else stacks[3, koll] = ' ';
                if (data[j][17] != ' ') stacks[4, koll] = data[j][17];
                else stacks[4, koll] = ' ';
                if (data[j][21] != ' ') stacks[5, koll] = data[j][21];
                else stacks[5, koll] = ' ';
                if (data[j][25] != ' ') stacks[6, koll] = data[j][25];
                else stacks[6, koll] = ' ';
                if (data[j][29] != ' ') stacks[7, koll] = data[j][29];
                else stacks[7, koll] = ' ';
                if (data[j][33] != ' ') stacks[8, koll] = data[j][33];
                else stacks[8, koll] = ' ';
                koll++;
            }

            for (int i = 10; i < data.Length; i++)
            {
                string[] commands = data[i].Split(' ');
                char save = ' ';
                for (int j = 0; j < int.Parse(commands[1]); j++)
                {
                    for (int k = stacks.GetLength(1) - 1; k > -1; k--)
                    {
                        if (stacks[int.Parse(commands[3]) - 1, k] != ' ')
                        {
                            save = stacks[int.Parse(commands[3]) - 1, k];
                            stacks[int.Parse(commands[3]) - 1, k] = ' ';
                            break;
                        }
                    }
                    for (int k = stacks.GetLength(1) - 1; k > -1; k--)
                    {
                        if (stacks[int.Parse(commands[5]) - 1, k] != ' ')
                        {
                            stacks[int.Parse(commands[5]) - 1, k + 1] = save;
                            break;
                        }
                        else if (k == 0) stacks[int.Parse(commands[5]) - 1, k + 1] = save;
                    }

                }
            }
            string answer = "";
            for (int i = 0; i < stacks.GetLength(0); i++)
            {
                for (int j = 1; j < stacks.GetLength(1); j++)
                {
                    if (stacks[i, j] == ' ')
                    {
                        answer = answer + stacks[i, j - 1];
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day5:");
            Console.Write("Step1 result " + answer);
        }
        public static void Day5PartTwo()
        {
            String[] data = File.ReadAllLines("Day5.txt");
            char[,] stacks = new char[9, 100];
            char[] save = new char[50];
            for (int i = 0; i < stacks.GetLength(0); i++)
            {
                for (int j = 0; j < stacks.GetLength(1); j++)
                {
                    stacks[i, j] = ' ';
                }
            }

            int koll = 0;
            for (int j = 7; j > -1; j--)
            {
                if (data[j][1] != ' ') stacks[0, koll] = data[j][1];
                else stacks[0, koll] = ' ';
                if (data[j][5] != ' ') stacks[1, koll] = data[j][5];
                else stacks[1, koll] = ' ';
                if (data[j][9] != ' ') stacks[2, koll] = data[j][9];
                else stacks[2, koll] = ' ';
                if (data[j][13] != ' ') stacks[3, koll] = data[j][13];
                else stacks[3, koll] = ' ';
                if (data[j][17] != ' ') stacks[4, koll] = data[j][17];
                else stacks[4, koll] = ' ';
                if (data[j][21] != ' ') stacks[5, koll] = data[j][21];
                else stacks[5, koll] = ' ';
                if (data[j][25] != ' ') stacks[6, koll] = data[j][25];
                else stacks[6, koll] = ' ';
                if (data[j][29] != ' ') stacks[7, koll] = data[j][29];
                else stacks[7, koll] = ' ';
                if (data[j][33] != ' ') stacks[8, koll] = data[j][33];
                else stacks[8, koll] = ' ';
                koll++;
            }

            for (int i = 10; i < data.Length; i++)
            {
                string[] commands = data[i].Split(' ');

                for (int k = stacks.GetLength(1) - 1; k > -1; k--)
                {
                    if (stacks[int.Parse(commands[3]) - 1, k] != ' ')
                    {
                        for (int j = 0; j < int.Parse(commands[1]); j++)
                        {
                            save[j] = stacks[int.Parse(commands[3]) - 1, k - j];
                            stacks[int.Parse(commands[3]) - 1, k - j] = ' ';
                        }
                        break;
                    }
                }
                for (int k = stacks.GetLength(1) - 1; k > -1; k--)
                {
                    if (stacks[int.Parse(commands[5]) - 1, k] != ' ')
                    {
                        int counter = 1;
                        for (int j = int.Parse(commands[1]) - 1; j > -1; j--)
                        {
                            stacks[int.Parse(commands[5]) - 1, k + counter] = save[j];
                            counter++;
                            save[j] = ' ';
                        }
                        break;
                    }
                    else if (k == 0)
                    {
                        int counter = 1;
                        for (int j = int.Parse(commands[1]) - 1; j > -1; j--)
                        {
                            stacks[int.Parse(commands[5]) - 1, k + counter] = save[j];
                            counter++;
                            save[j] = ' ';
                        }
                    }
                }
            }

            string answer = "";
            for (int i = 0; i < stacks.GetLength(0); i++)
            {
                for (int j = 1; j < stacks.GetLength(1); j++)
                {
                    if (stacks[i, j] == ' ')
                    {
                        answer = answer + stacks[i, j - 1];
                        break;
                    }
                }
            }
            Console.Write(" Step2 result " + answer);
            Console.WriteLine();
        }
        public static void Day6()
        {
            string readText = File.ReadAllText("Day6.txt");
            int step1 = 0; int step2 = 0;
            for (int i = 3; i < readText.Length; i++)
            {
                if (readText[i] != readText[i - 1] && readText[i] != readText[i - 2] && readText[i] != readText[i - 3])
                    if (readText[i - 1] != readText[i] && readText[i - 1] != readText[i - 2] && readText[i - 1] != readText[i - 3])
                        if (readText[i - 2] != readText[i - 1] && readText[i - 2] != readText[i] && readText[i - 2] != readText[i - 3])
                            if (readText[i - 3] != readText[i - 1] && readText[i - 3] != readText[i - 2] && readText[i - 3] != readText[i])
                            {
                                step1 = i + 1;
                                break;
                            }
            }
            for (int i = 0; i < readText.Length; i++)
            {
                if (step2 > 0) break;
                int check = 0;
                for (int j = 0; j < 14; j++)
                {
                    check = 0;
                    for (int k = 0; k < 14; k++)
                    {
                        if (readText[i + j] == readText[i + k] && i + j != i + k)
                        {
                            check = 1;
                            break;
                        }
                    }
                    if (check == 1)
                    {
                        break;
                    }
                    else if (j == 13) step2 = i + 14;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day6");
            Console.WriteLine("Step1 result " + step1 + " step2 result " + step2);
        }
        public static void Day7()
        {
            String[] data = File.ReadAllLines("Day7.txt");
            List<Dir> struktur = new List<Dir>();
            Dir M = new Dir();
            int currentDir = 0;
            int[] allSizes = new int[10000];
            int counter = 0;
            struktur.Add(M);
            struktur[0].dir = "/";
            struktur[0].pDir = 0;
            struktur[0].dirNo = 0;
            for (int i = 0; i < data.Length; i++)
            {
                String[] instructions = data[i].Split(' ');
                for (int j = 0; j < instructions.Length; j++)
                {
                    if (instructions[j] == "cd")
                    {
                        for (int k = 0; k < struktur.Count(); k++)
                        {
                            if (instructions[j + 1] == "..")
                            {
                                currentDir = struktur[currentDir].pDir;
                                break;
                            }
                            else if (struktur[currentDir].dir + instructions[j + 1] + "/" == struktur[k].dir)
                            {
                                currentDir = struktur[k].dirNo;
                                break;
                            }
                        }
                    }
                    if (instructions[j] == "dir")
                    {
                        struktur[currentDir].upDir.Add(struktur.Count());
                        M = new Dir();
                        M.dir = struktur[currentDir].dir + instructions[j + 1] + "/";
                        M.pDir = currentDir;
                        M.dirNo = struktur.Count();
                        struktur.Add(M);
                    }
                    int a;
                    if (int.TryParse(instructions[j], out a) == true)
                    {
                        Fil F = new Fil();
                        F.size = int.Parse(instructions[j]);
                        F.name = instructions[j + 1];
                        struktur[currentDir].files.Add(F);
                    }
                }
            }
            int result = 0;
            for (int i = struktur.Count() - 1; i > -1; i--)
            {
                struktur[struktur[i].pDir].upDir.AddRange(struktur[struktur[i].dirNo].upDir);
            }
            struktur[0].upDir.Sort();
            for (int i = 0; i < struktur[0].upDir.Count(); i++)
            {
                if (struktur[0].upDir[i] == struktur[0].upDir[i])
                {
                    struktur[0].upDir.RemoveAt(i);
                }
            }
            for (int i = 0; i < struktur.Count(); i++)
            {
                int max = 0;

                for (int k = 0; k < struktur[i].files.Count(); k++)
                {
                    max = max + struktur[i].files[k].size;
                }

                for (int j = 0; j < struktur[i].upDir.Count(); j++)
                {
                    for (int k = 0; k < struktur[struktur[i].upDir[j]].files.Count(); k++)
                    {
                        max = max + struktur[struktur[i].upDir[j]].files[k].size;
                    }
                }
                allSizes[counter] = max;
                counter++;
                if (max < 100001) result = result + max;
            }
            int maxx = 0;
            for (int i = 0; i < allSizes.Length; i++)
            {
                if (allSizes[i] > maxx) maxx = allSizes[i];
                if (allSizes[i] == 0) break;
            }
            double result2 = 70000000;
            for (int i = 0; i < allSizes.Length; i++)
            {
                if (allSizes[i] > 30000000 - (70000000 - maxx) && allSizes[i] < result2) result2 = allSizes[i];
                if (allSizes[i] == 0) break;
            }
            Console.WriteLine();
            Console.WriteLine("Day7");
            Console.WriteLine("Step1 result " + result + " Step2 result " + result2);
        }
        public static void Day8()
        {
            String[] data = File.ReadAllLines("Day8.txt");
            int step1 = 0;
            bool[,] check = new bool[99, 99];
            int[,] checkValue = new int[99, 99];
            //populate visible/invisible trees and tree height 
            for (int i = 0; i < check.GetLength(0); i++)
            {
                for (int j = 0; j < check.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == check.GetLength(0) - 1 || j == check.GetLength(1) - 1) check[i, j] = true;
                    else check[i, j] = false;
                    checkValue[i, j] = int.Parse(data[i][j].ToString());
                }
            }
            //check left to right
            for (int i = 1; i < check.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < check.GetLength(1) - 1; j++)
                {
                    if (checkValue[i, j] > checkValue[i, j - 1])
                    {
                        check[i, j] = true;
                    }
                    if (checkValue[i, j] < checkValue[i, j - 1])
                    {
                        checkValue[i, j] = checkValue[i, j - 1];
                    }
                }
            }
            //repopulate tree heights
            for (int i = 0; i < check.GetLength(0); i++)
            {
                for (int j = 0; j < check.GetLength(1); j++)
                {
                    checkValue[i, j] = int.Parse(data[i][j].ToString());
                }
            }
            //check from top
            for (int i = 1; i < check.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < check.GetLength(1) - 1; j++)
                {
                    if (checkValue[i, j] > checkValue[i - 1, j])
                    {
                        check[i, j] = true;
                    }
                    if (checkValue[i, j] < checkValue[i - 1, j])
                    {
                        checkValue[i, j] = checkValue[i - 1, j];
                    }
                }
            }
            //repopulate tree heights
            for (int i = 0; i < check.GetLength(0); i++)
            {
                for (int j = 0; j < check.GetLength(1); j++)
                {
                    checkValue[i, j] = int.Parse(data[i][j].ToString());
                }
            }
            //check from right
            for (int i = check.GetLength(0) - 2; i > 0; i--)
            {
                for (int j = check.GetLength(1) - 2; j > 0; j--)
                {
                    if (checkValue[i, j] > checkValue[i, j + 1])
                    {
                        check[i, j] = true;
                    }
                    if (checkValue[i, j] < checkValue[i, j + 1])
                    {
                        checkValue[i, j] = checkValue[i, j + 1];
                    }
                }
            }
            //repopulate tree heights
            for (int i = 0; i < check.GetLength(0); i++)
            {
                for (int j = 0; j < check.GetLength(1); j++)
                {
                    checkValue[i, j] = int.Parse(data[i][j].ToString());
                }
            }
            //check from bottom
            for (int i = check.GetLength(0) - 2; i > 0; i--)
            {
                for (int j = check.GetLength(1) - 2; j > 0; j--)
                {
                    if (checkValue[i, j] > checkValue[i + 1, j])
                    {
                        check[i, j] = true;
                    }
                    if (checkValue[i, j] < checkValue[i + 1, j])
                    {
                        checkValue[i, j] = checkValue[i + 1, j];
                    }
                }
            }

            //calculate how many visible trees
            for (int i = 0; i < check.GetLength(0); i++)
            {
                for (int j = 0; j < check.GetLength(1); j++)
                {
                    if (check[i, j] == true) step1++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day8");
            Console.Write("Step1 result " + step1 + " ");
        }
        public static void Day8PartTwo()
        {
            String[] data = File.ReadAllLines("Day8.txt");
            int[,] checkValue = new int[99, 99];
            int[,] resultN = new int[99, 99];
            int[,] resultE = new int[99, 99];
            int[,] resultS = new int[99, 99];
            int[,] resultW = new int[99, 99];
            //populate  tree height 
            for (int i = 0; i < checkValue.GetLength(0); i++)
            {
                for (int j = 0; j < checkValue.GetLength(1); j++)
                {
                    checkValue[i, j] = int.Parse(data[i][j].ToString());
                }
            }
            //check all directions
            int x = 0;
            int y = 0;
            for (int i = 0; i < checkValue.Length; i++)
            {
                if (x == 99) y++;
                if (x == 99) x = 0;
                int savey = y;
                int savex = x;
                for (int j = x + 1; j < checkValue.GetLength(0); j++)
                {
                    if (checkValue[x, y] > checkValue[j, y]) resultS[x, y]++;
                    else if (checkValue[x, y] <= checkValue[j, y])
                    {
                        resultS[x, y]++;
                        break;
                    }
                }
                x = savex;
                y = savey;
                for (int j = y + 1; j < checkValue.GetLength(0); j++)
                {
                    if (checkValue[x, y] > checkValue[x, j]) resultE[x, y]++;
                    else if (checkValue[x, y] <= checkValue[x, j])
                    {
                        resultE[x, y]++;
                        break;
                    }
                }
                x = savex;
                y = savey;
                for (int j = x - 1; j > -1; j--)
                {
                    if (checkValue[x, y] > checkValue[j, y]) resultN[x, y]++;
                    else if (checkValue[x, y] <= checkValue[j, y])
                    {
                        resultN[x, y]++;
                        break;
                    }
                }
                x = savex;
                y = savey;
                for (int j = y - 1; j > -1; j--)
                {
                    if (checkValue[x, y] > checkValue[x, j]) resultW[x, y]++;
                    else if (checkValue[x, y] <= checkValue[x, j])
                    {
                        resultW[x, y]++;
                        break;
                    }
                }
                x++;
            }

            int svar = 0;

            //find highest scenic score
            for (int i = 0; i < checkValue.GetLength(0); i++)
            {
                for (int j = 0; j < checkValue.GetLength(1); j++)
                {
                    if (svar < resultN[i, j] * resultE[i, j] * resultS[i, j] * resultW[i, j]) svar = resultN[i, j] * resultE[i, j] * resultS[i, j] * resultW[i, j];
                }
            }
            Console.Write("Step2 " + svar);
            Console.WriteLine();
        }
        public static void Day9()
        {
            String[] data = File.ReadAllLines("Day9.txt");
            bool[,] visited = new bool[1000, 1000];
            bool[,] visited2 = new bool[1000, 1000];
            int[] head = { 500, 500 };
            int[] tail = { 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, };
            int step1 = 0;
            int step2 = 0;
            visited[tail[0], tail[1]] = true;
            visited2[tail[16], tail[17]] = true;
            for (int i = 0; i < data.Length; i++)
            {
                string[] check = data[i].Split(" ");
                for (int j = 0; j < int.Parse(check[1]); j++)
                {
                    if (check[0] == "U")
                    {
                        head[0] = head[0] - 1;
                        if (head[0] < tail[0] - 1)
                        {
                            tail[0] = tail[0] - 1;
                            if (tail[1] != head[1])
                            {
                                if (tail[1] < head[1]) tail[1]++;
                                else tail[1]--;
                            }
                        }
                    }
                    if (check[0] == "R")
                    {
                        head[1] = head[1] + 1;
                        if (head[1] > tail[1] + 1)
                        {
                            tail[1] = tail[1] + 1;
                            if (tail[0] != head[0])
                            {
                                if (head[0] > tail[0]) tail[0]++;
                                else tail[0]--;
                            }
                        }
                    }
                    if (check[0] == "D")
                    {
                        head[0] = head[0] + 1;
                        if (head[0] > tail[0] + 1)
                        {
                            tail[0] = tail[0] + 1;
                            if (tail[1] != head[1])
                            {
                                if (tail[1] < head[1]) tail[1]++;
                                else tail[1]--;
                            }
                        }
                    }
                    if (check[0] == "L")
                    {
                        head[1] = head[1] - 1;
                        if (head[1] < tail[1] - 1)
                        {
                            tail[1] = tail[1] - 1;
                            if (tail[0] != head[0])
                            {
                                if (tail[0] < head[0]) tail[0]++;
                                else tail[0]--;
                            }
                        }
                    }
                    visited[tail[0], tail[1]] = true;
                    for (int k = 2; k < 18; k = k + 2)
                    {
                        if (tail[k - 2] < tail[k] - 1)
                        {
                            tail[k] = tail[k] - 1;
                            if (tail[k + 1] != tail[k - 1])
                            {
                                if (Math.Abs(tail[k + 1] - tail[k - 1]) > 2) tail[k + 1] = tail[k - 1];
                                else if (tail[k + 1] < tail[k - 1]) tail[k + 1]++;
                                else tail[k + 1]--;
                            }
                        }
                        if (tail[k - 1] > tail[k + 1] + 1)
                        {
                            tail[k + 1] = tail[k + 1] + 1;
                            if (tail[k] != tail[k - 2])
                            {
                                if (Math.Abs(tail[k] - tail[k - 2]) > 2) tail[k] = tail[k - 2];
                                else if (tail[k] < tail[k - 2]) tail[k]++;
                                else tail[k]--;
                            }
                        }
                        if (tail[k - 2] > tail[k] + 1)
                        {
                            tail[k] = tail[k] + 1;
                            if (tail[k + 1] != tail[k - 1])
                            {
                                if (Math.Abs(tail[k + 1] - tail[k - 1]) > 2) tail[k + 1] = tail[k - 1];
                                else if (tail[k + 1] < tail[k - 1]) tail[k + 1]++;
                                else tail[k + 1]--;
                            }
                        }
                        if (tail[k - 1] < tail[k + 1] - 1)
                        {
                            tail[k + 1] = tail[k + 1] - 1;
                            if (tail[k] != tail[k - 2])
                            {
                                if (Math.Abs(tail[k] - tail[k - 2]) > 2) tail[k] = tail[k - 2];
                                else if (tail[k] < tail[k - 2]) tail[k]++;
                                else tail[k]--;
                            }
                        }
                    }
                    visited2[tail[16], tail[17]] = true;
                }

            }

            for (int i = 0; i < visited.GetLength(0); i++)
            {
                for (int j = 0; j < visited.GetLength(0); j++)
                {
                    if (visited[i, j] == true) step1 = step1 + 1;
                    if (visited2[i, j] == true) step2 = step2 + 1;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day9");
            Console.WriteLine("Step1 " + step1 + " Step2 " + step2);
        }
        public static int CycleCheck(int cycle, int x)
        {
            cycle = cycle + 1;
            if (cycle == 20) return cycle * x;
            if (cycle == 60) return cycle * x;
            if (cycle == 100) return cycle * x;
            if (cycle == 140) return cycle * x;
            if (cycle == 180) return cycle * x;
            if (cycle == 220) return cycle * x;
            return 0;
        }
        public static string CrtCheck(string crt, int cycle, int x)
        {
            if (crt.Length < 40 && cycle == x || x == cycle - 1 || x == cycle + 1) return "#";
            else if (crt.Length < 80 && cycle - 40 == x || x == cycle - 40 - 1 || x == cycle - 40 + 1) return "#";
            else if (crt.Length < 120 && cycle - 80 == x || x == cycle - 80 - 1 || x == cycle - 80 + 1) return "#";
            else if (crt.Length < 160 && cycle - 120 == x || x == cycle - 120 - 1 || x == cycle - 120 + 1) return "#";
            else if (crt.Length < 200 && cycle == x - 160 || x == cycle - 160 - 1 || x == cycle - 160 + 1) return "#";
            else if (crt.Length < 240 && cycle - 200 == x || x == cycle - 200 - 1 || x == cycle - 200 + 1) return "#";
            else return ".";
        }
        public static void Day10()
        {
            String[] data = File.ReadAllLines("Day10.txt");
            int step1 = 0;
            int x = 1;
            int cycle = 0;
            string crt = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0) crt = crt + CrtCheck(crt, cycle, x);
                string[] check = data[i].Split(" ");
                if (check[0] == "noop")
                {
                    cycle++;
                    step1 = step1 + CycleCheck(cycle, x);
                    crt = crt + CrtCheck(crt, cycle, x);
                }
                if (check[0] == "addx")
                {
                    cycle++;
                    step1 = step1 + CycleCheck(cycle, x);
                    crt = crt + CrtCheck(crt, cycle, x);
                    cycle++;
                    x = x + int.Parse(check[1]);
                    step1 = step1 + CycleCheck(cycle, x);
                    crt = crt + CrtCheck(crt, cycle, x);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day10");
            Console.WriteLine("Step1 " + step1 + " Step2 ");
            Console.WriteLine();
            for (int i = 0; i < crt.Length; i++)
            {
                if (i % 40 == 0 && i != 0) Console.WriteLine();
                if (i != crt.Length - 1) Console.Write(crt[i]);
            }
        }
        public class Monkey
        {
            public List<long> items = new List<long>();
            public int divisible;
            public int op1;
            public int op2;
            public string operand = "";
            public int trueThrow;
            public int falseThrow;
            public long counter = 0;
        }
        public static void Day11()
        {
            String[] data = File.ReadAllLines("Day11.txt");
            long step1 = 0;
            long step2 = 0;
            int modulus = 1;
            List<Monkey> monkeys = new List<Monkey>();
            List<Monkey> monkeys2 = new List<Monkey>();
            for (int i = 0; i < data.Length; i = i + 7)
            {
                Monkey M = new Monkey();
                Monkey M2 = new Monkey();
                string[] info = data[i + 1].Split(':', ',');
                for (int j = 1; j < info.Length; j++)
                {
                    M.items.Add(int.Parse(info[j]));
                    M2.items.Add(int.Parse(info[j]));
                }
                string[] info2 = data[i + 2].Split('=', ' ');
                if (info2[6] == "old")
                {
                    M.op1 = -1;
                    M2.op1 = -1;
                }
                else
                {
                    M.op1 = int.Parse(info2[6]);
                    M2.op1 = int.Parse(info2[6]);
                }
                M.operand = info2[7];
                M2.operand = info2[7];
                if (info2[8] == "old")
                {
                    M.op2 = -1;
                    M2.op2 = -1;
                }
                else
                {
                    M.op2 = int.Parse(info2[8]);
                    M2.op2 = int.Parse(info2[8]);
                }
                string[] info3 = data[i + 3].Split(' ');
                M.divisible = int.Parse(info3[5]);
                M2.divisible = int.Parse(info3[5]);
                modulus *= M.divisible;
                string[] info4 = data[i + 4].Split(' ');
                M.trueThrow = int.Parse(info4[9]);
                M2.trueThrow = int.Parse(info4[9]);
                string[] info5 = data[i + 5].Split(' ');
                M.falseThrow = int.Parse(info5[9]);
                M2.falseThrow = int.Parse(info5[9]);
                monkeys.Add(M);
                monkeys2.Add(M2);
            }           
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < monkeys.Count(); j++)
                {
                    for (int k = 0; k < monkeys[j].items.Count(); k++)
                    {
                        monkeys[j].counter++;
                        long worry = 0;
                        if (monkeys[j].operand == "*" && monkeys[j].op1 == -1 && monkeys[j].op2 != -1) worry = monkeys[j].items[k] * monkeys[j].op2;
                        if (monkeys[j].operand == "*" && monkeys[j].op1 == -1 && monkeys[j].op2 == -1) worry = monkeys[j].items[k] * monkeys[j].items[k];
                        if (monkeys[j].operand == "+" && monkeys[j].op1 == -1) worry = monkeys[j].items[k] + monkeys[j].op2;
                        worry = worry / 3;
                        if (worry % monkeys[j].divisible == 0)
                        {
                            monkeys[monkeys[j].trueThrow].items.Add(worry);
                        }
                        else
                        {
                            monkeys[monkeys[j].falseThrow].items.Add(worry);
                        }
                    }
                    monkeys[j].items.Clear();
                }
            }
            long highest = 0;
            for (int i = 0; i < monkeys.Count(); i++)
            {
                if (monkeys[i].counter > highest)
                {
                    step1 = monkeys[i].counter * highest;
                    highest = monkeys[i].counter;
                }
            }
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < monkeys2.Count(); j++)
                {
                    for (int k = 0; k < monkeys2[j].items.Count(); k++)
                    {
                        long worry = 0;
                        monkeys2[j].counter++;
                        if (monkeys2[j].operand == "*" && monkeys2[j].op1 == -1 && monkeys2[j].op2 != -1) worry = monkeys2[j].items[k] * monkeys2[j].op2;
                        if (monkeys2[j].operand == "*" && monkeys2[j].op1 == -1 && monkeys2[j].op2 == -1) worry = monkeys2[j].items[k] * monkeys2[j].items[k];
                        if (monkeys2[j].operand == "+" && monkeys2[j].op1 == -1) worry = monkeys2[j].items[k] + monkeys2[j].op2;
                        worry = worry % modulus;
                        if (worry % monkeys2[j].divisible == 0)
                        {
                            monkeys2[monkeys2[j].trueThrow].items.Add(worry);
                        }
                        else
                        {
                            monkeys2[monkeys2[j].falseThrow].items.Add(worry);
                        }
                    }
                    monkeys2[j].items.Clear();
                }
            }
            highest = 0;
            for (int i = 0; i < monkeys2.Count(); i++)
            {
                if (monkeys2[i].counter > highest)
                {
                    step2 = monkeys2[i].counter * highest;
                    highest = monkeys2[i].counter;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Day11");
            Console.WriteLine("Step1 " + step1 + " step2 " + step2);
        }
        bool ArrayContainsValue(int[,] array, int value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static int Day12Calc(char[,] mountain, char[,] mountain2, int[] win, List<string> pos, List<int> partTwoPos, bool CalcAll)
        {
            int stepReturn = 10000;
            int counter = 0;
            again:
            for (int i = 0; i < mountain2.GetLength(0); i++)
            {
                for (int j = 0; j < mountain2.GetLength(1); j++)
                {
                    mountain2[i, j] = ' ';
                }
            }
            if (CalcAll == true)
            {
                pos.Clear();
                pos.Add(partTwoPos[counter].ToString() + ";" + partTwoPos[counter + 1].ToString());
                counter = counter + 2;
            }
            int step = 0;
            string won = "no";
            while (won == "no")
            {
                for(int i = pos.Count()-1; i > -1; i--)
                {                    
                    string[] split = pos[i].Split(";");
                    if (int.Parse(split[0]) != 0 && int.Parse(split[0]) != mountain2.GetLength(0) - 1 && int.Parse(split[1]) != 0 && int.Parse(split[1]) != mountain2.GetLength(1) - 1)
                    {
                        if (mountain2[int.Parse(split[0]) - 1, int.Parse(split[1])] == 'X' && mountain2[int.Parse(split[0]) + 1, int.Parse(split[1])] == 'X' && mountain2[int.Parse(split[0]), int.Parse(split[1]) - 1] == 'X' && mountain2[int.Parse(split[0]) + 1, int.Parse(split[1]) + 1] == 'X') pos.RemoveAt(i);
                    }
                    else if (int.Parse(split[0]) == 0 && mountain2[int.Parse(split[0]) + 1, int.Parse(split[1])] == 'X') pos.RemoveAt(i);
                }
                int tracker = 0;
                for (int i = pos.Count() - 1; i > -1; i--)
                {
                    string[] split = pos[i].Split(";");
                    if (int.Parse(split[0]) + 1 != mountain.GetLength(0))
                    {
                        if (mountain[int.Parse(split[0]), int.Parse(split[1])] - mountain[int.Parse(split[0]) + 1, int.Parse(split[1])] > -2)
                        {
                            string info = "";
                            int calc = int.Parse(split[0]) + 1;
                            if (win[0] == calc && win[1] == int.Parse(split[1]))
                            {
                                won = "yes!";
                                if (step < stepReturn) stepReturn = step + 1;
                            }
                            info = calc.ToString() + ";";
                            info = info + split[1];
                            pos.Add(info);
                            if (mountain2[calc, int.Parse(split[1])] == ' ')
                            {
                                mountain2[calc, int.Parse(split[1])] = 'X';
                                tracker++;
                            }
                        }
                    }
                    if (int.Parse(split[0]) - 1 != -1)
                    {
                        if (mountain[int.Parse(split[0]), int.Parse(split[1])] - mountain[int.Parse(split[0]) - 1, int.Parse(split[1])] > -2)
                        {
                            string info = "";
                            int calc = int.Parse(split[0]) - 1;
                            if (win[0] == calc && win[1] == int.Parse(split[1]))
                            {
                                won = "yes!";
                                if (step < stepReturn) stepReturn = step + 1;
                            }
                            info = calc.ToString() + ";";
                            info = info + split[1];
                            pos.Add(info);                            
                            if (mountain2[calc, int.Parse(split[1])] == ' ')
                            {
                                mountain2[calc, int.Parse(split[1])] = 'X';
                                tracker++;
                            }
                        }
                    }
                    if (int.Parse(split[1]) + 1 != mountain.GetLength(1))
                    {
                        if (mountain[int.Parse(split[0]), int.Parse(split[1])] - mountain[int.Parse(split[0]), int.Parse(split[1]) + 1] > -2)
                        {
                            string info = "";
                            int calc = int.Parse(split[1]) + 1;
                            if (win[1] == calc && win[0] == int.Parse(split[0]))
                            {
                                won = "yes!";
                                if (step < stepReturn) stepReturn = step+1;
                            }
                            info = split[0] + ";";
                            info = info + calc.ToString();
                            pos.Add(info);                            
                            if (mountain2[int.Parse(split[0]), calc] == ' ')
                            {
                                mountain2[int.Parse(split[0]), calc] = 'X';
                                tracker++;
                            }
                        }
                    }
                    if (int.Parse(split[1]) - 1 != -1)
                    {
                        if (mountain[int.Parse(split[0]), int.Parse(split[1])] - mountain[int.Parse(split[0]), int.Parse(split[1]) - 1] > -2)
                        {
                            string info = "";
                            int calc = int.Parse(split[1]) - 1;
                            if (win[1] == calc && win[0] == int.Parse(split[0]))
                            {
                                won = "yes!";
                                if (step < stepReturn) stepReturn = step + 1;
                            }
                            info = split[0] + ";";
                            info = info + calc.ToString();
                            pos.Add(info);                            
                            if (mountain2[int.Parse(split[0]), calc] == ' ')
                            {
                                mountain2[int.Parse(split[0]), calc] = 'X';
                                tracker++;
                            }
                        }
                    }
                }
                if (tracker == 0) won = "stuck";
                pos = pos.Distinct().ToList();
                step++;                
            }            
            if (counter < partTwoPos.Count() && CalcAll==true) goto again;
            return stepReturn;
        }
        public static void Day12()
        {
            String[] data = File.ReadAllLines("Day12.txt");
            char[,] mountain = new char[data.Length, data[0].Length];
            char[,] mountain2 = new char[data.Length, data[0].Length];
            List<string> pos = new List<string>();                        
            int step1 = 0; int step2 = 0;
            int[] win = new int[2];
            List<int> partTwoPos = new List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    mountain[i, j] = data[i][j];
                    if (data[i][j] == 'S')
                    {
                        string info ="";
                        mountain[i, j] = 'a';
                        info = i.ToString() +";" ;
                        info = info+ j.ToString();
                        pos.Add(info);
                    }
                    else if (data[i][j] == 'E')
                    {
                        mountain[i, j] = 'z';
                        win[0] = i;
                        win[1] = j;
                    }
                    else if (data[i][j]=='a')
                    {
                        mountain[i, j] = data[i][j];
                        partTwoPos.Add(i);
                        partTwoPos.Add(j);
                    }
                    else mountain[i, j] = data[i][j];
                }
            }
            step1 = Day12Calc(mountain, mountain2, win, pos, partTwoPos, false);                       
            step2= Day12Calc(mountain, mountain2, win, pos, partTwoPos, true);  
            Console.WriteLine();
            Console.WriteLine("Day12");
            Console.WriteLine("Step1 " + step1 + " step2 " + step2);
        }
        static void Main(string[] args)
        {
            Day1();
            Day2();
            Day3();
            Day4();
            Day5();
            Day5PartTwo();
            Day6();
            Day7();
            Day8();
            Day8PartTwo();
            Day9();
            Day10();
            Day11();
            Day12();
        }
    }
}
