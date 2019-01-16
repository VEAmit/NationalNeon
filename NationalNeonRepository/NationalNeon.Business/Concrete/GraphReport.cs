using NationalNeon.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Business.Concrete
{
    public class GraphReport : IGraphReport
    {


        #region Property
        public bool? VisibleOnDashboard { get; set; }
        public string DepartmentName { get; set; }
        public Int32 TotalHours { get; set; }
        #endregion

        string ConnectionString = string.Empty;
        SqlConnection conn;
        SqlCommand cmd;
        public GraphReport()
        {
            conn = new SqlConnection(
               new SqlConnectionStringBuilder()
               {
                   DataSource = "172.16.200.36,1433\\SQLEXPRESS",
                   InitialCatalog = "signum_nationalneon",
                   UserID = "national",
                   Password = "pass@123"
               }.ConnectionString
            );

        }
        public List<GraphReport> GetgraphData()

        {
            var list = new List<GraphReport>();

            using (var command = new SqlCommand("usp_getGraphRecord", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(SetProperties(reader));
                    }
                }
                conn.Close();
            }
            return list;
        }
        public GraphReport SetProperties(IDataReader dr)
        {
            var obj = new GraphReport();
            obj.DepartmentName = dr["DepartmentName"].ToString();
            obj.TotalHours = dr["TotalHours"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalHours"]);
            obj.VisibleOnDashboard = dr["VisibleOnDashboard"] == DBNull.Value ? false : Convert.ToBoolean(dr["VisibleOnDashboard"]);
            return obj;

        }


    }
}
