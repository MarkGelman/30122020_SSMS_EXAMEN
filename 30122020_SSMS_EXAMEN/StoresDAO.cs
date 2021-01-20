using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30122020_SSMS_EXAMEN
{
    class StoresDAO
    {
        string _query;
        string _con_string = SSMS_ExamenAppConfig.ConnectionString;
        log4net.ILog _log = SSMS_ExamenAppConfig._log;
        public void AddStore(Stores s)
        {

            _query = $"INSERT INTO Stores" +
                    $"VALUES ({s.Name},{s.Floor},{s.Name})";
            int row = NonReader(_query, "Add Store");
            GetAllStores("Add Store");
        }

        public void DeleteStore(int id)
        {
            _query = $"DELETE FROM Stores WHERE id ={id}";
            int row = NonReader(_query, "Delete Store");
            GetAllStores("Delete Store");
        }

        public void GetAllStores(string name_function)
        {
            _query = $"SELECT * FROM Stores";
           PrintAll( Reader(_query, name_function), name_function);
        }

        public void GetById(int id)
        {
            _query = $"SELECT * FROM Stores Where id = {id}";
            PrintAll( Reader(_query, "Get By Id"), $"Get  Id:{id}");

        }


        public void UpdateStore(int id, Stores s)
        {
            _query = $"UPDATE Stores SET id = {s.Id},name = {s.Name}, floor = {s.Floor},category_id = {s.Category_Id} ";
            int row = NonReader(_query, "Update Store");
            GetAllStores($"Update Store {id}");
        }

        public List<Stores> Reader(string query,string function)
        {
            List<Stores> allStores = new List<Stores>();
            try
            {
                using (SqlConnection connection = new SqlConnection (_con_string))
                {
                    connection.Open();
                     _log.Info($"Method {function}");
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Stores s = new Stores

                                {
                                    Id = (long)reader["id"],
                                    Category_Id = (long)reader["category_id"],
                                    Floor = (int)reader["floor"],
                                    Name = (string)reader["name"]
                                };
                                allStores.Add(s);
                            }
                        }
                    }
 
                    return allStores;
                }
               
            }

            catch (Exception ex)
            {
                _log.Error($"{function} method.\nException {ex}");
                Console.WriteLine($"Process '{ function}' failed.Exception : {ex}");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            return null;
        }

        public int NonReader( string query, string function)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_con_string))
                {
                    connection.Open();
                    _log.Info($"Method {function}");
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                _log.Error($"{function} method.\nException {ex}");
                Console.WriteLine($"Process '{ function}' failed.Exception : {ex}");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            return 0;
        }
        public List<Stores> GetAllByCategoryAndFloor (string category, int floor)
        {
            _query = $"SELEC s.name FROM Categories c JOIN Stores s ON s.category_id = c.id WHERE s.floor = {floor} AND c.name = {category} ";
            return Reader(_query, " Get All By Category And Floor");
        }

        void PrintAll(List<Stores> allStores,string name_function)
        {
            Console.WriteLine($"                ******** {name_function}*********");
            allStores.ForEach(c => Console.WriteLine($"{c}"));
            Console.WriteLine();
            Console.WriteLine();
        }
        

    }
}
