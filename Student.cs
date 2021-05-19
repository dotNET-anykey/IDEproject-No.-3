using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEproject_No._3
{
    class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int[] HwMarks { get; set; }
        public List<int> HwMarksList { get; set; }
        public int ExamMark { get; set; }
        public double Final_points { get; set; }

        public Student(string name, string surname)
        {
            Name = name;
            Surname = surname;
            var randNum = new Random();
            HwMarks = new int[randNum.Next(0, 10)];

            for (int i = 0; i < HwMarks.Length; i++)
            {
                HwMarks[i] = randNum.Next(1, 10);
            }

            ExamMark = randNum.Next(1, 10);
        }

        public void CalculateMedian()
        {
            var sortedPNumbers = Array.ConvertAll<int, double>(HwMarks, x => x);
            Array.Sort(sortedPNumbers);

            var size = sortedPNumbers.Length;
            var mid = size / 2;
            Final_points = size % 2 != 0 ? sortedPNumbers[mid] : (sortedPNumbers[mid] + sortedPNumbers[mid - 1]) / 2;
        }

        public void CalculateAvarage()
        {
            var avg = HwMarks.AsQueryable().Average();
            Final_points = 0.3 * avg + 0.7 * ExamMark;
        }
    }
}
