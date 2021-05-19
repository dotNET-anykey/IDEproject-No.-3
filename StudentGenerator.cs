using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEproject_No._3
{
    class StudentGenerator
    {
        public static List<Student> GenerateStudents(int amount)
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i < amount; i++)
            {
                students.Add(new Student(i));
            }

            return students;
        }
    }
}
