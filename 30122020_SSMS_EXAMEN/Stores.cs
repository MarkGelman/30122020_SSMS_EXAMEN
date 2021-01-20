using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30122020_SSMS_EXAMEN
{
    class Stores
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public long Category_Id { get; set; }

        public Stores()
        {

        }
        public Stores(long id, string name,int floor,long category_id)
        {
            Id = id;
            Name = name;
            Floor = floor;
            Category_Id = category_id;
        }

        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }

        public override bool Equals(object obj)
        {
            Stores test = obj as Stores;
            return this.Id.Equals(test.Id);
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }


        public static bool operator ==(Stores s1, Stores s2)
        {

            if (s1 is null && s2 is null)
                return true;
            if (s1.Id == s2.Id)
                return true;
            return false;
        }

        public static bool operator !=(Stores s1, Stores s2)
        {
            return !(s1 == s2);
        }


    }
}
