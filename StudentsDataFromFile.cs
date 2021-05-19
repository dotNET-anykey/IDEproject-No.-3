using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEproject_No._3
{
    class StudentsDataFromFile
    {
        public static List<Student> GetStudents()
        {
            var students = new List<Student>();
            List<string[]> data;

            try
            {
                data = File.ReadLines(@"students.txt").Skip(1).Select(line => line.Split(';').Take(3).ToArray())
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

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
                        else if (i == studentData.Length - 1)
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

            return students;
        }
    }
}
