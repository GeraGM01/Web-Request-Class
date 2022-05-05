using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Net;

namespace WebRequestClass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //se realiza la peticion a la pagina web
            var request = (HttpWebRequest)WebRequest.Create("https://spotifycharts.com/viral/mx/daily/latest");

            //Aqui simulamos que se esta realizando la peticion mediante un navegador web
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            
            //Obtenemos la respuesta
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            // se obtiene el html de la pagina
            Stream dataStream = response.GetResponseStream();

            // creamos un lector
            StreamReader reader = new StreamReader(dataStream);

            // se lee el contenido html
            string responseFromServer = reader.ReadToEnd();

            int inicioBusq = 0;
            while (true)
            {
                int i = responseFromServer.IndexOf("class=\"chart-table-position\"", inicioBusq);
                if (i == -1) break;
                int inicio = responseFromServer.IndexOf(">", i) + 1;
                int final = responseFromServer.IndexOf("<", inicio);
                string canciones = responseFromServer.Substring(inicio, final - inicio);
                int i2 = responseFromServer.IndexOf("class=\"chart-table-track\"", inicioBusq);
                int inicio2 = responseFromServer.IndexOf("strong", i2) + 7;
                int final2 = responseFromServer.IndexOf("<", inicio2);
                string canciones2 = responseFromServer.Substring(inicio2, final2 - inicio2);

                int i3 = responseFromServer.IndexOf("class=\"chart-table-track\"", inicioBusq);
                int inicio3 = responseFromServer.IndexOf("span", i3) + 7;
                int final3 = responseFromServer.IndexOf("<", inicio3);
                string canciones3 = responseFromServer.Substring(inicio3, final3 - inicio3);

                inicioBusq = final;
                dataGridView1.Rows.Add(canciones,canciones2,canciones3);
                //Console.WriteLine(canciones + " " + canciones2 + " " + canciones3);
            }
            //se cierran los archivos
            reader.Close();
            dataStream.Close();
            response.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
