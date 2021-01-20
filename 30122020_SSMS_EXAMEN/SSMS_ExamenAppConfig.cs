using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30122020_SSMS_EXAMEN
{
    class SSMS_ExamenAppConfig
    { 
        
        private string m_file_name;
        private JObject m_configRoot;
        internal static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string ConnectionString { get; set; }


        public SSMS_ExamenAppConfig()
        {
            Init("SSMS_ExamenConfig.json");
        }

        internal void Init(string file_name)
        {
            m_file_name = file_name;

            if (!File.Exists(m_file_name))
            {
                Console.WriteLine($"File {m_file_name} not exist!");
                Environment.Exit(-1);
            }

            var reader = File.OpenText(m_file_name);
            string json_string = reader.ReadToEnd();

            JObject jo = (JObject)JsonConvert.DeserializeObject(json_string);
            m_configRoot = (JObject)jo["SSMS_Examen"];
            ConnectionString = m_configRoot["ConnectionString"].Value<string>();
        }

       bool TestDbConnection()
       {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    return true;
                }

            }

            catch (Exception ex)
            {
                // write error to log file
                Console.WriteLine($"Connection failed.Exception: {ex}");
                Console.ReadKey();
                Environment.Exit(-1);
                return false;
            }
       }
       
       internal SqlConnection GetOpenConnection ()
       {
            if (TestDbConnection())
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return connection;
                }
                
            }
            Environment.Exit(-1);
            return null;
       }
    }
}
