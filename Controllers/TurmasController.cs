using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ProjetoPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _context.Turmas.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
                return NotFound("Turma não encontrada.");

            return Ok(turma);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(TurmaDTO dto)
        {
            var turma = new Turma
            {
                nome_turma = dto.nome_turma
            };

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Buscar), new { id = turma.id_turma }, turma);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, TurmaDTO dto)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
                return NotFound("Turma não encontrada.");

            turma.nome_turma = dto.nome_turma;

            await _context.SaveChangesAsync();

            return Ok(turma);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
                return NotFound("Turma não encontrada.");

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}