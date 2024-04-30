using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            return Ok(tarefaBanco);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefasBanco = _context.Tarefas.ToList();

            if (tarefasBanco == null)
                return NotFound();

            return Ok(tarefasBanco);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefasBanco = _context.Tarefas.Where(x => x.Titulo == titulo);

            if (tarefasBanco == null)
                return NotFound();

            return Ok(tarefasBanco);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefasBanco = _context.Tarefas.Where(x => x.Data.Date == data.Date);

            if (tarefasBanco == null)
                return NotFound();
            
            return Ok(tarefasBanco);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefasBanco = _context.Tarefas.Where(x => x.Status == status);

            if (tarefasBanco == null)
                return NotFound();
            
            return Ok(tarefasBanco);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Titulo == "" || tarefa.Titulo != null)
                return BadRequest(new { Erro = "O título da tarefa não pode ser vazio!" });

            if (tarefa.Descricao == "" || tarefa.Descricao != null)
                return BadRequest(new { Erro = "A descrição da tarefa não pode ser vazia!" });

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia!" });

            _context.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Titulo == "" || tarefa.Titulo != null)
                return BadRequest(new { Erro = "O título da tarefa não pode ser vazio!" });

            if (tarefa.Descricao == "" || tarefa.Descricao != null)
                return BadRequest(new { Erro = "A descrição da tarefa não pode ser vazia!" });

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
