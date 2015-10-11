using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
// var , trim gibi güzel methodların adresi
using System.Linq;
using System.Text;
// regex kullanım için c# kütüphanesi
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace regexap1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var: kendisi arka tarafta string olarak algılıyor
            var fileName = "c:\\data.xlsx";

            //connectionstring veriye nasıl ulaşacağımın bilgisini veriyor db'ye ulaşmak için web servis gibi bağlantı gereken her yerde kullanılır
            //Microsoft accessin veri tabanı sağlayıcı Oledb excel bağlantısını kuruyor. XLS için oleddb 4.0'ı kullanıyoruz. XLSX için oled db 12.0 kullandım
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"", fileName);

            // hemde içine yazdığım commendi çalıştırmama yardımcı oluyor.
            // excel dosyamdaki worksheet "uyeler" için işlem yapacağım adapter nesnesini kullanarak
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Üyeler$]", connectionString);

            // sütunların tutulduğu boş bir yapı. Data roll tek satır. data set tüm satırları okur.
            var ds = new DataSet();

            adapter.Fill(ds, "uyeTable");

            //data setin içindeki üye table olan tabloyu getir dedim.
            var data = ds.Tables["uyeTable"].AsEnumerable();

            //e-mail regex yazdım yapıştırdım. Ama hepsini tek tek inceleyip kodu geliştireceğim
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            // data.where c#'ın özel kodu istediğim kuralı yazmamı sağlıyor.
            //bana gelen excel dosyasındaki sütunu e-mail olanın tipi string olacak
            // String.isnullempty boşsa true doluysa false döndürür
            // !String.Isnull... bana e-maili dolu olan tüm satırlar geldi
            // select dediğim için de sadece e-mail satırını aldım
            var query = data.Where(x => !String.IsNullOrEmpty(x.Field<string>("e-mail"))).Select(x => x.Field<string>("e-mail").Trim());

            List<string> emailList = new List<string>();

            // neden foreach yerine where kalıbını kullanmadım ? nedeni e-maillerin kendi içinde de boşluk bırakılmış olması
            // trim kullandığım halde hata veriyor olması 
            // trim hata verdiği için regexmatch patladı. Muhtemelen regeximi ve trim kontrolümü geliştirmem gerekecek

            // for kullansam i =0 filan diyecektim query[i] yazıcaktım.
            // foreachle queryi geziyoruz.
            foreach (string email in query)
            {
                if (!String.IsNullOrEmpty(email))
                {

                    // regex'lerle e-maili başarıyla karşılaştırdıysa e-maileekle dedim
                    //tek satırda gerçekten e-mail gibi olanları alıyor ,'leri birden fazla e-mailleri ve boşluk olanları algılamıyor
                    if (regex.Match(email).Success)
                    {
                        emailList.Add(email);
                        Console.WriteLine(email);
                    }
                }
            }
            Console.ReadLine();
            }
        }
    }

