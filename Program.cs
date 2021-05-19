using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IDEproject_No._3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var passed = File.Open("Passed.txt", FileMode.Truncate);
            passed.Close();
            var failed = File.Open("Failed.txt", FileMode.Truncate);
            failed.Close();

            var exit = "";

            // var passedStudents = new LinkedList<Student>();
            // var failedStudents = new LinkedList<Student>();

            // var passedStudents = new List<Student>();
            // var failedStudents = new List<Student>();

            // var passedStudents = new Queue<Student>();
            // var failedStudents = new Queue<Student>();

            //await Task.Run(() => StudentsDataFromFile.WriteStudentDataToFile(StudentGenerator.GenerateStudentsList(1000)));

            // foreach (var VARIABLE in await Task.Run(() => StudentGenerator.GenerateStudentsLinkedList(10000)))
            // {
            //     if (VARIABLE.Final_points > 5)
            //     {
            //         passedStudents.Enqueue(VARIABLE);
            //     }
            //     else
            //     {
            //         failedStudents.Enqueue(VARIABLE);
            //     }
            // }

            var generatedStudents = new List<Student>();
            generatedStudents = StudentGenerator.GenerateStudentsList(10000);
            var failedStudents = new List<Student>();

            foreach (var student in generatedStudents.ToList())
            {
                if (student.Final_points < 5)
                {
                    failedStudents.Add(student);
                    generatedStudents.Remove(student);
                }
            }

            StudentsDataFromFile.WriteStudentDataToFile(generatedStudents);
            StudentsDataFromFile.WriteStudentDataToFile(failedStudents);


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

                    if (InputCheck.IsDigitsOnly(input.Split(" ")) &&
                        InputCheck.MarkCheck(Array.ConvertAll(input.Split(" "), int.Parse)))
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

                var students = new List<Student> {student};
                var mergedStudents = students.Union(StudentsDataFromFile.GetStudents()).ToList();
                mergedStudents.Sort((x, y) => string.Compare(x.Surname, y.Surname, StringComparison.Ordinal));

                var calculation = true;

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

                for (var i = 0; i <= header.Length + headerEnd.Length; i++)
                {
                    header2 += "-";
                }


                Console.Clear();
                Console.WriteLine(header + headerEnd);
                Console.WriteLine(header2);
                foreach (var varStudent in mergedStudents)
                {
                    var output = varStudent.Surname;
                    for (var i = 0; i < header.Length - varStudent.Surname.Length; i++)
                    {
                        output += " ";
                    }

                    output += varStudent.Name;
                    for (var i = 0;
                        i < headerEnd.Length - varStudent.Final_points.ToString(CultureInfo.InvariantCulture).Length -
                        varStudent.Name.Length;
                        i++)
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
