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
            INSERT INTO Atendimentos (
                PacienteId,
                Data,
                Hora,
                Descricao,
                Status
            )

            VALUES(
                @PacienteId,
                @Data,
                @Hora,
                @Descricao,
                @Status
            );

            SELECT last_insert_rowid();
            ",
            atendimento
        );
    }

    public async Task<IEnumerable<dynamic>> GetAllAntendimentos(
        int? pacienteId,
        string? pacienteNome,
        string? status,
        DateTime? dataInicio,
        DateTime? dataFim,
        int page,
        int pageSize
    )
    {
        using var conn = _context.Create();

        var offset = (page - 1) * pageSize;

        return await conn.QueryAsync(
        @"
            SELECT
                a.Id,
                a.PacienteId,
                p.Nome AS PacienteNome,
                a.Data,
                a.Hora,
                a.Descricao,
                a.Status

            FROM Atendimentos a
            INNER JOIN Pacientes p
                ON p.Id = a.PacienteId

            WHERE(
                @pacienteId IS NULL OR a.PacienteId=@pacienteId
            )

            AND (
                @pacienteNome IS NULL
                OR @pacienteNome=''
                OR p.Nome LIKE '%' || @pacienteNome || '%'
            )

            AND (
                @status IS NULL 
                OR @status=''
                OR a.Status=@status
            )

            AND(
                @dataInicio IS NULL
                OR a.Data>=@dataInicio
            )

            AND (
                @dataFim IS NULL
                OR a.Data<=@dataFim
            )

            ORDER BY a.Data DESC
            LIMIT @pageSize
            OFFSET @offset
            ",
            new
            {
                pacienteId,
                pacienteNome,
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
            Data=@Data,
            Hora=@Hora,
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
    string? pacienteNome,
    string? status,
    DateTime? dataInicio,
    DateTime? dataFim
    )
    {
        using var conn = _context.Create();

        return await conn.ExecuteScalarAsync<int>(
        @"
            SELECT COUNT(*)
            FROM Atendimentos a

            INNER JOIN Pacientes p
                ON p.Id = a.PacienteId

            WHERE (@pacienteId IS NULL OR a.PacienteId=@pacienteId)

            AND (
                @pacienteNome IS NULL
                OR @pacienteNome=''
                OR p.Nome LIKE '%' || @pacienteNome || '%'
            )

            AND (
                @status IS NULL
                OR @status=''
                OR a.Status=@status
            )

            AND (
                @dataInicio IS NULL
                OR a.Data>=@dataInicio
            )

            AND (
                @dataFim IS NULL
                OR a.Data<=@dataFim
            )
        ",
        new
        {
            pacienteId,
            pacienteNome,
            status,
            dataInicio,
            dataFim
        });
    }
}