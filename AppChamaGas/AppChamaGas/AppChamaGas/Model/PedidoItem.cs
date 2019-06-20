using System;
using System.Collections.Generic;
using System.Text;
using AppChamaGas.Interface;

namespace AppChamaGas.Model
{
    public class PedidoItem : IAzureTabela
    {
        public string Id { get; set; }
        public string PedidoId { get; set; }
        public string ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }

        //Calculado internamente na classe
        // ninguem consegue trocar o valor na tela.
        public double ValorTotal { get; private set; }

        //Metodo Construtor
        public PedidoItem(string pedidoId, string produtoId, int quantidade=1, double preco=0)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Preco = preco;
            if (quantidade > 0 && preco > 0)
            {
                ValorTotal = Quantidade * Preco;
            }
        }

    }
}
