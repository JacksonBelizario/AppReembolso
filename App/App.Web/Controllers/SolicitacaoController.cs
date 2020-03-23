using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Web.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SolicitacaoController(SistemaDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<string> OnPostUploadAsync(List<IFormFile> files)
        {
            try
            {
                string folder = Path.GetFullPath("~/Uploads");
                string uploadsFolder = Path.GetDirectoryName(folder);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                long size = files.Sum(f => f.Length);

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {

                        var filePath = Path.GetTempFileName();

                        // Randomize the filename everytime so we don't overwrite files
                        string randomFile = Path.GetFileNameWithoutExtension(filePath)
                                            + "_"
                                            + Guid.NewGuid().ToString().Substring(0, 4)
                                            + Path.GetExtension(filePath);

                        string targetFile = Path.GetFullPath(Path.Combine(uploadsFolder, randomFile));

                        using (var destinationStream = System.IO.File.Create(targetFile))
                        {
                            await formFile.CopyToAsync(destinationStream);
                        }

                        return "/Uploads/" + randomFile;
                       /* using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }*/
                    }
                }

            } catch(Exception ex)
            {
                return ex.Message;
            }

            return "";

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

           //  return Ok(new { count = files.Count, size });
        }

        private bool SolicitacaoExists(int id)
        {
            return _context.Solicitacoes.Any(e => e.Id == id);
        }
    }
}