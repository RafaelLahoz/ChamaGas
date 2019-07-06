using AppChamaGas.Helper;
using AppChamaGas.Interface;
using AppChamaGas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace AppChamaGas.Model
{
    public class Pessoa : ViewModelBase, IAzureTabela
    {
        public string Id { get; set; }
        private string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { SetProperty(ref tipo, value); }
        }
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

        //byte[] convertido para string
        private string foto;
        public string Foto
        {
            get { return foto; }
            set { SetProperty(ref foto, value); }
        }
        //byte [] array
        private byte[] fotoByte;
        [JsonIgnore]
        public byte[] FotoByte
        {
            get
            {
                if (fotoByte == null && Foto != null)
                    FotoByte = Convert.FromBase64String(Foto);
                return fotoByte;
            }
            set
            {
                SetProperty(ref fotoByte, value);
                if (value != null)
                {
                    FotoSource = value.ToImageSource();
                    Foto = Convert.ToBase64String(value);
                }
            }
        }
        //imageSource
        private ImageSource fotoSource;
        [JsonIgnore]
        public ImageSource FotoSource
        {
            get { return fotoSource; }
            set { SetProperty(ref fotoSource, value); }
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Deleted { get; set; }

        //ignora campo quando converte para texto no formato Json
        [JsonIgnore]
        public double Distancia { get; set; }

        private bool botaoVisivel;
        [JsonIgnore]
        public bool BotaoVisivel
        {
            get { return botaoVisivel; }
            set { SetProperty(ref botaoVisivel, value); }
        }

        private bool imagemVisivel;
        [JsonIgnore]
        public bool ImagemVisivel
        {
            get { return imagemVisivel; }
            set { SetProperty(ref imagemVisivel, value); }
        }

        
        [JsonIgnore]
        public string TextoBotaoFoto { get; set; }
        public Pessoa()
        {
           
            BotaoVisivel = true;
            ImagemVisivel = false;

            TextoBotaoFoto = Font_Index.camera;
        }

    }
}
