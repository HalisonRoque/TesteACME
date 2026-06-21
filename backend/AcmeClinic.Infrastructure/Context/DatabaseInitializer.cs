using Dapper;

namespace AcmeClinic.Infrastructure.Context;

public static class DatabaseInitializer
{
    public static void Initialize(
        DbSQLiteContext factory)
    {
        using var conn = factory.Create();

        conn.Open();

        conn.Execute(@"
            CREATE TABLE IF NOT EXISTS Pacientes(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,

                Nome VARCHAR(150) NOT NULL,

                DataNascimento DATETIME NOT NULL,

                CPF VARCHAR(11) NOT NULL UNIQUE,

                Sexo VARCHAR(20) NOT NULL,

                CEP VARCHAR(8) NOT NULL,

                Cidade VARCHAR(100) NOT NULL,

                Bairro VARCHAR(100) NOT NULL,

                Endereco VARCHAR(200) NOT NULL,

                Complemento VARCHAR(200),

                Status VARCHAR(10) NOT NULL
                    CHECK(Status IN ('Ativo','Inativo'))
            );

            CREATE TABLE IF NOT EXISTS Atendimentos(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,

                PacienteId INTEGER NOT NULL,

                Data DATE NOT NULL,

                Hora TEXT NOT NULL,

                Descricao TEXT NOT NULL,

                Status VARCHAR(10) NOT NULL
                    CHECK(Status IN ('Ativo','Inativo')),

                FOREIGN KEY(PacienteId)
                REFERENCES Pacientes(Id)

            );

        ");
    }
}