using System;
using System.Data;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;

class DatabaseMigrator
{
    private const string sqliteConnectionString = "Data Source=C:\\Users\\Kev_i\\source\\repos\\Krono\\Krono\\Infrastructure\\KronoDB.db";
    private const string mysqlConnectionString = "";

    public static void Main()
    {
        using var sqlite = new SqliteConnection(sqliteConnectionString);
        using var mysql = new MySqlConnection(mysqlConnectionString);

        sqlite.Open();
        mysql.Open();

        Console.WriteLine("Connected to both databases.");

        MigrateTable("Products", sqlite, mysql);
        MigrateTable("Shops", sqlite, mysql);
        MigrateTable("PriceEntries", sqlite, mysql);
        MigrateTable("Barcodes", sqlite, mysql);

        Console.WriteLine("✅ Migration completed.");
    }

    static void MigrateTable(string tableName, SqliteConnection sqlite, MySqlConnection mysql)
    {
        Console.WriteLine($"🔄 Migrating table: {tableName}");

        using var cmd = new SqliteCommand($"SELECT * FROM {tableName}", sqlite);
        using var reader = cmd.ExecuteReader();

        var schema = reader.GetSchemaTable();
        var columnNames = new List<string>();
        var paramNames = new List<string>();

        foreach (DataRow row in schema.Rows)
        {
            var colName = row["ColumnName"].ToString();
            columnNames.Add(colName);
            paramNames.Add("@" + colName);
        }

        var insertSql = $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) VALUES ({string.Join(",", paramNames)})";

        using var insert = new MySqlCommand(insertSql, mysql);

        while (reader.Read())
        {
            insert.Parameters.Clear();

            for (int i = 0; i < columnNames.Count; i++)
            {
                var val = reader.IsDBNull(i) ? null : reader.GetValue(i);
                insert.Parameters.AddWithValue(paramNames[i], val ?? DBNull.Value);
            }

            try
            {
                insert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to insert row into {tableName}: {ex.Message}");
            }
        }

        Console.WriteLine($"✅ Finished migrating table: {tableName}");
    }
}
