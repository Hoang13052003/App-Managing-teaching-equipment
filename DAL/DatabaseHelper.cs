using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;

public class DatabaseHelper
{

    //private readonly string connectionString = "Data Source=SQL1001.site4now.net;Initial Catalog=db_ab0082_demoqltb;User Id=db_ab0082_demoqltb_admin;Password=*Hoang@2003*";
    private readonly string connectionString = "Data Source=LAPTOP-H0BOFLN9;Initial Catalog=QlyThietBiDayHoc_BanCu;User ID=sa;Password=123;Encrypt=False";
    //private readonly string connectionString = "Data Source=DESKTOP-FGB2G23;Initial Catalog=QlyThietBiDayHoc;Integrated Security=True";

    //private readonly string connectionString = "Data Source=LAPTOPTQT03;Initial Catalog=QLThietBiDayHoc;Integrated Security=True;Encrypt=False";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public void Open()
    {
        try
        {
            if (GetConnection().State == ConnectionState.Closed)
                GetConnection().Open();
            //MessageBox.Show("Kết nối thành công với cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error opening connection: " + ex.Message);
            // Có thể xử lý hoặc ném exception tùy thuộc vào yêu cầu cụ thể của bạn.
            //MessageBox.Show("Kết nối không thành công với cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public void Close()
    {
        try
        {
            if (GetConnection().State == ConnectionState.Open)
                GetConnection().Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error closing connection: " + ex.Message);
            // Có thể xử lý hoặc ném exception tùy thuộc vào yêu cầu cụ thể của bạn.
        }
    }

    public int GetNonQuery(string SQLQueryString, SqlParameter[] para)
    {
        int result = 0;
        try
        {
            Open();
            using (SqlCommand cmd = new SqlCommand(SQLQueryString, GetConnection()))
            {
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error executing non-query: " + ex.Message);
            // Có thể xử lý hoặc ném exception tùy thuộc vào yêu cầu cụ thể của bạn.
        }
        finally
        {
            Close();
        }
        return result;
    }
    //public int GetNonQuery(string SQLProc, SqlParameter[] para)
    //{
    //    int result = 0;
    //    try
    //    {
    //        Open();
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            cmd.CommandText = SQLProc;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Connection = GetConnection();
    //            if (para != null)
    //            {
    //                cmd.Parameters.AddRange(para);
    //            }
    //            result = cmd.ExecuteNonQuery();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("Error executing non-query: " + ex.Message);
    //        // Có thể xử lý hoặc ném exception tùy thuộc vào yêu cầu cụ thể của bạn.
    //    }
    //    finally
    //    {
    //        Close();
    //    }
    //    return result;
    //}

    public int GetExecuteScalar(string SQLQueryString)
    {
        int result = 0;
        try
        {
            Open();
            using (SqlCommand cmd = new SqlCommand(SQLQueryString, GetConnection()))
            {
                result = (int)cmd.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error executing scalar: " + ex.Message);
            // Có thể xử lý hoặc ném exception tùy thuộc vào yêu cầu cụ thể của bạn.
        }
        finally
        {
            Close();
        }
        return result;
    }

    public DataTable GetDataTable(string SQLQueryString)
    {
        DataTable dataTable = new DataTable();
        try
        {
            Open();
            using (SqlDataAdapter da = new SqlDataAdapter(SQLQueryString, GetConnection()))
            {
                da.Fill(dataTable);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error getting data table: " + ex.Message);
            // Có thể xử lý hoặc ném exception tùy thuộc vào yêu cầu cụ thể của bạn.
        }
        finally
        {
            Close();
        }
        return dataTable;
    }
    public DataTable GetDataTableQuery(string query, SqlParameter[] parameters = null)
    {
        DataTable dataTable = new DataTable();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    try
                    {
                        connection.Open();
                        adapter.Fill(dataTable);
                    }
                    catch (Exception ex)
                    {
                        // Ghi log hoặc xử lý lỗi ở đây
                        Console.WriteLine($"Error getting data table: {ex.Message}");
                        // Trả về một DataTable mặc định
                        return new DataTable(); // Trả về một DataTable rỗng nếu có lỗi
                    }
                }
            }
        }

        return dataTable;
    }

    public DataTable GetDataTable(string SQLProc, SqlParameter[] para)
    {
        DataTable dataTable = new DataTable();
        try
        {
            Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SQLProc;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = GetConnection();
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dataTable);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error getting data table: " + ex.Message);
            // Có thể xử lý hoặc ném exception tùy thuộc vào yêu cầu cụ thể của bạn.
        }
        finally
        {
            Close();
        }
        return dataTable;
    }
}
