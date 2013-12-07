using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;


public class Database
{   
    public SqlCeConnection Connection;
    public Exception exeption;

    public Database(string connectionString)
    {
        this.Connection=new SqlCeConnection(connectionString);
        this.Connection.Open();
    }

    public void Close()
    {
        this.Connection.Close();
    }

    public DataTable Query(string Query, SqlCeParameter[] parameters=null)
    {
        SqlCeCommand SqlCommand = new SqlCeCommand(Query, this.Connection);
        if (parameters != null)
            SqlCommand.Parameters.AddRange(parameters);
            
        SqlCeDataAdapter Adapter = new SqlCeDataAdapter(SqlCommand);
        
        DataTable Results = new DataTable();
        try
        {
            Adapter.Fill(Results);
        }
        catch (Exception e)
        {
            this.exeption = e;
            return null;
        }
        return Results;
    }

    public void NonQuery(string Query, SqlCeParameter[] parameters = null)
    {
        SqlCeCommand SqlCommand = new SqlCeCommand(Query);
        if (parameters != null)
            SqlCommand.Parameters.AddRange(parameters);
        SqlCommand.ExecuteNonQuery();
    }

    public void ScalarQuery(string Query)
    {
        SqlCeCommand SqlCommand = new SqlCeCommand(Query);
        SqlCommand.ExecuteScalar();
         // to get a single value from the Query
        return;
    }

}
    

