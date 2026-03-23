using academico.Models;

namespace academico.Repositories
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAll(CancellationToken cansellationToken = default);
        Task<Aluno?> GetById(int id, CancellationToken cansellationToken = default);
        Task Create(Aluno aluno, CancellationToken cansellationToken = default);
        Task Edit(Aluno aluno, CancellationToken cansellationToken = default);
        Task Delete(int id, CancellationToken cansellationToken = default);
        Task<bool> Exists(int id, CancellationToken cansellationToken = default);
    }
}
