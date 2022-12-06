using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Xml.XPath;

namespace Advent_of_Code_2022
{
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
            Console.WriteLine($"Step1: {elvesTotal.Max()} pounds Step2 {elvesTotal[0]+ elvesTotal[1]+ elvesTotal[2]} pounds");
            
        }
        public static void Day2()
        {
            string readText = File.ReadAllText("Day2.txt");
            String[] choices = readText.Split("\n");
            int scoreMe=0;  
            for(int i = 0; i < choices.Length; i++)
            {
                if (choices[i][2] == 'Y') scoreMe = scoreMe + 2;
                if (choices[i][2] == 'X') scoreMe = scoreMe + 1;
                if (choices[i][2] == 'Z') scoreMe = scoreMe + 3;
                if (choices[i][0] == 'A' && choices[i][2]=='Y') scoreMe = scoreMe + 6;
                if (choices[i][0] == 'B' && choices[i][2] == 'Z') scoreMe = scoreMe + 6;
                if (choices[i][0] == 'C' && choices[i][2] == 'X') scoreMe = scoreMe + 6;
                if (choices[i][0] == 'A' && choices[i][2] == 'X') scoreMe = scoreMe + 3;
                if (choices[i][0] == 'B' && choices[i][2] == 'Y') scoreMe = scoreMe + 3;
                if (choices[i][0] == 'C' && choices[i][2] == 'Z') scoreMe = scoreMe + 3;
            }
            int scoreMe2 = scoreMe;
            scoreMe =0;
            for(int i = 0; i < choices.Length; i++)
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
            Console.WriteLine("My Score step 1 " + scoreMe2+" step 2: "+scoreMe);
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
                    for (int k = chars.Length-1; k > (chars.Length/2)-1; k--)
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
            int sum1=sum;
            sum = 0;
            for (int i = 0; i < rucksacks.Length; i=i+3)
            {
                char[] chars = rucksacks[i].ToCharArray();
                char[] chars2 = rucksacks[i+1].ToCharArray();
                char[] chars3 = rucksacks[i+2].ToCharArray();
                bool[] checkDouble = new bool[300];
                bool[] checkTripple = new bool[300];
                string contains = "";
                char result=' ';
                for (int j = 0; j < chars.Length; j++)
                {
                    for(int k = 0; k < chars2.Length; k++)
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
            Console.WriteLine("My Score step1 "+sum1+" step2: "+sum);
        }
        public static void Day4()
        {
            String[] pairs = File.ReadAllLines("Day4.txt");
            int result = 0;
            for(int i = 0; i < pairs.Length; i++)
            {
                string[] split = pairs[i].Split(new char[] {'-', ',' });
                if (int.Parse(split[0]) <= int.Parse(split[2]) && int.Parse(split[1]) >= int.Parse(split[3])) result++;
                else if (int.Parse(split[0]) >= int.Parse(split[2]) && int.Parse(split[1]) <= int.Parse(split[3])) result++;
            }
            int result1 = result;
            result = 0;
            for (int i = 0; i < pairs.Length; i++)
            {
                string[] split = pairs[i].Split(new char[] { '-', ',' });
                int[] overlap= new int[100];
                int[] overlap2 = new int[100];
                for (int j = 0; j < overlap.Length; j++)
                {
                    if (int.Parse(split[1]) >= int.Parse(split[0])+j) overlap[j] = int.Parse(split[0]) + j;
                    if (int.Parse(split[3]) >= int.Parse(split[2]) + j) overlap2[j] = int.Parse(split[2]) + j;
                }
                for(int j = 0; j < overlap.Length; j++)
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
            Console.WriteLine("Step1 result " + result1 + " step2: "+result);
        }
        public static void Day5()
        {
            String[] data = File.ReadAllLines("Day5.txt");
            char[,] stacks =new char[9,100];
            for (int i = 0; i < stacks.GetLength(0); i++)
            {
                for (int j = 0; j < stacks.GetLength(1); j++)
                {
                    stacks[i, j] = ' ';
                }
            }


            int koll = 0;
            for(int j = 7; j > -1; j--)
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
                char save=' ';
                for(int j = 0; j < int.Parse(commands[1]); j++)
                {                    
                    for (int k = stacks.GetLength(1)-1; k > -1; k--)
                    {                        
                        if (stacks[int.Parse(commands[3])-1, k]!=' ')
                        {                               
                            save = stacks[int.Parse(commands[3]) - 1, k];
                            stacks[int.Parse(commands[3])-1, k] = ' ';
                            break;                            
                        }                        
                    }
                    for (int k = stacks.GetLength(1) - 1; k > -1; k--)
                    {
                        if (stacks[int.Parse(commands[5])-1, k] != ' ')
                        {                            
                            stacks[int.Parse(commands[5]) - 1, k+1] = save;
                            break;
                        }
                        else if(k==0) stacks[int.Parse(commands[5]) - 1, k + 1] = save;
                    }

                }                
            }
            string answer = "";
            for (int i = 0; i < stacks.GetLength(0); i++)
            {
                for (int j = 1; j < stacks.GetLength(1); j++)
                {
                    if(stacks[i,j] == ' ')
                    {
                        answer = answer + stacks[i, j - 1];
                        break;
                    }
                }                
            }
            Console.WriteLine();
            Console.WriteLine("Day5:");
            Console.Write("Step1 result "+answer);
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
                            save[j] = stacks[int.Parse(commands[3]) - 1, k-j];
                            stacks[int.Parse(commands[3]) - 1, k-j] = ' ';
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
        }
        public static void Day6()
        {
            string readText = File.ReadAllText("Day6.txt");

            Console.WriteLine();
            Console.Write(" Step1 result ");
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
        }
    }
}