using AppChamaGas.Model;
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
	public partial class LoginView : ContentPage
	{
        Usuario usuario;

		public LoginView ()
		{
			InitializeComponent ();
            usuario = new Usuario();
            this.BindingContext = usuario;
		}

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Informações", $"Email: { usuario.Email} - Senha: {usuario.Password}", "Fechar");
        }

        private void BtnPular_Clicked(object sender, EventArgs e)
        {

        }
    }
}