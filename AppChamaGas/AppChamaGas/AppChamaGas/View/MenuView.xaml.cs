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
	public partial class MenuView : ContentPage
	{
        List<Pagina> paginas;
        private object pagina;

        public MenuView ()
		{
			InitializeComponent ();
            IniciaLista();

        }

        public void IniciaLista()
        {
            paginas = new List<Pagina>();
            paginas.Add(new Pagina
            {
                Titulo = "Pessoa",
                Icone = "",
                PaginaView = typeof(PessoaView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Fornecedor",
                Icone = "",
                PaginaView = typeof(LoginView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Produto",
                Icone = "",
                PaginaView = typeof(PessoaView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Lista de Pedidos",
                Icone = "",
                PaginaView = typeof(PessoaView)
            });
            

            lvMenu.ItemsSource = paginas;
        }

        private void LvMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Seleciona a pagina
            var pagina = e.SelectedItem as Pagina;
            //Verifica o  retorno da selecao
            if (pagina != null)
            {

                //Inicia a navegacao
                MasterView.NavegacaoMasterDetail.IsPresented = false;
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PopToRootAsync();
                //Criação da pagina view
                Page paginaview = Activator.CreateInstance(pagina.PaginaView) as Page;
                //Navega para a pagina view
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(paginaview);
                //Desativa o item selecionado
                lvMenu.SelectedItem = null;
               
            }
        }
    }
}