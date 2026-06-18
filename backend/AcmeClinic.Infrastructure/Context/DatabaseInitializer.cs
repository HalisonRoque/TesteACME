using Dapper;

namespace AcmeClinic.Infrastructure.Context;

public static class DatabaseInitializer
{
    public static void Initialize(
        DbSQLiteContext factory)
    {
        using var conn =
            factory.Create();

        conn.Open();

        conn.Execute(@"

            CREATE TABLE IF NOT EXISTS Patients(

            Id INTEGER PRIMARY KEY,

            Name TEXT NOT NULL,

            BirthDate TEXT NOT NULL,

            CPF TEXT NOT NULL UNIQUE,

            Gender INTEGER NOT NULL,

            ZipCode TEXT,

            City TEXT,

            Neighborhood TEXT,

            Address TEXT,

            Complement TEXT,

            Active INTEGER

            );

            CREATE TABLE IF NOT EXISTS Appointments(

            Id INTEGER PRIMARY KEY,

            PatientId INTEGER,

            DateTime TEXT,

            Description TEXT,

            Active INTEGER,

            FOREIGN KEY(PatientId)
            REFERENCES Patients(Id)

            );

        ");
    }
}