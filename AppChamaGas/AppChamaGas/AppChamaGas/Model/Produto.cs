using System;
using System.Collections.Generic;
using System.Text;
using AppChamaGas.Interface;

namespace AppChamaGas.Model
{
    public class Produto : IAzureTabela
    {
        public string Id { get; set; }
        public string FornecedorId { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string Foto { get; set; }
        public string UnidMedida { get; set; }


    }
}
