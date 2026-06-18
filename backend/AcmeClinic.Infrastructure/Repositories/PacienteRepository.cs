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


    public async Task<IEnumerable<Paciente>> GetAll(
        string? nome,
        string? cpf,
        string? status,
        int page,
        int pageSize
    )
    {
        using var conn = _db.Create();

        var offset = (page - 1) * pageSize;

        return await conn
        .QueryAsync<Paciente>(
            @"
                SELECT
                    Id,
                    Nome,
                    CPF,
                    Sexo,
                    Cidade,
                    Status

                FROM Pacientes

                WHERE (
                @nome IS NULL
                OR Nome LIKE '%' || @nome || '%'
                )

                AND (
                @cpf IS NULL
                OR CPF=@cpf
                )

                AND (
                @status IS NULL
                OR Status=@status
                )

                LIMIT @pageSize
                OFFSET @offset
            ",
            new {
                nome,
                cpf,
                status,
                pageSize,
                offset
            }
        );
    }


    public async Task<Paciente?> GetById(int id)
    {
        using var conn = _db.Create();

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

    public async Task UpdatePaciente(Paciente paciente)
    {
        using var conn = _db.Create();

        await conn.ExecuteAsync(
            @"    
                UPDATE Pacientes

                SET
                Nome=@Nome,
                DataNascimento=@DataNascimento,
                Sexo=@Sexo,
                CEP=@CEP,
                Cidade=@Cidade,
                Bairro=@Bairro,
                Endereco=@Endereco,
                Complemento=@Complemento,
                Status=@Status

                WHERE Id=@Id    
            ", paciente
        );
    }

    public async Task InactivatePaciente(int id)
    {
        using var conn = _db.Create();

        await conn.ExecuteAsync(
            @"
                UPDATE Pacientes
                SET Status='Inativo'
                WHERE Id=@id
            ",
            new { id }
        );
    }

    public async Task ActivatePaciente(int id)
    {
        using var conn = _db.Create();

        await conn.ExecuteAsync(
            @"
                UPDATE Pacientes
                SET Status='Ativo'
                WHERE Id=@id
            ",
            new { id }
        );
    }

    public async Task<bool> ExistsCPF(string cpf)
    {
        using var conn = _db.Create();

        var total = await conn
        .ExecuteScalarAsync<int>(
            @"
                SELECT COUNT(*)
                FROM Pacientes
                WHERE CPF=@cpf
            ",
            new { cpf }
        );

        return total > 0;
    }

    public async Task<int> Count(
        string? nome,
        string? cpf,
        string? status
    )
    {
        using var conn = _db.Create();
    
        return await conn
        .ExecuteScalarAsync<int>(
            @"

            SELECT COUNT(*)

            FROM Pacientes

            WHERE (
            @nome IS NULL
            OR Nome LIKE '%' || @nome || '%'
            )

            AND (
            @cpf IS NULL
            OR CPF=@cpf
            )

            AND (
            @status IS NULL
            OR Status=@status
            )

            ",
            new {
                nome,
                cpf,
                status
            }
        );
    }
}