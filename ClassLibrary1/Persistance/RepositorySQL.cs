using Contracts;
using Microsoft.Data.SqlClient;

namespace Calc.Persistance
{
    public class RepositorySQL : IRepository
    {
        public readonly DatabaseConnection _databaseConnection;
        public List<MathLog> Memory { get; set; } = new List<MathLog>();
        public int MemoryPosition { get; set; }
        public RepositorySQL()
        {
            _databaseConnection = new DatabaseConnection();
        }

        public void SaveMemory(string filePath)
        {
            using (var connection = _databaseConnection.Connect())
            {
                var entities = Memory.ToEntities();
                connection.Open();
                var clearTable = new SqlCommand("truncate table MathLogEntities;", connection);
                clearTable.ExecuteNonQuery();
                foreach (var entity in entities)
                {
                    var command = new SqlCommand("INSERT INTO MathLogEntities (Math, ResultValue, ResultUnit) VALUES (@Math, @ResultValue, @ResultUnit)", connection);
                    command.Parameters.AddWithValue("@Math", entity.Math);
                    command.Parameters.AddWithValue("@ResultValue", entity.ResultValue);
                    command.Parameters.AddWithValue("@ResultUnit", entity.ResultUnit);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void LoadMemory(string filePath)
        {
            var entities = new List<MathLogEntity>();

            using (var dbCalculator = _databaseConnection.Connect())
            {
                dbCalculator.Open();

                var command = new SqlCommand("SELECT * FROM MathLogEntities", dbCalculator);
                var reader = command.ExecuteReader();

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

