using Microsoft.Data.SqlClient;

namespace Calc
{
    public class RepositorySQL : IRepository
    {
        public readonly SQLConnection connection;
        public List<MathLog> Memory { get; set; } = new List<MathLog>();
        public int MemoryPosition { get; set; }
        public RepositorySQL()
        {
            connection = new SQLConnection();
        }

        public void SaveMemory(string filePath)
        {
            using (var database = connection.Connect())
            {
                var entities = Memory.ToEntities();
                database.Open();
                var clearTable = new SqlCommand("truncate table MathLogEntities;", database);
                clearTable.ExecuteNonQuery();
                foreach (var entity in entities)
                {                    
                    var cmd = new SqlCommand("INSERT INTO MathLogEntities (Math, ResultValue, ResultUnit) VALUES (@Math, @ResultValue, @ResultUnit)", database);
                    cmd.Parameters.AddWithValue("@Math", entity.Math);
                    cmd.Parameters.AddWithValue("@ResultValue", entity.ResultValue);
                    cmd.Parameters.AddWithValue("@ResultUnit", entity.ResultUnit);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadMemory(string filePath)
        {
            var entities = new List<MathLogEntity>();

            using (var conn = connection.Connect())
            {
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM MathLogEntities", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    entities.Add(new MathLogEntity
                    {
                        Math = reader["Math"].ToString(),
                        ResultValue = (double)reader["ResultValue"],
                        ResultUnit = reader["ResultUnit"].ToString()
                    });
                }
            }
            foreach (var item in entities)
            {
                Memory.Add(item.FromEntity());
            }
            MemoryPosition = 0;
        }
    }
}

