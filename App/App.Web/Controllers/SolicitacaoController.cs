using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using App.Web.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("upload")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            try
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                long size = files.Sum(f => f.Length);

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {

                        var filePath = formFile.FileName;

                        // Randomize the filename everytime so we don't overwrite files
                        string randomFile = Regex.Replace(Path.GetFileNameWithoutExtension(filePath), "[^a-zA-Z0-9.]+", "-", RegexOptions.Compiled)
                                            + "_"
                                            + Guid.NewGuid().ToString().Substring(0, 4)
                                            + Path.GetExtension(filePath);

                        string targetFile = Path.Combine(uploadsFolder, randomFile);

                        using (var destinationStream = System.IO.File.Create(targetFile))
                        {
                            await formFile.CopyToAsync(destinationStream);
                        }

                        var file = "/uploads/" + randomFile;

                        return Ok(file);
                    }
                }

            } catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Forbid();
        }

        private bool SolicitacaoExists(int id)
        {
            return _context.Solicitacoes.Any(e => e.Id == id);
        }
    }
}