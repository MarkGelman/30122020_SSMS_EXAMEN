using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30122020_SSMS_EXAMEN
{
    class Categories
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Categories()
        {

        }
        public Categories(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }

        public override bool Equals(object obj)
        {
            Categories test = obj as Categories;
            return this.Id.Equals(test.Id);
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }


        public static bool operator ==(Categories c1, Categories c2)
        {

            if (c1 is null && c2 is null)
                return true;
            if (c1.Id == c2.Id)
                return true;
            return false;
        }

        public static bool operator !=(Categories c1, Categories c2)
        {
            return !(c1 == c2);
        }

    }
}
