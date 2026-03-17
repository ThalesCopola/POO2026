using Microsoft.AspNetCore.Mvc;
using academico.Models;

namespace academico.Controllers
{
    public class AlunoController : Controller
    {
        private static List<Aluno> alunos = new List<Aluno>()
        {
            new Aluno()
            {
                AlunoId = 1,
                Nome = "Aluno Teste",
                Email = "aluno@gmail.com",
                Telefone = "(99) 99999-9999",
                Endereco = "Rua X, Numero 123",
                Complemento = "apto 1001",
                Bairro = "Bairro do aluno",
                Municipio = "Cidade do aluno",
                Uf = "SP",
                Cep = "27100-000"
            }
        };
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    aluno.AlunoId = alunos
                        .Select(a => a.AlunoId)
                        .DefaultIfEmpty(0)
                        .Max() + 1;

                    alunos.Add(aluno);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    $"Ocorreu um erro ao criar o aluno: {ex.Message}");
            }

            return View(aluno);
        }
        public IActionResult Index()
        {
            return View(alunos);
        }

        public IActionResult Edit(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoId == id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("AlunoId,Nome,Email,Telefone,Endereco,Complemento,Bairro,Municipio,Uf,Cep")] Aluno aluno)
        {
            try
            {
                if (id != aluno.AlunoId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var existingAluno = alunos.FirstOrDefault(a => a.AlunoId == id);

                    if (existingAluno == null)
                    {
                        return NotFound();
                    }

                    existingAluno.Nome = aluno.Nome;
                    existingAluno.Email = aluno.Email;
                    existingAluno.Telefone = aluno.Telefone;
                    existingAluno.Endereco = aluno.Endereco;
                    existingAluno.Complemento = aluno.Complemento;
                    existingAluno.Bairro = aluno.Bairro;
                    existingAluno.Municipio = aluno.Municipio;
                    existingAluno.Uf = aluno.Uf;
                    existingAluno.Cep = aluno.Cep;

                    return RedirectToAction("");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    $"Ocorreu um erro ao atualizar o aluno: {ex.Message}");
            }

            return View(aluno);
        }

        public IActionResult Details(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        public IActionResult Delete(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var aluno = alunos.FirstOrDefault(a => a.AlunoId == id);
                if (aluno == null)
                {
                    return NotFound();
                }
                alunos.Remove(aluno);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    $"Ocorreu um erro ao excluir o aluno: {ex.Message}");
                return View();
            }
        }
    }
}