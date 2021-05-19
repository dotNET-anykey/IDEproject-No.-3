using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEproject_No._3
{
    class StudentGenerator
    {
        public static List<Student> GenerateStudentsList(int amount)
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i < amount; i++)
            {
                students.Add(new Student(i));
            }

            return students;
        }

        public static LinkedList<Student> GenerateStudentsLinkedList(int amount)
        {
            LinkedList<Student> students = new LinkedList<Student>();

            for (int i = 0; i < amount; i++)
            {
                students.AddLast(new Student(i));
            }

            return students;
        }

        public static Queue<Student> GenerateStudentsQueue(int amount)
        {
            Queue<Student> students = new Queue<Student>();

            for (int i = 0; i < amount; i++)
            {
                students.Enqueue(new Student(i));
            }

            return students;
        }
    }
}
