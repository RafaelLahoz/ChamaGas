using AppChamaGas.Model;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.DataAccess
{
    public class Conexao
    {
        public static CreationCollisionOption CreationCollision { get; private set; }

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
            conn.CreateTable<Produto>();
            conn.Commit();
            conn.Close();
        }
        
        public static SQLiteConnection GetConn()
        {
            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("teste.db", CreationCollisionOption.OpenIfExists);

            return new SQLiteConnection(arquivo.Path);
        }
        public static void Initialize()
        {
            var conn = GetConn();
            conn.BeginTransaction();
            conn.CreateTable<FotoMD>();
            conn.Commit();
            conn.Close();
        }
        
    }
}
