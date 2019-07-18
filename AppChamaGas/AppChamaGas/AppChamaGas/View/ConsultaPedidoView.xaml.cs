using AppChamaGas.Model;
using AppChamaGas.Services.Azure;
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
    public partial class ConsultaPedidoView : ContentPage
    {
        PedidoAzureService pedido_service = new PedidoAzureService();
        Pedido pedido;
        public ConsultaPedidoView(Pedido pedido)
        {
            this.pedido = pedido;
            InitializeComponent();

            PopulaCampos(pedido);

        }

        private void PopulaCampos(Pedido pedido)
        {
            lblID.Text = pedido.Id;
            lblDataEmissao.Text = pedido.DataEmissao.ToString("dd/MM/yyyy");
            lblNomeForn.Text = pedido.NomeFornecedor;
            lblNomeCliente.Text = pedido.NomeCliente;
            lvItens.ItemsSource = pedido.listaItens;
            lblTotal.Text = pedido.TotalPedido.ToString("C2");
            lblSolicitado.Text = pedido.DataAgenda.ToString("dd/MM/yyyy HH:mm");
            if (pedido.DataEntrega == null || pedido.DataEntrega == DateTime.MinValue)
                ControlaVisibilidadeEntrega(string.Empty);
            else
                ControlaVisibilidadeEntrega(pedido.DataEntrega.ToString("dd/MM/yyy HH:mm"));
        }


        private void ControlaVisibilidadeEntrega( string dataEntrega)
            {
                bool visibilidadeEntregaVazia = string.IsNullOrEmpty(dataEntrega);
                lblEntregue.Text = dataEntrega;
                lblLBLEntregue.IsVisible = !visibilidadeEntregaVazia;
                lblEntregue.IsVisible = !visibilidadeEntregaVazia;

                btnEntregue.IsVisible = visibilidadeEntregaVazia;
            }

        private async void BtnEntregue_Clicked(object sender, EventArgs e)
        {
            this.pedido.DataEntrega = DateTime.Now;

            bool sucesso = await pedido_service.AlterarRegistro(this.pedido);

            if (sucesso)
            {
                await Navigation.PopAsync();
                await this.DisplayAlert("Sucesso", "Data de Emtrega salva", "Ok");
            }
            else
                await this.DisplayAlert("Sucesso", "Falha ao alterar pedido", "Ok");


        }
    }
}