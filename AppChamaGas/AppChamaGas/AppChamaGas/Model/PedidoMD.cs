using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Model
{
    public class PedidoMD
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int IdPessoa { get; set; }

        [NotNull]
        public int IdFornecedor { get; set; }

        [NotNull]
        public DateTime DataEmissao { get; set; }

        [NotNull]
        public DateTime DataEntrega { get; set; }

        [NotNull]
        public DateTime DataAtualizacao { get; set; }

        [NotNull]
        public DateTime DataAgenda { get; set; }

        [NotNull]
        public int IdNuvem { get; set; }

    }
}
