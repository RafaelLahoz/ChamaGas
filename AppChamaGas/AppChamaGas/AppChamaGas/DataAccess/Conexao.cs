using AppChamaGas.Model;
using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.DataAccess
{
    public class Conexao
    {
        public static SQLiteConnection Get()
        {
            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("BancoGas.db"
                , PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);
            return new SQLiteConnection(arquivo.Path);

            // return new SQLiteConnection(@"C:\temp\BancoGas.db");
        }
        public void CriaEstruturaBanco()
        {
            var conn = Get();
            conn.BeginTransaction();
            conn.CreateTable<ProdutoMD>();
            conn.Commit();
            conn.Close();
        }
    }
}
