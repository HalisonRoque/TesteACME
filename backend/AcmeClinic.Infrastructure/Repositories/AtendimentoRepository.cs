using Dapper;
using AcmeClinic.Domain.Entities;
using AcmeClinic.Domain.Interfaces;
using AcmeClinic.Infrastructure.Context;

public class AtendimentoRepository : IAtendimentoRepository
{
    private readonly DbSQLiteContext _context;

    public AtendimentoRepository(DbSQLiteContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAtendimento(Atendimento atendimento)
    {
        using var conn = _context.Create();

        return await conn.ExecuteScalarAsync<int>(
        @"

            INSERT INTO Atendimentos
            (
            PacienteId,
            DataHora,
            Descricao,
            Status
            )

            VALUES
            (
            @PacienteId,
            @DataHora,
            @Descricao,
            @Status
            );

            SELECT last_insert_rowid();

            ",
            atendimento
        );
    }

    public async Task<IEnumerable<Atendimento>> GetAllAntendimentos(
        int? pacienteId,
        string? status,
        DateTime? dataInicio,
        DateTime? dataFim,
        int page,
        int pageSize
    )
    {
        using var conn = _context.Create();

        var offset = (page - 1) * pageSize;

        return await conn.QueryAsync<Atendimento>(
            @"
                SELECT *
                FROM Atendimentos

                WHERE (
                @pacienteId IS NULL
                OR PacienteId=@pacienteId
                )

                AND (
                @status IS NULL
                OR Status=@status
                )

                AND (
                @dataInicio IS NULL
                OR DataHora>=@dataInicio
                )

                AND (
                @dataFim IS NULL
                OR DataHora<=@dataFim
                )

                LIMIT @pageSize
                OFFSET @offset
            ",
            new {
            pacienteId,
            status,
            dataInicio,
            dataFim,
            pageSize,
            offset
        });
    }

    public async Task<Atendimento?> GetById(int id)
    {
        using var conn = _context.Create();

        return await conn.QueryFirstOrDefaultAsync<Atendimento>(
                "SELECT * FROM Atendimentos WHERE Id=@id",
                new { id }
        );
    }

    public async Task UpdateAtendimento(Atendimento atendimento)
    {
        using var conn = _context.Create();

        await conn.ExecuteAsync(
        @"
            UPDATE Atendimentos    
            SET
            DataHora=@DataHora,
            Descricao=@Descricao,
            Status=@Status

            WHERE Id=@Id
            ",
            atendimento
        );
    }

    public async Task InactivateAntedimento(int id)
    {
        using var conn = _context.Create();

        await conn.ExecuteAsync(
        @"
            UPDATE Atendimentos
            SET Status='Inativo'
            WHERE Id=@id
        ",
        new { id });
    }

    public async Task ActivateAntedimento(int id)
    {
        using var conn = _context.Create();

        await conn.ExecuteAsync(
        @"
            UPDATE Atendimentos
            SET Status='Ativo'
            WHERE Id=@id
        ",
        new { id });
    }

    public async Task<int> Count(
        int? pacienteId,
        string? status,
        DateTime? dataInicio,
        DateTime? dataFim
    )
    {
        using var conn = _context.Create();

        return await conn.ExecuteScalarAsync<int>(
        @"
            SELECT COUNT(*)
            FROM Atendimentos
        ",
        new {}
        );
    }
}