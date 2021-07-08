using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using WebAPI.Models;

namespace ClientAPI_CadastroDeClientes
{
    public partial class Form1 : Form
    {
       public string URI = "http://localhost:53223/api/Clientes";
        private string cpf;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    

        //Métodos
        //Retorna todos os Clientes
        private async void GetAllClientes()
        {

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ClienteJsonString = await response.Content.ReadAsStringAsync();
                        dataGridView.DataSource = JsonConvert.DeserializeObject<Cliente[]>(ClienteJsonString).ToList();
                        
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível obter os registros: " + response.StatusCode);
                    }
                }

            }
        }

        //Retorna Cliente por CPF
        private async void GetClienteByCPF(string cpf)
        {
            using (var client = new HttpClient())
            {
                BindingSource bsDados = new BindingSource();
                URI = URI + "/?cpf=" + cpf;
               
                HttpResponseMessage response = await client.GetAsync(URI);
                if (response.IsSuccessStatusCode)
                {
                    var ClienteJsonString = await response.Content.ReadAsStringAsync();
                    bsDados.DataSource = JsonConvert.DeserializeObject<Cliente>(ClienteJsonString);
                    dataGridView.DataSource = bsDados;
                }
                else
                {
                    MessageBox.Show("Falha ao retornar o cliente desejado: " + response.StatusCode);
                }

            }
        }
        //Remove registro de cliente
        private void RemoveCliente(string cpf)
        {
            
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                 var responseMessage = client.DeleteAsync($"{URI}/?cpf={cpf}").Result;
                
                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cliente excluído com sucesso");
                    GetAllClientes();
                }
                else
                {
                    MessageBox.Show("Falha ao excluir o cliente : " + responseMessage.StatusCode);
                }
            }

        }

        //Adicionar Novo Cliente
        private async void AddCliente()
        {

            using (var client = new HttpClient())
            {
               var uri= client.BaseAddress = new Uri("http://localhost:53223/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var cliente = new Cliente()
                {
                    Nome = txtNome.Text,
                    CPF = txtCPF.Text,
                    Telefone = txtTelefone.Text,
                    Email = txtEmail.Text,
                    Enderecos = { new Endereco
                    {
                        TipoEndereco=txtTipo.Text,
                        Logradouro=txtLogradouro.Text,
                        Bairro=txtBairro.Text,
                        Cidade=txtCidade.Text,
                        Complemento=txtComp.Text,
                        Numero=Convert.ToInt32(txtNum.Text),
                        Estado=txtEstado.Text,

                    }
                   }
                };
                var novoCliente = await client.PostAsJsonAsync("clientes",cliente);

                using (var response = await client.GetAsync($"{uri}/clientes"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ClienteJsonString = await response.Content.ReadAsStringAsync();
                        dataGridView.DataSource = JsonConvert.DeserializeObject<Cliente[]>(ClienteJsonString).ToList();

                    }
                    else
                    {
                        MessageBox.Show("Não foi possível obter os registros: " + response.StatusCode);
                    }
                }

            }

        }

        //Alterar Cadastro
        private async void UpdateCliente(string cpf)
        {
            txtNome.Focus();

            Cliente cliente = new Cliente();
            Endereco enderecos = new Endereco();

            cliente.Nome = txtNome.Text;
            cliente.CPF = txtCPF.Text;
            cliente.Telefone = txtTelefone.Text;
            cliente.Email = txtEmail.Text;

            enderecos.TipoEndereco = txtTipo.Text;
            enderecos.Logradouro = txtLogradouro.Text;
            enderecos.Bairro = txtBairro.Text;
            enderecos.Cidade = txtCidade.Text;
            enderecos.Complemento = txtComp.Text;
            enderecos.Numero = Convert.ToInt32(txtNum.Text);
            enderecos.Estado = txtEstado.Text;

        
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(URI + "/?cpf=" + cpf,cliente);
                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cliente atualizado");
                }
                else
                {
                    MessageBox.Show("Falha ao atualizar o cliente : " + responseMessage.StatusCode);
                }
            }

            GetAllClientes();
        }

        //Inputs
        private void InputBox_BuscaPorCPF()
        {
            string Prompt = "Informe o CPF do cliente: ";
            string Titulo = "Buscar Por CPF";
            string resultado = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Titulo, "", 600, 350);
            if (resultado != "")
            {
                cpf = resultado;
            }
            else
            {
                cpf = string.Empty;
            }
        }


        //Eventos de Clique
        private void btnTodos_Click(object sender, EventArgs e)
        {
            GetAllClientes();
        }

        private void menu_BuscarPorCPF_Click(object sender, EventArgs e)
        {
            InputBox_BuscaPorCPF();
            if (cpf != string.Empty)
            {
                GetClienteByCPF(cpf);
            }
        }

        private void menu_RemoverCadastro_Click(object sender, EventArgs e)
        {
            InputBox_BuscaPorCPF();
            if (cpf != string.Empty)
            {
                RemoveCliente(cpf);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            AddCliente();
        }

        private void menu_AlterarCadastro_Click(object sender, EventArgs e)
        {
            InputBox_BuscaPorCPF();
            if (cpf != string.Empty)
            {
                UpdateCliente(cpf);
            }
        }
    }
}

