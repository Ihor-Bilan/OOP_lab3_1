using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        Console.OutputEncoding=System.Text.Encoding.UTF8;
        List<string> noFile = new List<string>();
        List<string> badData = new List<string>();
        List<string> overflow = new List<string>();
        List<int> products = new List<int>();

        try
        {
            for (int i = 10; i <= 29; i++)
            {
                string fileName = $"{i}.txt";
                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                                        
                    EnsureTwoLines(lines);
                                       
                    int num1 = int.Parse(lines[0].Trim());
                    int num2 = int.Parse(lines[1].Trim());
                                        
                    checked
                    {
                        products.Add(num1 * num2);
                    }
                }
                catch (FileNotFoundException)
                {
                    noFile.Add(fileName);
                }
                catch (FormatException)
                {
                    badData.Add(fileName);
                }
                catch (OverflowException)
                {
                    overflow.Add(fileName);
                }
            }
                        
            WriteToFile("no_file.txt", noFile);
            WriteToFile("bad_data.txt", badData);
            WriteToFile("overflow.txt", overflow);
                        
            int average = products.Count > 0 ? products.Sum() / (int)products.Count : 0;
            Console.WriteLine($"Середнє арифметичне добутків: {average}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критична помилка: {ex.Message}");
        }
    }

    static void EnsureTwoLines(string[] lines)
    {
        try
        {            
            _ = lines[0];
            _ = lines[1];
        }
        catch (IndexOutOfRangeException)
        {
            throw new FormatException("Not enough lines");
        }
    }

    static void WriteToFile(string fileName, List<string> content)
    {
        try
        {
            File.WriteAllLines(fileName, content);
        }
        catch
        {
            throw new Exception($"Не вдалося створити або перезаписати файл: {fileName}");
        }
    }
}
