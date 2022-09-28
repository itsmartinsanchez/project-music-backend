namespace Song_Project.Services;

using System.Collections.Generic;
using System.Data.SqlClient;

using Song_Project.Models;

public class RawArtistsService : IArtistsService
{
    private const string ArtistsTable = "Artists";

    public List<Artists> GetAll()
    {
        List<Artists> artists = new List<Artists>();

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT Id, Name FROM " + ArtistsTable;

        SqlCommand command = new SqlCommand(sql, connection);

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            artists.Add(
                new Artists(
                    (int)reader["Id"],
                    (string)reader["Name"]
                )
            );
        }

        connection.Close();

        return artists;
    }

    public Artists FindById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string name)
    {
        throw new NotImplementedException();
    }

    public Artists FindByName(string name)
    {
        throw new NotImplementedException();
    }

    public Artists Save(Artists a)
    {
        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "INSERT INTO Artists (Name) VALUES (@Name)";

        if(a.Id != null) {
            // UPDATE
            sql = "UPDATE Artists SET Name=@Name WHERE Id=@Id";
        }

        SqlCommand command = new SqlCommand(sql, connection);

        if(a.Id != null) {
            command.Parameters.AddWithValue("@Id", a.Id);
        }

        command.Parameters.AddWithValue("@Name", a.Name);

        command.ExecuteNonQuery();

        connection.Close();

        return a;
    }

    public Artists Save(Dictionary<string, object> hash)
    {
        Int32 id       = Int32.Parse(hash["id"].ToString());
        String name    = hash["name"].ToString();

        Artists temp = new Artists(id, name);

        return this.Save(temp);
    }

    public Artists Delete(Artists a)
    {
        throw new NotImplementedException();
    }
}