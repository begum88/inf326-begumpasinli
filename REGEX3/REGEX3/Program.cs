using REGEX3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace regexap1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Verifier> verifiers = new List<Verifier>(){
                new Verifier(),
                new UpperCaseVerifier(),
                new NonUnicodeVerifier()
            };
            bool isCorrect = false;

            var fileName = "c:\\data.xlsx";

            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"", fileName);


            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Üyeler$]", connectionString);


            var ds = new DataSet();

            adapter.Fill(ds, "uyeTable");

            //data setin içindeki üye table olan tabloyu getir dedim.
            var data = ds.Tables["uyeTable"].AsEnumerable();

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            var query = data.Where(x => !String.IsNullOrEmpty(x.Field<string>("e-mail"))).Select(x => x.Field<string>("e-mail").Trim()).SelectMany(x => x.Split(',', ';', ' ').ToList());

            List<string> emailList = new List<string>();

            foreach (string email in query)
            {               
                foreach (var verifier in verifiers)
                {
                    isCorrect = verifier.CorrectEmail(email);

                    if (!isCorrect)
                    {
                        break;
                    }                    
                }
                if (isCorrect)
                {
                    string newEmail = email.ToLower();
                    emailList.Add(newEmail);                    
                }                
            }
            Console.WriteLine("------------------------------- \n Geçerli Mail Adresleri");
            foreach (string email in emailList)
            {
                Console.WriteLine(email);
            }
            
            Console.ReadLine();
        }
    }
}