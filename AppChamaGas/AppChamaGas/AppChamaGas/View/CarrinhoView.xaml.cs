using AppChamaGas.Model;
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
        public static Pedido pedido = new Pedido("", "");

        public static ObservableCollection<PedidoItem> Itens = new ObservableCollection<PedidoItem>();

        //Pessoa usuarioLogado;
        //bool eh_Distribuidor;
        public CarrinhoView ()
		{
			InitializeComponent ();
            //eh_Distribuidor = usuarioLogado.Tipo == "Distribuidor";

            //lblTitCar.Text = eh_Distribuidor ? "Meus Pedidos" : "Meu Carrinho";
            this.BindingContext = CarrinhoView.pedido;

            lvCarrinho.ItemsSource = CarrinhoView.Itens;
        }



        
        
    }
}