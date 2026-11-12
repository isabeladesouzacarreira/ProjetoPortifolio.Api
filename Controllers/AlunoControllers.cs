using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ProjetoPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _context.Alunos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
                return NotFound("Aluno não encontrado.");

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(AlunosDTO dto)
        {
            var turma = await _context.Turmas.FindAsync(dto.id_turma);

            if (turma == null)
                return BadRequest("A turma informada não existe.");

            var aluno = new Aluno
            {
                nome_aluno = dto.nome_aluno,
                id_turma = dto.id_turma
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Buscar), new { id = aluno.Id }, aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, AlunosDTO dto)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
                return NotFound("Aluno não encontrado.");

            var turma = await _context.Turmas.FindAsync(dto.id_turma);

            if (turma == null)
                return BadRequest("A turma informada não existe.");

            aluno.nome_aluno = dto.nome_aluno;
            aluno.id_turma = dto.id_turma;

            await _context.SaveChangesAsync();

            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
                return NotFound("Aluno não encontrado.");

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}