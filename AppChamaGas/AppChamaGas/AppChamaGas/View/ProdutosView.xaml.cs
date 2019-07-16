using AppChamaGas.Helper;
using AppChamaGas.Model;
using AppChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdutosView : ContentPage
    {
        PessoaAzureService pessoa_Service = new PessoaAzureService();
        ProdutoAzureService produto_Service = new ProdutoAzureService();
        Pessoa usuarioLogado;
        bool eh_Distribuidor;
        public ProdutosView()
        {
            InitializeComponent();
            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            vCarro.Text = Font_Index.dolly;
        }

        private void PopuleBindings()
        {
            CarrinhoView.pedido.DelegateAtualizadorLista += ColecaoAlterada;
            this.BindingContext = CarrinhoView.pedido;
            CarrinhoView.Itens.CollectionChanged += ColecaoAlterada;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(CarrinhoView.pedido.FornecedorId))
                PopuleBindings();

            eh_Distribuidor = usuarioLogado.Tipo == "Distribuidor";

            if (eh_Distribuidor)
                AdicionarBotaoNovoProduto();

            lblTitulo.Text = eh_Distribuidor ? "Meus Produtos" : "Lista de Produtos";
            //btnAdd.IsVisible = eh_Distribuidor ? true : false;
            StkCarro.IsVisible = !eh_Distribuidor;
            IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarAsync();
            IEnumerable<Produto> produtos = await produto_Service.ListarAsync();
            if (eh_Distribuidor)
            {
                pessoas = pessoas.Where(p => p.Id == usuarioLogado.Id).ToList();
                produtos = produtos.Where(p => p.FornecedorId == usuarioLogado.Id).ToList();
                //StkCarro.IsVisible = false;
            }

            else
            {
                pessoas = pessoas.Where(p => p.Tipo == "Distribuidor").ToList();
                //StkCarro.IsVisible = true;
            }

            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var mPosition = await Geolocation.GetLocationAsync(request);

            foreach (var produto in produtos)
            {
                var forn = pessoas.Where(p => p.Id == produto.FornecedorId).FirstOrDefault();
                if (forn == null)
                    continue;

                produto.FornecedorNome = forn.RazaoSocial;
                produto.Latitude = forn.Latitude;
                produto.Longitude = forn.Longitude;

                Location locForn = new Location(forn.Latitude, forn.Longitude);
                forn.Distancia = mPosition.CalculateDistance(locForn, DistanceUnits.Kilometers);
                produto.Distancia = $"({forn.Distancia.ToString("N4")} kms";

                produto.FotoSource = produto.FotoByte.ToImageSource();

            }


            lvProdutos.ItemsSource = produtos;


            //lvProdutos.ItemsSource = new List<Produto>
            //{
            //    new Produto
            //    {
            //        Descricao = "Gas do Bom",
            //        Distancia = "5,0 km",
            //        FornecedorNome = "Rui Gas",
            //        Preco = 70.00,
            //        //Foto = "RuiGas.png",
            //    },
            //    new Produto
            //    {
            //        Descricao = "Gas do Quase Bom",
            //        Distancia = "7,0 km",
            //        FornecedorNome = "Joao do Gas",
            //        Preco = 80.00,
            //        //Foto = "Ultragaz.png",
            //    }
            //};
        }

        private void AdicionarBotaoNovoProduto()
        {
            if (this.ToolbarItems.Count == 0)
                this.ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Add",
                    Command = new Command(AbrirTelaCadastroProduto),
                });
        }

        private void AbrirTelaCadastroProduto(object obj)
        {
            Navigation.PushAsync(new ProdutoCadView());
        }

        private void GesComprar_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarrinhoView());
        }

        private void LvProdutos_ItemTapped(object sender, ItemTappedEventArgs args)
        {
            var prd = (Produto)args.Item;

            int proximoId = CarrinhoView.Itens.Count() + 1;

            CarrinhoView.Itens.Add(new PedidoItem("", prd.Id, proximoId.ToString(), 1, prd.Preco) { DescricaoProduto = prd.Descricao, PedidoPai = CarrinhoView.pedido });
            CarrinhoView.pedido.FornecedorId = prd.FornecedorId;

            
        }

        private void ColecaoAlterada(object sender, EventArgs e)
        {
            CarrinhoView.pedido.TotalPedido = CarrinhoView.Itens.Sum(p => p.ValorTotal);
            CarrinhoView.pedido.TotalItens = CarrinhoView.Itens.Count();

        }

        private void EtBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.DisplayAlert("Msg", "Texto Alterado", "Ok");

        }

        private void EtBusca_Unfocused(object sender, FocusEventArgs e)
        {
            this.DisplayAlert("Msg", "Texto Desfocado", "Ok");
        }

        private void LvProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lvProdutos.SelectedItem = null;
        }
    }
}