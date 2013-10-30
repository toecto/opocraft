using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;


public class Database
    {   
        SqlCeConnection sqlConnection1 = new SqlCeConnection();
        SqlCeCommand sqlCeCom = new SqlCeCommand(); 
        DataTable dt = new DataTable();

public void CreateMySqlCommand() 
  {
    sqlConnection1 = new SqlCeConnection("DataSource = C:\\Users\\Megan\\Documents\\MyDatabase.sdf");   
        sqlConnection1.Open();
       sqlCeCom = new SqlCeCommand("SELECT * FROM BOOKS");
       SqlCeDataAdapter adapter = new SqlCeDataAdapter(sqlCeCom);
       adapter.Fill(dt);
       Console.WriteLine(adapter);
  }
}
    

