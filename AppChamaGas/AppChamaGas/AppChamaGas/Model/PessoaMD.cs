using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Model
{
    public class PessoaMD
    {
        [PrimaryKey, AutoIncrement]
        public int IdPessoa { get; set; }

        [NotNull]
        public string TipoPessoa { get; set; }

        [NotNull]
        public string RazaoSocial { get; set; }

        [NotNull]
        public string Endereco { get; set; }

        [NotNull]
        public string Logradouro { get; set; }

        [NotNull]
        public string Numero { get; set; }

       [NotNull]
        public string Bairro { get; set; }

        [NotNull]
        public string Cidade { get; set; }

        [NotNull]
        public string UF { get; set; }

        [NotNull]
        public string CEP { get; set; }

        [NotNull]
        public string Email { get; set; }

        [NotNull]
        public string Senha { get; set; }

        [NotNull]
        public string Telefone { get; set; }

        [NotNull]
        public string ImgPessoa { get; set; }

        [NotNull]
        public int Latitude { get; set; }

        [NotNull]
        public int Longitude { get; set; }

        [NotNull]
        public bool Ativo { get; set; }

        //[NotNull]
        //public int IdNuvem { get; set; }

        //[NotNull]
        //public DateTime DataAtualizacao { get; set; }

    }
}
