using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace IDEproject_No._3
{
    class Program
    {
        static void Main(string[] args)
        {
            var exit = "";
            var students = new List<Student>();
            
            do
            {
                string input = null;
                Student student = null;

                do
                {
                    Console.WriteLine("Please enter a student name and surname.");
                    input = Console.ReadLine();
                    if (input == null) continue;

                    if (input.Split().Length == 2)
                    {
                        var studentNameSurname = input.Split();
                        student = new Student(studentNameSurname[0], studentNameSurname[1]);
                    }
                    else
                        input = null;

                } while (input == null);

                do
                {
                    Console.WriteLine(
                        "Please enter student marks for the homeworks with a space in between each mark. (10 9 3 5 5)");
                    input = Console.ReadLine();
                    if (input == null) continue;

                    if (InputCheck.IsDigitsOnly(input.Split(" ")) && InputCheck.MarkCheck(Array.ConvertAll(input.Split(" "), int.Parse)))
                    {
                        student.HwMarks = Array.ConvertAll(input.Split(" "), int.Parse);
                        student.HwMarksList = input.Split(" ").Select(int.Parse).ToList();
                    }
                    else
                        input = null;

                } while (input == null);

                do
                {
                    Console.WriteLine("Please enter a student mark for the exam.");
                    input = Console.ReadLine();
                    if (input != null)
                    {
                        if (InputCheck.IsDigitsOnly(input.Split(" ")) && InputCheck.MarkCheck(int.Parse(input)))
                        {
                            student.ExamMark = int.Parse(input);
                        }
                        else
                            input = null;
                    }
                } while (input == null);
                students.Add(student);
                var mergedStudents = students.Union(StudentsDataFromFile.GetStudents()).ToList();
                mergedStudents.Sort((x, y) => string.Compare(x.Surname, y.Surname, StringComparison.Ordinal));

                bool calculation = true;

                do
                {
                    Console.WriteLine("Please enter 1 for average calculation or 2 for median calculation.");
                    input = Console.ReadLine();
                    if (input != null)
                    {
                        if (input == "1")
                        {
                            calculation = true;
                        }
                        else if (input == "2")
                        {
                            calculation = false;
                        }
                        else
                            input = null;
                    }
                } while (input == null);

                var header = "Surname         ";
                var headerEnd = "";
                var header2 = "";

                if (calculation)
                {
                    headerEnd = "Name               Final Points(Avg.)";
                    foreach (var VARIABLE in mergedStudents)
                    {
                        VARIABLE.CalculateAvarage();
                    }
                }
                else
                {
                    headerEnd = "Name               Final Points(Med.)";
                    foreach (var VARIABLE in mergedStudents)
                    {
                        VARIABLE.CalculateMedian();
                    }
                }

                for (int i = 0; i <= header.Length + headerEnd.Length; i++)
                {
                    header2 += "-";
                }


                Console.Clear();
                Console.WriteLine(header + headerEnd);
                Console.WriteLine(header2);
                foreach (var varStudent in mergedStudents)
                {
                    string output = varStudent.Surname;
                    for (int i = 0; i < header.Length - varStudent.Surname.Length; i++)
                    {
                        output += " ";
                    }

                    output += varStudent.Name;
                    var finalPoints = $"{varStudent.Final_points:0.00}";
                    for (int i = 0; i < headerEnd.Length - finalPoints.Length - varStudent.Name.Length; i++)
                    {
                        output += " ";
                    }

                    output += varStudent.Final_points;
                    Console.WriteLine(output);
                }


                Console.WriteLine();
                Console.WriteLine(
                    @"If you would like to exit, type ""exit"". If you would like to repeat, just press enter.");
                input = Console.ReadLine();
                if (input != null)
                {
                    exit = input.ToUpper();
                }
            } while (exit != "EXIT");
        }
    }
}
