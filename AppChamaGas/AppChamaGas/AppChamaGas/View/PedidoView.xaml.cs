using AppChamaGas.Model;
using AppChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PedidoView : ContentPage
	{
        PedidoAzureService pedido_Service = new PedidoAzureService();
        PedidoItemAzureService pedidoItens_Service = new PedidoItemAzureService();
        PessoaAzureService pessoa_Service = new PessoaAzureService();
        ProdutoAzureService produto_Service = new ProdutoAzureService();
        Pessoa usuarioLogado;
        IEnumerable<Produto> listaProdutos;
        public PedidoView ()
		{
			InitializeComponent ();
            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");

            this.Appearing += CarregaProdutos;
		}

        private async void CarregaProdutos(object sender, EventArgs e)
        {
            listaProdutos = await produto_Service.ListarAsync();
        }

        private void LvPedido_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ped = (Pedido)e.Item;

            //while (listaProdutos == null) continue;

            if (listaProdutos == null)
            {
                DisplayAlert("Alerta", "Estamos carregando tudo para você. Tente novamente", "Ok");
                return;
            }

            foreach (var item in ped.listaItens)
                item.DescricaoProduto = listaProdutos.Where(p => p.Id == item.ProdutoId).FirstOrDefault().Descricao;


            Navigation.PushAsync(new ConsultaPedidoView(ped));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool eh_distribuidor = usuarioLogado.Tipo == "Distribuidor";

            IEnumerable<Pedido> pedidos = await pedido_Service.ListarAsync();
            IEnumerable<PedidoItem> pedidosItens = await pedidoItens_Service.ListarAsync();
            IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarAsync();

            if (eh_distribuidor)
                pedidos = pedidos.Where(p => p.FornecedorId == usuarioLogado.Id);
            else
                pedidos = pedidos.Where(p => p.ClienteId == usuarioLogado.Id);

           

            foreach (var pedido in pedidos)
            {
                //Pessoa pessoa = eh_distribuidor 
                //    ? pessoas.Where(p => p.Id == pedido.ClienteId).FirstOrDefault()
                //    : pessoas.Where(p => p.Id == pedido.FornecedorId).FirstOrDefault();

                pedido.NomeFornecedor = pessoas.Where(p => p.Id == pedido.ClienteId).FirstOrDefault().RazaoSocial;
                pedido.NomeCliente = pessoas.Where(p => p.Id == pedido.FornecedorId).FirstOrDefault().RazaoSocial;

                var itensFiltrados = pedidosItens.Where(i => i.PedidoId == pedido.Id).ToList();
                pedido.listaItens = itensFiltrados;
                var total = itensFiltrados.Sum(i => i.ValorTotal);
                //pedido.ValorTotal = "R$ " + total.ToString("C2");
            }

            lvPedido.ItemsSource = pedidos;

            //lvPedido.ItemsSource = new List<Pedido>
            //{
            //    new Pedido("10", "5")
            //    {
            //        DataAgenda = DateTime.Now,
            //        DataEmissao = DateTime.Now,
            //        DataEntrega = DateTime.Now,
            //        Id = "1",
            //        NomeFornecedor = "João do Gás",
            //        ValorTotal = "R$ 67,45"

            //    },

            //    new Pedido("15", "8")
            //    {
            //        DataAgenda = DateTime.Now,
            //        DataEmissao = DateTime.Now,
            //        DataEntrega = DateTime.Now,
            //        Id = "2",
            //        NomeFornecedor = "Rui Gás",
            //        ValorTotal = "R$ 85,90"

            //    }
            //};
        }

        
    }
}