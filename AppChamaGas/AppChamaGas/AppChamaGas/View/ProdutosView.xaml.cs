using AppChamaGas.Model;
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
	public partial class ProdutosView : ContentPage
	{
        Pessoa usuarioLogado;
        bool eh_Distribuidor;
		public ProdutosView ()
		{
			InitializeComponent ();

            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            eh_Distribuidor = usuarioLogado.Tipo == "Distribuidor";
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lblTitulo.Text = eh_Distribuidor ? "Meus Produtos" : "Lista de Produtos";

            lvProdutos.ItemsSource = new List<Produto>
            {
                new Produto
                {
                    Descricao = "Gas do Bom",
                    Distancia = "5km",
                    FornecedorNome = "Rui Gas",
                    Preco = 70.00,
                },
                new Produto
                {
                    Descricao = "Gas do Quase Bom",
                    Distancia = "7km",
                    FornecedorNome = "Joao do Gas",
                    Preco = 80.00,
                }
            };
        }


    }
}