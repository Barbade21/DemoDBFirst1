using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DemoDBFirst.Models;

namespace DemoDBFirst.Repositories
{
    public static class StudentRepository
    {
        private static string GetConnectionString()
        {
            var cs = ConfigurationManager.ConnectionStrings["StudentDb"];
            return cs?.ConnectionString;
        }

        public static IEnumerable<Student> GetAll()
        {
            var list = new List<Student>();
            var connStr = GetConnectionString();
            if (string.IsNullOrEmpty(connStr)) return list;

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("SELECT Id, Name,  Age FROM Students ORDER BY Id", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Student
                        {
                            Id = r.GetInt32(0),
                            Name = r.IsDBNull(1) ? null : r.GetString(1),
                            //Email = r.IsDBNull(2) ? null : r.GetString(2),
                            Age = r.IsDBNull(2) ? 0 : r.GetInt32(2)
                        });
                    }
                }
            }

            return list;
        }

        public static Student Get(int id)
        {
            var connStr = GetConnectionString();
            if (string.IsNullOrEmpty(connStr)) return null;

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("SELECT Id, Name, Age FROM Students WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new Student
                        {
                            Id = r.GetInt32(0),
                            Name = r.IsDBNull(1) ? null : r.GetString(1),
                            //Email = r.IsDBNull(2) ? null : r.GetString(2),
                            Age = r.IsDBNull(2) ? 0 : r.GetInt32(2)
                        };
                    }
                }
            }

            return null;
        }

        public static Student Add(Student student)
        {
            var connStr = GetConnectionString();
            if (string.IsNullOrEmpty(connStr)) return null;

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("INSERT INTO Students (Name,  Age) OUTPUT INSERTED.Id VALUES (@name,  @age)", conn))
            {
                cmd.Parameters.AddWithValue("@name", (object)student.Name ?? DBNull.Value);
                //cmd.Parameters.AddWithValue("@email", (object)student.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@age", student.Age);
                conn.Open();
                var id = (int)cmd.ExecuteScalar();
                student.Id = id;
                return student;
            }
        }

        public static void Update(Student student)
        {
            var connStr = GetConnectionString();
            if (string.IsNullOrEmpty(connStr)) return;

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("UPDATE Students SET Name = @name,  Age = @age WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@name", (object)student.Name ?? DBNull.Value);
                //cmd.Parameters.AddWithValue("@email", (object)student.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@age", student.Age);
                cmd.Parameters.AddWithValue("@id", student.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            var connStr = GetConnectionString();
            if (string.IsNullOrEmpty(connStr)) return;

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("DELETE FROM Students WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}