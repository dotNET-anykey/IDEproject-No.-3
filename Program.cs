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
            var data = File.ReadLines(@"students.txt").Skip(1).Select(line => line.Split(';').Take(3).ToArray()).ToList();

            foreach (var VARIABLE in data)
            {
                foreach (var VARIABLE2 in VARIABLE)
                {
                    var student = new Student();
                    var studentData = VARIABLE2.Split(" ");
                    var hwMarksSize = studentData.Length - 3;
                    var hwMarksIndex = 0;
                    student.HwMarks = new int[hwMarksSize];
                    for (int i = 0; i < studentData.Length; i++)
                    {

                        if (i == 0)
                        {
                            student.Surname = studentData[i];
                        }
                        else if (i == 1)
                        {
                            student.Name = studentData[i];
                        }
                        else if(i == studentData.Length - 1)
                        {
                            student.ExamMark = Convert.ToInt32(studentData[^1]);
                        }
                        else
                        {
                            student.HwMarks[hwMarksIndex] = Convert.ToInt32(studentData[i]);
                            hwMarksIndex++;
                        }
                    }
                    students.Add(student);
                }

            }

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

                    if (IsDigitsOnly(input.Split(" ")) && MarkCheck(Array.ConvertAll(input.Split(" "), int.Parse)))
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
                        if (IsDigitsOnly(input.Split(" ")) && MarkCheck(int.Parse(input)))
                        {
                            student.ExamMark = int.Parse(input);
                        }
                        else
                            input = null;
                    }
                } while (input == null);
                students.Add(student);
                students.Sort((x, y) => string.Compare(x.Surname, y.Surname, StringComparison.Ordinal));

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
                    foreach (var VARIABLE in students)
                    {
                        VARIABLE.CalculateAvarage();
                    }
                }
                else
                {
                    headerEnd = "Name               Final Points(Med.)";
                    foreach (var VARIABLE in students)
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
                foreach (var varStudent in students)
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

        static bool IsDigitsOnly(string[] str)
        {
            foreach (var variable in str)
            {
                foreach (char c in variable)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
            }

            return true;
        }

        static bool MarkCheck(int[] homeworkMarks)
        {
            foreach (var homeworkMark in homeworkMarks)
            {
                if (homeworkMark < 1 || homeworkMark > 10)
                    return false;
            }

            return true;
        }

        static bool MarkCheck(int examMark)
        {
            if (examMark < 1 || examMark > 10)
            {
                return false;
            }

            return true;
        }
    }
}
