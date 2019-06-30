using AppChamaGas.Helper;
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
    public partial class MenuView : ContentPage
    {
        List<Pagina> paginas;
        private object pagina;
        //Pessoa pessoaContext;

        public MenuView()
        {
            InitializeComponent();

            ExibirPessoa();
            IniciaLista();

        }

        public void IniciaLista()
        {
            paginas = new List<Pagina>();
            //paginas.Add(new Pagina
            //{
            //    Titulo = "Usuário",
            //    Icone = Font_Index.user,
            //    PaginaView = typeof(UsuarioView)
            //});
            //paginas.Add(new Pagina
            //{
            //    Titulo = "Login",
            //    Icone = Font_Index.user,
            //    PaginaView = typeof(LoginView)
            //});
            paginas.Add(new Pagina
            {
                Titulo = "Perfil",
                Icone = Font_Index.user_secret,
                PaginaView = typeof(PessoaView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Produtos",
                Icone = Font_Index.cart_arrow_down,
                PaginaView = typeof(ProdutosView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Lista de Pedidos",
                Icone = Font_Index.clipboard_list,
                PaginaView = typeof(PedidoView)
            });
            //paginas.Add(new Pagina
            //{
            //    Titulo = "Camera",
            //    Icone = Font_Index.camera,
            //    PaginaView = typeof(CameraView)
            //});
            


            lvMenu.ItemsSource = paginas;
        }

        private void LvMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            foreach (Pagina item in lvMenu.ItemsSource)
            {
                item.CorLetra = Color.Gray;
            }
            //Seleciona a pagina
            var pagina = e.Item as Pagina;
            //Verifica o  retorno da selecao
            if (pagina != null)
            {
                pagina.CorLetra = Color.Black;
                //Inicia a navegacao
                MasterView.NavegacaoMasterDetail.IsPresented = false;
                //MasterView.NavegacaoMasterDetail.Detail.Navigation.PopToRootAsync();

                //Criação da pagina view
                Page paginaview = null;
                if (pagina.PaginaView == typeof(PessoaView))
                    paginaview = new PessoaView(new Pessoa());
                else if (pagina.PaginaView == typeof(CameraView))
                    paginaview = new CameraView();
                else
                    paginaview = Activator.CreateInstance(pagina.PaginaView) as Page;



                //Navega para a pagina view
                //MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(paginaview);
                MasterView.NavegacaoMasterDetail.Detail = new NavigationPage(paginaview);
                //Desativa o item selecionado
                lvMenu.SelectedItem = null;

            }
        }

        private void ExibirPessoa()
        {
            var pessoa = Barrel.Current.Get<Pessoa>("pessoa");
            if (pessoa != null)
            {
                vNome.Text = pessoa.RazaoSocial;
                vEmail.Text = pessoa.Email;
                Uri uri = new Uri(@"https://picsum.photos/id/237/200/300");
                vFoto.Source = ImageSource.FromUri(uri);

            }
        }
    }
}