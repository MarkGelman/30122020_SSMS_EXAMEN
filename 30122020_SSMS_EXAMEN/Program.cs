using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30122020_SSMS_EXAMEN
{
    class Program
    {
        static log4net.ILog _log = SSMS_ExamenAppConfig._log;
        static void Main(string[] args)
        {

            _log.Info("******************** System startup");
            SSMS_ExamenAppConfig _config = new SSMS_ExamenAppConfig();
            SqlConnection connection = _config.GetOpenConnection();

            StoresDAO stores = new StoresDAO();
            CategoriesDAO categories = new CategoriesDAO();
            categories.GetAllCategories();
            categories.GetById(1);
            categories.GetLargestTypeOfStories();
            
            stores.GetAllStores("Get All Stores");
            stores.GetById(1);
            _log.Info("******************** System shutdown");
            Console.ReadKey();
        }
    }
}
