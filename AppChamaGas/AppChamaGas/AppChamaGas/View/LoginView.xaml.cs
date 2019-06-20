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
	public partial class LoginView : ContentPage
	{
        PessoaAzureService pessoaAzureService;
        UsuarioModel usuarioModel;

		public LoginView ()
		{
			InitializeComponent ();
            pessoaAzureService = new PessoaAzureService();
            usuarioModel = new UsuarioModel();
            this.BindingContext = usuarioModel;
		}

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            //Validar dados
            if (ValidarDados())
            {
                var pessoa = await pessoaAzureService.AutenticaUsuarioAsync(usuarioModel.Email, usuarioModel.Senha);
                //Verificação da Autenticação do Usuario
                if (pessoa != null)
                {
                    usuarioModel.Id = pessoa.Id;
                    usuarioModel.Email = pessoa.Email;
                    usuarioModel.Senha = pessoa.Senha;
                    usuarioModel.Permissao = pessoa.Tipo;
                    usuarioModel.Autenticado = true;
                    //Salvar no banco de dados
                    //Define a nova MainPage (pagina principal)
                    App.Current.MainPage = new MasterView();
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Email ou Senha inválidos", "Fechar");
            }
            
        }

        private void Registrar_Clicked(object sender, EventArgs e)
        {
            //MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new PessoaView());
            this.Navigation.PushModalAsync(new PessoaView());
        }

        private bool ValidarDados()
        {
           
            if (string.IsNullOrWhiteSpace(vEmail.Text) &&
                string.IsNullOrWhiteSpace(vSenha.Text))
            {
                vErro.IsVisible = true;
                vErro.Text = "Atenção, informe os seus dados corretamente";
                return false;
            }
            else if (vSenha.Text.Length > 8)
            {
                vErro.Text = "Atenção, senha inválida (menor que 8 caracteres)";
                return false;
            }
            else
            {
               
                return true;
            }
        }
    }
}