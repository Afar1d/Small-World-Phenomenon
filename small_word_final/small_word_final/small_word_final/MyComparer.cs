using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace small_word_final
{
    public class MyComparer : IComparer<KeyValuePair<int, string>>
    {
        public int Compare(KeyValuePair<int, string> fn1, KeyValuePair<int, string> fn2)
        {


            return fn1.Key == fn2.Key ? fn1.Value.CompareTo(fn2.Value) : fn1.Key.CompareTo(fn2.Key);

        }
    }
}
