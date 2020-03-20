using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static App.Web.Models.Enums;

namespace App.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoController : ControllerBase
    {
        private readonly SistemaDbContext _context;

        public SolicitacaoController(SistemaDbContext context)
        {
            _context = context;
        }

        // GET: api/Solicitacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitacao>>> GetSolicitacoes()
        {
            return await _context.Solicitacoes.Where(e => e.Status != (int)Status.Excluida).ToListAsync();
        }

        // GET: api/Solicitacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitacao>> GetSolicitacao(int id)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);

            if (solicitacao == null)
            {
                return NotFound();
            }

            return solicitacao;
        }

        // PUT: api/Solicitacoes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitacao(int id, Solicitacao solicitacao)
        {
            if (id != solicitacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Solicitacoes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Solicitacao>> PostSolicitacao(Solicitacao solicitacao)
        {

            _context.Solicitacoes.Add(solicitacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitacao", new { id = solicitacao.Id }, solicitacao);
        }

        // DELETE: api/Solicitacoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Solicitacao>> DeleteSolicitacao(int id)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return NotFound();
            }

            solicitacao.Status = (int)Status.Excluida;

            _context.Entry(solicitacao).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return solicitacao;
        }

        private bool SolicitacaoExists(int id)
        {
            return _context.Solicitacoes.Any(e => e.Id == id);
        }
    }
}