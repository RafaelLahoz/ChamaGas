using AppChamaGas.Components;
using AppChamaGas.Helper;
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
    public partial class FlexPage : ContentPage
    {
        public FlexPage()
        {
            InitializeComponent();

            foreach (Mesa mesa in GerarMesas())
            {
                var stc = new StackLayout() { Margin = new Thickness(10) };
                var corSelecao = mesa.Status == "Disponíveis" ? Color.DarkGreen : mesa.Status == "Ocupado" ? Color.DarkRed : Color.Yellow;
                var ic = new Icon() { Text = Font_Index.table, FontSize = 40, TextColor = mesa.Cor };
                var lbl = new Label() { Text = mesa.Nome };

                stc.Children.Add(ic);
                stc.Children.Add(lbl);
                flPrincipal.Children.Add(stc);
            }
        }

        public IEnumerable<Mesa> GerarMesas()
        {
            return new List<Mesa>
        {
            new Mesa {Nome = "Mesa 1",  Status = "Disponíveis"},
            new Mesa {Nome = "Mesa 2",  Status = "Disponíveis"},
            new Mesa {Nome = "Mesa 3",  Status = "Disponíveis" },
            new Mesa {Nome = "Mesa 4",  Status = "Disponíveis"},
            new Mesa {Nome = "Mesa 5",  Status = "Pendentes"},
            new Mesa {Nome = "Mesa 6",  Status = "Pendentes"},
            new Mesa {Nome = "Mesa 7",  Status = "Pendentes"},
            new Mesa {Nome = "Mesa 8",  Status = "Pendentes"},
            new Mesa {Nome = "Mesa 9",  Status = "Ocupado"},
            new Mesa {Nome = "Mesa 10", Status = "Ocupado"},
            new Mesa {Nome = "Mesa 11", Status = "Ocupado"},
            new Mesa {Nome = "Mesa 12", Status = "Ocupado"},
        };
        }
    }

    public class Mesa
    {
        public string Nome { get; set; }
        public Color Cor { get
            {
                var corSelecao = this.Status == "Disponíveis"
                    ? Color.DarkGreen 
                    : this.Status == "Ocupado"
                    ? Color.DarkRed
                    : Color.Yellow;

                return corSelecao;
            } }
        public string Status { get; set; }
    }
}