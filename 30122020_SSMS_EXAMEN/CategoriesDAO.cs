using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30122020_SSMS_EXAMEN
{
    class CategoriesDAO
    {
        string _query;
        string _con_string = SSMS_ExamenAppConfig.ConnectionString;
        log4net.ILog _log = SSMS_ExamenAppConfig._log;
        public void AddCategory(Categories c)
        {

            _query = $"INSERT INTO Categories" +
                    $"VALUES ({ c.Id},{ c.Name})";
            int row = NonReader(_query, "Add Category");
        }

        public void DeleteCategory(int id)
        {
            _query = $"DELETE FROM Categories WHERE id ={id}";
            int row = NonReader(_query, "Delete Category");
        }

        public void GetAllCategories()
        {
            _query = $"SELECT * FROM Categories";
            PrintAll(Reader(_query,"Get All Categories"), "Get All Categories");
        }


        public void UpdateCategory(int id, Categories c)
        {
            _query = $"UPDATE Category SET id = {c.Id},name = {c.Name}";
            int row = NonReader(_query, "Update Category");
        }

        public List<Categories> Reader(string query,string function)
        {
            List<Categories> allCategories = new List<Categories>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_con_string))
                {
                    connection.Open();
                    _log.Info($"Method {function}");
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categories t = new Categories

                                {
                                    Id = (Int64)reader["id"],
                                    Name = (string)reader["name"]
                                };
                                allCategories.Add(t);
                            }
                        }
                        return allCategories;
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
            return null;

        }

        public int NonReader(string query, string function)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(_con_string))
                {
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
              
        public void GetById (int id)
        {
            _query = $"SELECT * FROM Categories Where id = {id}";
            PrintAll ( Reader(_query,"Get By Id"), $"Get Id:{id}");
           
        }

        public void GetLargestTypeOfStories ()
        {
            _query = "SELECT c.id,c.name FROM  (SELECT category_id, Count(*) Numbers_Of_Category FROM Stores GROUP BY category_id) nn JOIN Categories c ON  nn.category_id = c.id WHERE nn.Numbers_Of_Category  =(SELECT TOP 1 Count(*) FROM Stores GROUP BY category_id  Order BY Count(*) DESC);";
                       
                           
                    
                    
                                                       
                                                       
                                                     

            PrintAll(Reader(_query, "Get Largest Type Of Stories"), "Get Largest Type Of Stories");
        }

        void PrintAll(List<Categories> allStores, string name_function)
        {
            Console.WriteLine($"                ******** {name_function}*********");
            Console.WriteLine();
            allStores.ForEach(c => Console.WriteLine($"{c}"));
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}
