using AppChamaGas.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Services
{
    public class ReqRes_Service : Base_Service
    {
        public const string base_url = "https://reqres.in/api/";
        string api_name;

        public ReqRes_Service(string api_name) : base(base_url)
        {
            this.api_name = api_name;
        }

        public override async Task<T> Get<T>(string input)
        {
            var client = base.GetClient();
            var retorno = await client.GetAsync($"{api_name}?page={input}");
            var retornoTexto = await retorno.Content.ReadAsStringAsync();
            T obj_Convertido = JsonConvert.DeserializeObject<T>(retornoTexto);
            return obj_Convertido;
        }

        public async Task<T> Post<T>(T md)
        {
            return await Post<T, T>(md);

        }
        public async Task<To> Post<Ti, To>(Ti md)
        {
            var client = base.GetClient();
            var corpo = GetBody(md);
            var retorno = await client.PostAsync(this.api_name, corpo);

            if (retorno.IsSuccessStatusCode)
            {
                var retornoTexto = await retorno.Content.ReadAsStringAsync();
                To md_retorno = JsonConvert.DeserializeObject<To>(retornoTexto);
                return md_retorno;
            }
            else
                throw new Exception($"Ocorreu um erro: {retorno.StatusCode.ToString()}");
        }

        private StringContent GetBody<T>(T md)
        {
            var texto = JsonConvert.SerializeObject(md);
            return new StringContent(texto, Encoding.UTF8, "application/json");
        }

        public async Task<T> Put<T>(T md)
        {
            var client = base.GetClient();
            var corpo = GetBody(md);
            var atualiza = await client.PutAsync(this.api_name, corpo);
            var atualizaTexto = await atualiza.Content.ReadAsStringAsync();
            T md_atualiza = JsonConvert.DeserializeObject<T>(atualizaTexto);
            return md_atualiza;
        }
    }
}
