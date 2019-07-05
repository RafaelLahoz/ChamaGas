using AppChamaGas.Helper;
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
	public partial class ProdutoCadView : ContentPage
	{
        ProdutoAzureService produto_Service = new ProdutoAzureService();
        private Produto produtoBC { get { return (Produto)this.BindingContext; } }
		public ProdutoCadView ()
		{
			InitializeComponent ();

            this.BindingContext = new Produto();

            var usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            produtoBC.FornecedorId = usuarioLogado.Id;

            btnFoto.Text = Font_Index.camera;
		}

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            bool sucesso = string.IsNullOrEmpty(produtoBC.Id)
                ? await produto_Service.IncluirRegistro(produtoBC)
                : await produto_Service.AlterarRegistro(produtoBC);

            if (sucesso)
            { 
                await this.DisplayAlert("Sucesso", "Registro salvo com sucesso", "OK");
                await Navigation.PopAsync();
            }

        }

        private async void BtnFoto_Clicked(object sender, EventArgs e)
        {
            var md = await Photo.TiraFoto("produto.jpg");

            if (md == null)
                return;

            this.produtoBC.FotoByte = md.fotoArray;
        }
    }
}