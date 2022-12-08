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
        public List<Fil> files=new List<Fil>();
        public void Print()
        {
            for(int i=0; i<files.Count();i++)
            {
                Console.WriteLine(files[i].name+" "+files[i].size);
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
            int step1 = 0;int step2 = 0;
            for(int i = 3; i < readText.Length; i++)
            {
                if (readText[i] != readText[i - 1] && readText[i] != readText[i - 2] && readText[i] != readText[i - 3])
                    if (readText[i - 1] != readText[i] && readText[i - 1] != readText[i - 2] && readText[i - 1] != readText[i - 3])
                        if (readText[i - 2] != readText[i - 1] && readText[i - 2] != readText[i] && readText[i - 2] != readText[i - 3])
                            if (readText[i - 3] != readText[i - 1] && readText[i-3] != readText[i - 2] && readText[i-3] != readText[i])
                            {
                                step1 = i + 1;
                                break;
                            }
            }            
            for (int i = 0; i < readText.Length; i++)
            {
                if (step2 > 0) break;
                int check = 0;
                for(int j=0; j < 14; j++)
                {
                    check = 0;
                    for (int k = 0; k < 14; k++)
                    {                        
                        if(readText[i+j] == readText[i+k] && i+j!=i+k)
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
            Console.WriteLine();
            Console.WriteLine("Step1 result "+step1+" step2 result "+step2);
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
                for(int j = 0; j < instructions.Length; j++)
                {
                    if(instructions[j] == "cd")
                    {
                        for(int k = 0; k < struktur.Count(); k++)
                        {                            
                            if (instructions[j + 1]=="..")
                            {                                
                                currentDir = struktur[currentDir].pDir;
                                break;
                            }
                            else if(struktur[currentDir].dir + instructions[j + 1]+"/" == struktur[k].dir)
                            {                                
                                currentDir = struktur[k].dirNo;
                                break;
                            }
                        }
                    }
                    if(instructions[j] == "dir")
                    {
                        struktur[currentDir].upDir.Add(struktur.Count());
                        M = new Dir();
                        M.dir = struktur[currentDir].dir + instructions[j+1]+"/";
                        M.pDir = currentDir;
                        M.dirNo=struktur.Count();
                        struktur.Add(M);
                    }
                    int a;
                    if (int.TryParse(instructions[j],out a) == true)
                    {
                        Fil F = new Fil();
                        F.size = int.Parse(instructions[j]);
                        F.name = instructions[j+1];
                        struktur[currentDir].files.Add(F);
                    }
                }
            }
            int result = 0;
            for(int i= struktur.Count()-1; i>-1;i--)
            {
                struktur[struktur[i].pDir].upDir.AddRange(struktur[struktur[i].dirNo].upDir);
            }
            struktur[0].upDir.Sort();
            for(int i=0; i< struktur[0].upDir.Count();i++)
            {
                if (struktur[0].upDir[i] == struktur[0].upDir[i])
                {
                    struktur[0].upDir.RemoveAt(i);
                }
            }
            for (int i = 0; i<struktur.Count(); i++)
            {                
                int max = 0;

                for (int k = 0; k < struktur[i].files.Count(); k++)
                {
                    max = max + struktur[i].files[k].size;
                }                

                for (int j = 0; j < struktur[i].upDir.Count(); j++)
                {  
                    for(int k = 0; k < struktur[struktur[i].upDir[j]].files.Count(); k++)
                    {   
                        max = max+ struktur[struktur[i].upDir[j]].files[k].size;
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
            double result2= 70000000;            
            for (int i = 0; i<allSizes.Length; i++)
            {                
                if (allSizes[i]> 30000000-(70000000 - maxx) && allSizes[i]<result2) result2=allSizes[i];
                if (allSizes[i] == 0) break;
            }
            Console.WriteLine();
            Console.WriteLine("Day7");
            Console.WriteLine("Step1 result "+result+ " Step2 result " + result2);
        }
        public static void Day8()
        {
            String[] data = File.ReadAllLines("Day8.txt");
            int step1 = 0;            
            bool[,] check = new bool[99, 99];
            int[,] checkValue=new int[99,99];            
            //populate visible/invisible trees and tree height 
            for (int i = 0; i < check.GetLength(0); i++)
            {
                for(int j = 0; j < check.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == check.GetLength(0) - 1 || j == check.GetLength(1) - 1) check[i, j] = true;                     
                    else check[i, j] = false;
                    checkValue[i, j] = int.Parse(data[i][j].ToString());
                }                
            }                      
            //check left to right
            for (int i=1; i < check.GetLength(0)-1; i++)
            {
                for( int j = 1; j < check.GetLength(1)-1; j++)
                {
                    if (checkValue[i,j]> checkValue[i,j-1])
                    {                        
                        check[i, j] = true;
                    }
                    if(checkValue[i, j] < checkValue[i, j - 1])
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
                    if (checkValue[i, j] > checkValue[i-1, j])
                    {
                        check[i, j] = true;
                    }
                    if (checkValue[i, j] < checkValue[i-1, j])
                    {
                        checkValue[i, j] = checkValue[i-1, j];
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
                for (int j = check.GetLength(1)-2; j > 0; j--)
                {
                    if (checkValue[i, j] > checkValue[i, j+1])
                    {
                        check[i, j] = true;
                    }
                    if (checkValue[i, j] < checkValue[i, j+1])
                    {
                        checkValue[i, j] = checkValue[i, j+1];
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
                    if (checkValue[i, j] > checkValue[i+1, j])
                    {
                        check[i, j] = true;
                    }
                    if (checkValue[i, j] < checkValue[i+1, j])
                    {
                        checkValue[i, j] = checkValue[i+1, j];
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
            Console.WriteLine("Day7");
            Console.WriteLine("Step1 result "+step1);
        }
        public static void Day8PartTwo()
        {
            String[] data = File.ReadAllLines("Day8.txt");
            int step2 = 0;            
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
            int y=0;
            for(int i=0; i < checkValue.Length; i++)
            {                
                if (x == 99) y++;
                if (x == 99) x = 0;
                int savey = y;
                int savex = x;
                for (int j = x+1; j < checkValue.GetLength(0); j++)
                {                      
                    if (checkValue[x, y] > checkValue[j, y]) resultS[x, y]++;
                    else if(checkValue[x, y] <= checkValue[j, y])
                    {
                        resultS[x, y]++;
                        break;
                    }
                }
                x = savex;
                y = savey;
                for (int j = y+1; j < checkValue.GetLength(0); j++)
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
                for (int j = x - 1; j > -1 ; j--)
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
            Console.Write("Step2 "+svar);
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
        }
    }
}