using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //RunAsync(txt).Wait();
            RunAsyncChuck(txt);
            RunAsyncJson(txt);
            RunAsyncJsonPost(txt);
        }

        static async void RunAsyncJson(RichTextBox txt)
        {
            //Esse método envia uma requisição GET para obter dados de uma API. (Json Placeholder)
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("GET");

                HttpResponseMessage response = await client.GetAsync("todos/1");
                if (response.IsSuccessStatusCode)
                {
                    ModeloJson model = await response.Content.ReadAsAsync<ModeloJson>();
                    Console.WriteLine($"\nGET\n\n{model.userId}\n{model.id}\n{model.title}\n{model.completed}\n");
                    txt.Text += $"{model.userId}\n{model.id}\n{model.title}\n{model.completed}\n\n";


                }
                else
                {
                    Console.WriteLine("Deu ruim menó kkkkkkkkkkkkk");
                    txt.Text = "\nDeu ruim menó kkkkkkkkkkkkk\n";
                }



            }
        }

        private async void RunAsyncChuck(RichTextBox txt)
        {
            //Esse método envia uma requisição GET para obter dados de uma API. (Chuck Norris)
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.chucknorris.io");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("GET");

                HttpResponseMessage response = await client.GetAsync("/jokes/random");
                if (response.IsSuccessStatusCode)
                {
                    ChuckNoris model = await response.Content.ReadAsAsync<ChuckNoris>();
                    Console.WriteLine($"\nGET\n\n{model.icon_url}\n{model.id}\n{model.url}\n{model.value}\n");
                    txt.Text += $"\nGET\n\n{model.icon_url}\n{model.id}\n{model.url}\n{model.value}\n\n";


                }
                else
                {
                    Console.WriteLine("Deu ruim menó kkkkkkkkkkkkk");
                    txt.Text = "\nDeu ruim menó kkkkkkkkkkkkk\n";
                }



            }






        }

        static async void RunAsyncJsonPost(RichTextBox txt)
        {
            //Esse método insere um item, enviando um JSON através da requisição POST.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("POST");

                ModeloJson model = new ModeloJson() { id=null, userId=100, title="titulo", completed=true};


                //O Put funciona da mesma forma, mas ele atualiza um item já existente através
                //de uma requisição PUT.

                //Requisições PUT te obrigam a colocar todos os dados no JSON, já o PATCH tem a mesma
                //função, porém permite inserir apenas o que você desejar atualizar, enviando através
                //de uma requisição PATCH para a API.
                HttpResponseMessage response = await client.PostAsJsonAsync("posts", model);
                if (response.IsSuccessStatusCode)
                {
                    Uri personUrl = response.Headers.Location;
                    Console.WriteLine($"\nPOST\n\n\n {personUrl}");
                    txt.Text += $"\nPOST\n\n{personUrl}\n\n";

                    response = await client.PutAsJsonAsync("posts", model);
                    //response = await client.PatchAsync("posts", model);

                    //Requisições DELETE apenas utilizam o URI (endereço) do qual você deseja excluir
                    //por exemplo, para excluir o post com id 2, você passa posts/2 na URI.
                    response = await client.DeleteAsync(personUrl);


                }
                else
                {
                    Console.WriteLine("Deu ruim menó kkkkkkkkkkkkk");
                    txt.Text = "\nDeu ruim menó kkkkkkkkkkkkk\n";
                }



            }
        }




    }
}
