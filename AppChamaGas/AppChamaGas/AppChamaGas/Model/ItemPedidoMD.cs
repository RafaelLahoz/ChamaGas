using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Model
{
    public class ItemPedidoMD
    {
        [PrimaryKey, AutoIncrement]
        public int IdItemPedido{ get; set; }

        [NotNull]
        public int IdProduto { get; set; }

        [NotNull]
        public int IdPedido { get; set; }

        [NotNull]
        public int Preco { get; set; }

        [NotNull]
        public int Qtde { get; set; }

        [NotNull]
        public DateTime DataAtualizacao { get; set; }

        [NotNull]
        public string ImgProd { get; set; }

    }
}
