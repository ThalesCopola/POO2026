using academico.Models;
using academico.Repositories;

namespace academico.Repositories
{
    public class inMemoryProjetoRepository : IProjetoRepository
    {
        private readonly List<Projeto> _projetos = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public inMemoryProjetoRepository()
        {
            // 🔥 DOIS PROJETOS INICIAIS (REQUISITO)
            _projetos.Add(new Projeto
            {
                Id = _nextId++,
                Nome = "Sistema Escolar",
                Sigla = "SE",
                Ano = DateTime.Now.Year,
                Status = Status.EM_DESENVOLVIMENTO
            });

            _projetos.Add(new Projeto
            {
                Id = _nextId++,
                Nome = "App Delivery",
                Sigla = "AD",
                Ano = DateTime.Now.Year,
                Status = Status.IMPLANTADO
            });
        }

        public Task<IEnumerable<Projeto>> GetAll(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_projetos.AsEnumerable());
        }

        public Task<Projeto?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var projeto = _projetos.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(projeto);
        }

        public Task Create(Projeto projeto, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                projeto.Id = _nextId++;
                _projetos.Add(projeto);
            }
            return Task.CompletedTask;
        }

        public Task Edit(Projeto projeto, CancellationToken cancellationToken = default)
        {
            var existente = _projetos.FirstOrDefault(p => p.Id == projeto.Id);

            if (existente != null)
            {
                existente.Nome = projeto.Nome;
                existente.Sigla = projeto.Sigla;
                existente.Ano = projeto.Ano;
                existente.Status = projeto.Status;
            }

            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancellationToken = default)
        {
            var projeto = _projetos.FirstOrDefault(p => p.Id == id);
            if (projeto != null)
                _projetos.Remove(projeto);

            return Task.CompletedTask;
        }

        public Task<Projeto?> GetByID(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}