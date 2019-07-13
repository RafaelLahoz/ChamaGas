using AppChamaGas.Model;
using AppChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarrinhoView : ContentPage
    {
        PedidoAzureService Pedido_Service = new PedidoAzureService();
        PedidoItemAzureService PedidoItens_Service = new PedidoItemAzureService();

        public static Pedido pedido = new Pedido("", "");

        public static ObservableCollection<PedidoItem> Itens = new ObservableCollection<PedidoItem>();

        Pessoa usuario;
        //Pessoa usuarioLogado;
        //bool eh_Distribuidor;
        public CarrinhoView()
        {
            InitializeComponent();
            //eh_Distribuidor = usuarioLogado.Tipo == "Distribuidor";
            usuario = Barrel.Current.Get<Pessoa>("pessoa");
            CarrinhoView.pedido.ClienteId = usuario.Id;

            //lblTitCar.Text = eh_Distribuidor ? "Meus Pedidos" : "Meu Carrinho";
            this.BindingContext = CarrinhoView.pedido;

            lvCarrinho.ItemsSource = CarrinhoView.Itens;
        }

        private async void BtnSalvaCarrinho_Clicked(object sender, EventArgs e)
        {
            bool confirma = await DisplayAlert("Confirmação", "Deseja realmente ENVIAR o pedido para nossa Central ?", "Sim", "Não");

            if (!confirma)
                return;

            if (!await Pedido_Service.IncluirRegistro(CarrinhoView.pedido))
                return;

            var inserido = (await Pedido_Service.ListarAsync()).LastOrDefault();

            foreach (var item in CarrinhoView.Itens)
            {
                item.PedidoId = inserido.Id;
                item.Id = string.Empty;
                if (!await PedidoItens_Service.IncluirRegistro(item))
                {
                    await this.DisplayAlert("Falha", "Falha ao transmitir o pedido", "Fechar");
                    return;
                }

            }


            ZeraPedido();


        }
        private async void ZeraPedido()
        {

            await Navigation.PopAsync();
            CarrinhoView.pedido.Id = string.Empty;
            CarrinhoView.pedido.FornecedorId = string.Empty;
            CarrinhoView.pedido.NomeFornecedor = string.Empty;
            CarrinhoView.pedido.TotalItens = 0;
            CarrinhoView.pedido.TotalPedido = 0;

            CarrinhoView.Itens.Clear();

            await this.DisplayAlert("SUCESSO", "Pedido Transmitido", "Ok");
        }
    }
}