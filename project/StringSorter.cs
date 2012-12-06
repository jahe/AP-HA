using System;
using System.Collections.Generic;

namespace AP_HA
{
    public class StringSorter : IComparable<StringSorter>
    {
        private List<string> _strings;
        private List<int> _numbers;

        #region Constructors
        public StringSorter()
        {

        }

        public StringSorter(string str)
        {
            _strings = new List<string>();
            _numbers = new List<int>();
            
            int position = 0;
            bool isNumber = false;
            
            while (position < str.Length)
            {
                int len = 0;

                while (position + len < str.Length && Char.IsDigit(str[position + len]) == isNumber)
                {
                    len++;
                }
                
                if (isNumber)
                {
                    _numbers.Add(int.Parse(str.Substring(position, len)));
                }
                else
                {
                    _strings.Add(str.Substring(position, len));
                }

                position += len;
                isNumber = !isNumber;
            }
        }
        #endregion

        public int CompareTo(StringSorter other)
        {
            int index = 0;

            while (index < _strings.Count && index < other._strings.Count)
            {
                int result = _strings[index].CompareTo(other._strings[index]);

                if (result != 0)
                {
                    return result;
                } 

                if (index < _numbers.Count && index < other._numbers.Count)
                {
                    result = _numbers[index].CompareTo(other._numbers[index]);
                    
                    if (result != 0)
                    {
                        return result;
                    } 
                }

                index++;
            }

            return 0;
        }
    }
}
