using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QUANLYNHAHANG
{
    internal class BanHang
    {
        SqlConnection con;

        public BanHang()
        {
            con = new SqlConnection(
                @"Data source=.;Initial Catalog=QLNHT;Integrated Security=True");

            if (con.State == ConnectionState.Closed)
                con.Open();
        }


        public DataTable LayDuLieu(string procedure, List<SqlParameter> pars = null)
        {
            using (SqlCommand cmd = new SqlCommand(procedure, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (pars != null)
                {
                    foreach (SqlParameter p in pars)
                        cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value)); // FIX reuse
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ==========================
        // INSERT / UPDATE / DELETE
        // ==========================
        public int XuLy(string procedure, List<SqlParameter> pars)
        {
            using (SqlCommand cmd = new SqlCommand(procedure, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter p in pars)
                    cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value)); // FIX reuse

                return cmd.ExecuteNonQuery();
            }
        }
        public object ExecuteScalar(string sql, SqlParameter[] pars)
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;

                if (pars != null)
                    cmd.Parameters.AddRange(pars);

                return cmd.ExecuteScalar();
            }
        }
    }
}