using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEproject_No._3
{
    class InputCheck
    {
        public static bool IsDigitsOnly(string[] str)
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

        public static bool MarkCheck(int[] homeworkMarks)
        {
            foreach (var homeworkMark in homeworkMarks)
            {
                if (homeworkMark < 1 || homeworkMark > 10)
                    return false;
            }

            return true;
        }

        public static bool MarkCheck(int examMark)
        {
            if (examMark < 1 || examMark > 10)
            {
                return false;
            }

            return true;
        }
    }
}
