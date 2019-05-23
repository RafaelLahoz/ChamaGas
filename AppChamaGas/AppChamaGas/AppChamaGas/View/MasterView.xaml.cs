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
    public partial class MasterView : MasterDetailPage
    {
        //Naegacao para estrutura Master Detail Page
        public static MasterDetailPage NavegacaoMasterDetail { get; set; }

        public MasterView()
        {
            InitializeComponent();
            //Configuração
            //Area Principal
            this.Detail = new NavigationPage (new HomeView()
            {
                Title = "ChamaGas",
                Icon = ""
            });
            //Menu
            this.Master = new MenuView()
            {
                Title = "Menu"
            };
            this.MasterBehavior = MasterBehavior.Popover;

            //Inicializa Navegação Master Detail Page
            NavegacaoMasterDetail = this;
        }
    }
}