using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3_ASPNet.Models;

namespace TP3_ASPNet.Dados {
    class DadosPessoa {
        public static List<PessoaModel> pessoas = new List<PessoaModel>();
        public static List<PessoaModel> pessoasEncontradas = new List<PessoaModel>();

        public static void BuscarPessoas(string nome) {
            foreach (PessoaModel pessoa in pessoas) {
                if (pessoa.Nome.Contains(nome) || pessoa.SobreNome.Contains(nome)) {
                    pessoasEncontradas.Add(pessoa);
                }
            }

        }

        public static IEnumerable<PessoaModel> Listar() {
            return pessoas;
        }
    }
}
