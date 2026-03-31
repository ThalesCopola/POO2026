using Microsoft.AspNetCore.Mvc;
using academico.Models;
using academico.Repositories;

namespace academico.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoController(IProjetoRepository repository)
        {
            _projetoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IActionResult> Index()
        {
            var projetos = await _projetoRepository.GetAll();
            return View(projetos);
        }

        public IActionResult Create()
        {
            return View(new Projeto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projeto projeto)
        {
            if (!ModelState.IsValid)
            {
                return View(projeto);
            }

            await _projetoRepository.Create(projeto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var projeto = await _projetoRepository.GetById(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return View(projeto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projeto projeto)
        {
            if (id != projeto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(projeto);
            }

            await _projetoRepository.Edit(projeto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var projeto = await _projetoRepository.GetById(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return View(projeto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var projeto = await _projetoRepository.GetById(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return View(projeto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _projetoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}