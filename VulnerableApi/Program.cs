using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/appointments/{id}", (string id) =>
{
    using var connection = new SqliteConnection("Data Source=test.db;");
    connection.Open();

    using var cmd = new SqliteCommand($"select id, [date], text from appointments where id = {id}", connection);

    using var reader = cmd.ExecuteReader();

    var responses = new List<object>();
    while (reader.Read())
    {
        responses.Add(new
        {
            Id = reader["id"],
            Date = reader["date"],
            Text = reader["text"]
        });
    }

    return responses;
});

app.Run();