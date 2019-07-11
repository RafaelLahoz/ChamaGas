using System;
using System.Collections.Generic;
using System.Text;
using AppChamaGas.Interface;
using AppChamaGas.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AppChamaGas.Model
{
    public class PedidoItem : ViewModelBase, IAzureTabela
    {
        public string Id { get; set; }
        public string PedidoId { get; set; }
        public string ProdutoId { get; set; }
       
        public double Preco { get; set; }
        [JsonIgnore]
        public Command MaisCommand { get; set; }
        [JsonIgnore]
        public Command MenosCommand { get; set; }
        [JsonIgnore]
        public string DescricaoProduto { get; set; }

        //Calculado internamente na classe
        // ninguem consegue trocar o valor na tela.
        private double valorTotal;
        public double ValorTotal
        {
            get { return valorTotal; }
            private set { SetProperty(ref valorTotal, value); }
        }
        private double quantidade;
        public double Quantidade
        {
            get { return quantidade; }
            set { SetProperty(ref quantidade, value); }
        }

        //Metodo Construtor
        public PedidoItem(string pedidoId, string produtoId, string IdDoItem, int quantidade=1, double preco=0)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Id = IdDoItem;
            Quantidade = quantidade;
            Preco = preco;
            if (quantidade > 0 && preco > 0)
            {
                ValorTotal = Quantidade * Preco;
            }

            MaisCommand = new Command(Mais);
            MenosCommand = new Command(Menos);

        }
        private void Menos()
        {
            Quantidade -= 1;
            ValorTotal = Quantidade * Preco;
        }
        private void Mais()
        {
            Quantidade += 1;
            ValorTotal = Quantidade * Preco;
        }

    }
}
