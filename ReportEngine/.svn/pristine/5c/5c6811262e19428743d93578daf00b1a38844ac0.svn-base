using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = @"[A1]bac[[A2]]dfd[A3]";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("A1", "H1");
            dic.Add("A2", "H2");
            dic.Add("A3", "H3");
            BindValue(dic, text);
            Console.ReadKey();
        }

       public static void BindValue(IDictionary<string, Object> values,string text)
        {
            if (null != values && values.Count>0)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    Regex reg = new Regex(string.Format(@"\[{0}\]",values.Keys.ElementAt(i)));
                    text = reg.Replace(text, values.Values.ElementAt(i).ToString());
                }
            }
           Console.WriteLine(text.ToString());
        }


    }
}
