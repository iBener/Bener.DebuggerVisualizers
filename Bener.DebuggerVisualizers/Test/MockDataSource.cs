using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bener.DebuggerVisualizers.Test
{
    public static class MockDataSource
    {
        public static List<Person> GetPeopleList()
        {
            var lst = new List<Person>
            {
                new Person() { Id = 3, Name = "Mehmet Yılmaz", Age = 18, Price = 23.42M, Picture = new byte[] { 43, 25, 18, 21 } },
                new Person() { Id = 5, Name = "Ayşe Çelik", Age = 23, Price = 13.83M, Picture = new byte[] { 83, 12, 38, 25 }  },
                new Person() { Id = 8, Name = "Mustafa Şahin", Age = 31, Price = 42.71M, Picture = new byte[] { 55, 44, 33, 22 }  }
            };
            return lst;
        }

        public static List<string> GetStringList()
        {
            var lst = new List<string>
            {
                "Mehmet Yılmaz",
                "Ayşe Çelik",
                "Mustafa Şahin"
            };
            return lst;
        }

        public static Dictionary<Guid, Person> GetPeopleDictionary()
        {
            var list = GetPeopleList();
            var dict = new Dictionary<Guid, Person>();
            foreach (var item in list)
            {
                dict.Add(Guid.NewGuid(), item);
            }
            return dict;
        }

        public static Dictionary<int, string> GetStringDictionary()
        {
            var dict = new Dictionary<int, string>
            {
                { 3, "Mehmet Yılmaz" },
                { 5, "Ayşe Çelik" },
                { 8, "Mustafa Şahin" }
            };
            return dict;
        }
    }
}
