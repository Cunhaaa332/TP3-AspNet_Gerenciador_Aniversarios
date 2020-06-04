using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP3_ASPNet.Models {
    public class PessoaModel {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime birth { get; set; }

        public PessoaModel() {
        }

        public PessoaModel(String nome, String sobrenome, DateTime data) {
            Nome = nome;
            SobreNome = sobrenome;
            birth = data;
        }

        public override string ToString() {
            return " Nome Completo: " + Nome + SobreNome +
                   "\n Data do Aniversario: " + birth.Day + "/" + birth.Month + "/" + birth.Year;
        }
    }
}
