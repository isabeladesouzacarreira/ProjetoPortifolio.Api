using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPortifolio.Api.DTO;


namespace ProjetoPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _context.Projetos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
                return NotFound("Projeto não encontrado.");

            return Ok(projeto);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ProjetoDTO dto)
        {
            var aluno = await _context.Alunos.FindAsync(dto.id_aluno);

            if (aluno == null)
                return BadRequest("O aluno informado não existe.");

            var projeto = new Projeto
            {
                nome_projeto = dto.nome_projeto,
                imagem_video = dto.imagem_video,
                descricao = dto.descricao,
                id_aluno = dto.id_aluno
            };

            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Buscar), new { id = projeto.Id_projeto }, projeto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, ProjetoDTO dto)
        {
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
                return NotFound("Projeto não encontrado.");

            var aluno = await _context.Alunos.FindAsync(dto.id_aluno);

            if (aluno == null)
                return BadRequest("O aluno informado não existe.");

            projeto.nome_projeto = dto.nome_projeto;
            projeto.imagem_video = dto.imagem_video;
            projeto.descricao = dto.descricao;
            projeto.id_aluno = dto.id_aluno;

            await _context.SaveChangesAsync();

            return Ok(projeto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
                return NotFound("Projeto não encontrado.");

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}