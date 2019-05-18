using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Model
{
    public class ProdutoMD
    {
        [PrimaryKey, AutoIncrement]
        public int IdProduto { get; set; }

        [NotNull]
        public string Descricao { get; set; }

        [NotNull]
        public decimal Preco { get; set; }

        [NotNull]
        public int IdFornecedor { get; set; }

        [NotNull]
        public int IdNuvem { get; set; }

        [NotNull]
        public DateTime DataAtualizacao { get; set; }

        [NotNull]
        public string ImgProd { get; set; }

    }
}
