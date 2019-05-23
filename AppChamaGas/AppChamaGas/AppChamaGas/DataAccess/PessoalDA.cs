using AppChamaGas.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppChamaGas.DataAccess
{
    public class PessoaDA
    {
        public PessoaMD Create(SQLiteConnection conn, ProdutoMD md)
        {
            conn.Insert(md);
            return conn.Table<PessoaMD>().LastOrDefault();
        }

        //public ObservableCollection<PessoaMD> List(SQLiteConnection conn, bool ativo)
        //{

        //    if (conn == null)
        //        conn = Conexao.Get();

        //    List<PessoaMD> listaDeProduto = conn
        //        .Table<PessoaMD>()
        //        .Where(p => p.Ativo == ativo)
        //        .ToList();

        //    return new ObservableCollection<ProduPessoaMDtoMD>(listaDeProduto);
        //}

        public PessoaMD Update(SQLiteConnection conn, PessoaMD md_obj)
        {
            conn.Update(md_obj);
            return conn.Table<PessoaMD>().Where(p => p.IdPessoa == md_obj.IdPessoa).FirstOrDefault();
        }

        public PessoaMD Delete(SQLiteConnection conn, PessoaMD md_obj)
        {
            conn.Delete(md_obj);
            return md_obj;
        }
    }
}
