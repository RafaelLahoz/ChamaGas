using AppChamaGas.Model;
using AppChamaGas.Services;
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
	public partial class PessoaView : ContentPage
	{
        Base_Service client_cep = new Base_Service(Base_Service.URL_VIACEP);
        //Base_Service client_api = new Base_Service(Base_Service.URL_MINHAAPI);

        public PessoaView ()
		{
			InitializeComponent ();
            
		}

        private async void EtCEP_Unfocused(object sender, FocusEventArgs e)
        {
            var cep_ret = await client_cep.Get<Cep>(etCEP.Text);

            this.etCEP.Text = cep_ret.CEP;
            this.etLogradouro.Text = cep_ret.Logradouro;
            this.etComplemento.Text = cep_ret.Complemento;
            this.etBairro.Text = cep_ret.Bairro;
            this.etLocalidade.Text = cep_ret.Localidade;
            this.etUF.Text = cep_ret.Uf;

            var obj_reqres_user = new ReqRes_Service("users");
            var user_ret = await obj_reqres_user.Get<RetornoTeste>("2");

            await DisplayAlert("Dados", $"Id: { user_ret.data.FirstOrDefault().id} - Email: { user_ret.data.FirstOrDefault().email}", "Fechar");


        }


    }
}