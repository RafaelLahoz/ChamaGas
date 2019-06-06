using AppChamaGas.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Model
{
    public class Pessoa : IAzureTabela
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }


        public string Foto { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Deleted { get; set; }

        //ignora campo quando converte para texto no formato Json
        [JsonIgnore]
        public double Distancia { get; set; }
    }
}
