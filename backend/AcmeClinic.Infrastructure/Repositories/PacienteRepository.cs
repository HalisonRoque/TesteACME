using Dapper;
using AcmeClinic.Domain.Entities;
using AcmeClinic.Domain.Interfaces;
using AcmeClinic.Infrastructure.Context;

namespace AcmeClinic.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly DbSQLiteContext _db;

    public PacienteRepository(DbSQLiteContext db)
    {
        _db = db;
    }

    public async Task<int> Create(Paciente paciente)
    {
        using var conn = _db.Create();

        var sql =
            @"
                    INSERT INTO Pacientes(
                        Nome,
                        DataNascimento,
                        CPF,
                        Sexo,
                        CEP,
                        Cidade,
                        Bairro,
                        Endereco,
                        Complemento,
                        Status
                    )

                    VALUES(
                        @Nome,
                        @DataNascimento,
                        @CPF,
                        @Sexo,
                        @CEP,
                        @Cidade,
                        @Bairro,
                        @Endereco,
                        @Complemento,
                        @Status
                    );

                    SELECT last_insert_rowid();
                    ";

        return await conn
            .ExecuteScalarAsync<int>(
                sql,
                paciente
            );

    }

    public async Task<IEnumerable<Paciente>> GetAll()
    {
        using var conn =
            _db.Create();

        return await conn
            .QueryAsync<Paciente>(
                @"
                SELECT *
                FROM Pacientes
                "
            );
    }

    public async Task<Paciente?> GetById(int id)
    {
        using var conn =
            _db.Create();

        return await conn
            .QueryFirstOrDefaultAsync<Paciente>(
                @"
                SELECT *
                FROM Pacientes
                WHERE Id=@id
                ",
                new { id }
            );
    }

    public async Task<Paciente?> GetByName(string name)
    {
        using var conn =
            _db.Create();

        return await conn
            .QueryFirstOrDefaultAsync<Paciente>(
                @"
                SELECT *
                FROM Pacientes
                WHERE Name=name
                ",
                new { name }
            );
    }
}