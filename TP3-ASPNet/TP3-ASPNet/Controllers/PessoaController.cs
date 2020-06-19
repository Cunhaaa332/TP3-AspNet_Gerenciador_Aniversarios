using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP3_ASPNet.Dados;
using TP3_ASPNet.Models;

namespace TP3_ASPNet.Controllers
{
    public class PessoaController : Controller
    {

        // GET: Pessoa
        [Route("Pessoa/")]
        public ActionResult Index()
        {
            var pessoa = DadosPessoa.Listar();
            return View(pessoa);
        }

        // GET: Buscar
        [Route("Pessoa/Buscar")]
        public ActionResult Buscar() {
            var pessoa = DadosPessoa.pessoas.Where(pessoa => pessoa.Nome.Contains(HttpContext.Request.Form["Nome"],StringComparison.InvariantCultureIgnoreCase) || pessoa.SobreNome.Contains(HttpContext.Request.Form["Nome"], StringComparison.InvariantCultureIgnoreCase));
            return View(pessoa);
        }

        // GET: Pessoa/Details/5
        [Route("Pessoa/DetalhesPessoa/{id}")]
        public ActionResult DetalhesPessoa(int id)
        {
            foreach (var pessoa in DadosPessoa.pessoas) {
                if (pessoa.Id == id) {
                    return View(pessoa);
                }
            }
            return View();
        }

        // GET: Pessoa/Criar
        [Route("Pessoa/Criar")]
        public ActionResult Criar()
        {
            return View();
        }

        // POST: Pessoa/Criar
        [Route("Pessoa/Criar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(PessoaModel pessoa)
        {
            try
            {
                DadosPessoa.pessoas.Add(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Edit/5
        [Route("Pessoa/Editar/{id}")]
        public ActionResult Editar(int id)
        {
            foreach (var pessoa in DadosPessoa.pessoas) {
                if (pessoa.Id == id) {
                    return View(pessoa);
                }
            }
            return View();
        }

        // POST: Pessoa/Edit/5
        [Route("Pessoa/Editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, PessoaModel pessoa)
        {
            try
            {
                foreach (var objeto in DadosPessoa.pessoas) {
                    if (objeto.Id == id) {
                        objeto.Nome = HttpContext.Request.Form["Nome"];
                        objeto.SobreNome = HttpContext.Request.Form["Sobrenome"];
                        objeto.birth = DateTime.Parse(HttpContext.Request.Form["Nome"]);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Delete/5
        [Route("Pessoa/Deletar/{id}")]
        public ActionResult Deletar(int id)
        {
            foreach (var pessoa in DadosPessoa.pessoas) {
                if (pessoa.Id == id) {
                    return View(pessoa);
                }
            }
            return View();
        }

        // POST: Pessoa/Delete/5
        [Route("Pessoa/Deletar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int id, PessoaModel pessoa)
        {
            try
            {

                DadosPessoa.pessoas.RemoveAll(x => x.Id == pessoa.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}