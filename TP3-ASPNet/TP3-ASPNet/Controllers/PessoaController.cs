using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP3_ASPNet.Models;
using TP3_ASPNet.Repositorio;

namespace TP3_ASPNet.Controllers
{
    public class PessoaController : Controller
    {
        private RepositorioPessoa RepositorioPessoa { get; set; }

        public PessoaController(RepositorioPessoa repositorioPessoa) {
            this.RepositorioPessoa = repositorioPessoa;
        }
        // GET: Pessoa
        [Route("Pessoa/")]
        public ActionResult Index()
        {
            var pessoas = this.RepositorioPessoa.Listar();
            return View(pessoas);
        }

        [Route("Pessoa/AniversariantesDia")]
        public ActionResult AniversariantesDia() {
            DateTime hj = DateTime.Today;
            var pessoas = this.RepositorioPessoa.Listar();
            var aniversariantes = new List<PessoaModel>();

            foreach (var pessoa in pessoas) {
                if (pessoa.birth.Day == hj.Day && pessoa.birth.Month == hj.Month) {
                    aniversariantes.Add(pessoa);
                }
            }
            return View(aniversariantes);
        }

        // GET: Buscar
        [Route("Pessoa/Buscar")]
        public ActionResult Buscar() {
            var pessoa = RepositorioPessoa.Listar().Where(pessoa => pessoa.Nome.Contains(HttpContext.Request.Form["Nome"],StringComparison.InvariantCultureIgnoreCase) 
            || pessoa.SobreNome.Contains(HttpContext.Request.Form["Nome"], StringComparison.InvariantCultureIgnoreCase));
            return View(pessoa);
        }

        // GET: Pessoa/Details/5
        [Route("Pessoa/DetalhesPessoa/{id}")]
        public ActionResult DetalhesPessoa(int id)
        {
            var pessoa = this.RepositorioPessoa.GetById(id);
            return View(pessoa);

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
                if (ModelState.IsValid == false)
                    return View();
                RepositorioPessoa.Salvar(pessoa);
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
            
            var pessoa = this.RepositorioPessoa.GetById(id);
            
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        [Route("Pessoa/Editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, PessoaModel pessoa){
            try
            {
                if (ModelState.IsValid == false)
                    return View();
                var pessoaEdit = this.RepositorioPessoa.GetById(id);

                pessoaEdit.Nome = pessoa.Nome;
                pessoaEdit.SobreNome = pessoa.SobreNome;
                pessoaEdit.birth = pessoa.birth;
                
                RepositorioPessoa.Editar(pessoaEdit);

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
            var pessoa = this.RepositorioPessoa.GetById(id);
                    return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [Route("Pessoa/Deletar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(PessoaModel pessoa)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View();

                RepositorioPessoa.Deletar(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}