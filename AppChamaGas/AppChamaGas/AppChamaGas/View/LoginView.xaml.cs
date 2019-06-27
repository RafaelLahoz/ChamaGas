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
	public partial class LoginView : ContentPage
	{
        PessoaAzureService pessoaAzureService;
        UsuarioModel usuarioModel { get; set; }

		public LoginView ()
		{
			InitializeComponent ();
            pessoaAzureService = new PessoaAzureService();
            usuarioModel = new UsuarioModel();
            this.BindingContext = usuarioModel;
		}

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            CarregarTela(true);
            //Validar dados
            if (ValidarDados())
            {
                var pessoa = await pessoaAzureService.AutenticaUsuarioAsync(usuarioModel.Email, usuarioModel.Senha);
                //Verificação da Autenticação do Usuario
                if (pessoa != null)
                {
                    //usuarioModel.Id = pessoa.Id;
                    //usuarioModel.Email = pessoa.Email;
                    //usuarioModel.Senha = pessoa.Senha;
                    //usuarioModel.Permissao = pessoa.Tipo;
                    //usuarioModel.Autenticado = true;
                    //Cache de dados para a pessoa
                    //Salvar no banco de dados
                    Barrel.Current.Add(key: "pessoa", data: pessoa, expireIn: TimeSpan.FromMinutes(3));


                    //Define a nova MainPage (pagina principal)
                    App.Current.MainPage = new MasterView();
                    CarregarTela(false);
                }
                else
                {
                    CarregarTela(false);
                    await DisplayAlert("Atenção", "Conta não encontrado", "Fechar");
                  
                }
            }
            else
            {
                CarregarTela(false);
                await DisplayAlert("Atenção", "Email ou Senha inválidos", "Fechar");
                
            }
            CarregarTela(false);
            return;
            
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
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
            else if (vSenha.Text.Length < 8)
            {
                vErro.Text = "Atenção, senha inválida (menor que 8 caracteres)";
                return false;
            }
            vErro.Text = string.Empty;
            return true;
        }

        private void CarregarTela(bool resultado)
        {
            //Default
            aiCarregar.IsVisible = true;
            aiCarregar.IsRunning = true;
            vSenha.IsEnabled = false;
            vEmail.IsEnabled = false;
            btnEntrar.IsEnabled = false;
            btnRegistrar.IsEnabled = false;
            //Verificacao Resultado
            if (!resultado)
            {
                aiCarregar.IsVisible = false;
                aiCarregar.IsRunning = false;
                vSenha.IsEnabled = true;
                vEmail.IsEnabled = true;
                btnEntrar.IsEnabled = true;
                btnRegistrar.IsEnabled = true;
            }
        }
        private void VerificarLogin()
        {
            var usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            if (usuarioLogado != null)
            {
                App.Current.MainPage = new MasterView();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VerificarLogin();
        }
    }
}