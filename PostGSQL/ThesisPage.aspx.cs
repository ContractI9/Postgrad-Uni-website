﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostGSQL
{
    public partial class ThesisPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["PostGSQL"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand ViewAllMythesis = new SqlCommand("ViewAllMythesis", conn);
            ViewAllMythesis.CommandType = CommandType.StoredProcedure;
            ViewAllMythesis.Parameters.Add(new SqlParameter("@studentID", SqlDbType.Int)).Value = Session["user"];

            conn.Open();
            SqlDataReader rdr = ViewAllMythesis.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable table = new DataTable();
            table.Columns.Add("serialNumber");
            table.Columns.Add("field");
            table.Columns.Add("type");
            table.Columns.Add("title");
            table.Columns.Add("startDate");
            table.Columns.Add("endDate");
            table.Columns.Add("defenseDate");
            table.Columns.Add("years");
            table.Columns.Add("grade");
            table.Columns.Add("payment_id");
            table.Columns.Add("noExtension");
            while (rdr.Read())
            {
                DataRow dataRow = table.NewRow();
                int serialNumber = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));
                String field = rdr.GetString(rdr.GetOrdinal("field"));
                String type = rdr.GetString(rdr.GetOrdinal("type"));
                String title = rdr.GetString(rdr.GetOrdinal("title"));
                DateTime startDate = rdr.GetDateTime(rdr.GetOrdinal("startDate"));
                DateTime endDate = rdr.GetDateTime(rdr.GetOrdinal("endDate"));
                DateTime defenseDate = rdr.GetDateTime(rdr.GetOrdinal("defenseDate"));
                int years = rdr.GetInt32(rdr.GetOrdinal("years"));
              //  Decimal grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));
                int payment_id = rdr.GetInt32(rdr.GetOrdinal("payment_id"));
                int noExtension = rdr.GetInt32(rdr.GetOrdinal("noExtension"));

                dataRow["serialNumber"] = serialNumber;
                dataRow["field"] = field;
                dataRow["type"] = type;
                dataRow["title"] = title;
                dataRow["startDate"] = startDate;
                dataRow["endDate"] = endDate;
                dataRow["defenseDate"] = defenseDate;
                dataRow["years"] = years;
                //dataRow["grade"] = grade;
                dataRow["payment_id"] = payment_id;
                dataRow["noExtension"] = noExtension;
                table.Rows.Add(dataRow);





            }
            GridView1.DataSource = table;
            GridView1.DataBind();





        }
    }
}