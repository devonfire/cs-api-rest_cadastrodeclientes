using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Interfaces;

namespace WebAPI.Models.Repositories
{
    public class ClienteRepository : IClienteRepository 
    {
        private List<Cliente> clientes = new List<Cliente>();
        
        private int _nextId = 1;

        public ClienteRepository()
        {
            Add(new Cliente
            {
                CPF = "9088721797",
                Nome = "Iago Mendes",
                Email = "iago@hotmail.com",
                Telefone = "11978387299",
                Enderecos = {
                    new Endereco{
                        IdEndereco =0,
                        IdCliente =1,
                        TipoEndereco ="Comercial",
                        Logradouro ="Rua Moreia",
                        Numero =5,Complemento="b",
                        Bairro ="Jardim Santo André",
                        Cidade ="São Paulo",
                        Estado ="São Paulo"
                    },
                    new Endereco{
                        IdEndereco =1,
                        IdCliente =1,
                        TipoEndereco ="Cobrança",
                        Logradouro ="Rua Solimões",
                        Numero =555,Complemento="ba",
                        Bairro ="Jardim Primavera",
                        Cidade ="São Paulo",
                        Estado ="São Paulo"
                    }
                }
            });
        }
        //Post
        public Cliente Add(Cliente cliente)
        {
       

            if (cliente == null)
            {
                throw new ArgumentNullException("cliente");
            }
            cliente.IdCliente = _nextId++;
            clientes.Add(cliente);

            return cliente;

        }

        public Cliente Get(string cpf)
        {
            return clientes.Find(c => c.CPF == cpf);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return clientes;
        }

        public void Remove(string cpf)
        {
            clientes.RemoveAt(clientes.IndexOf(clientes.First(cli=>cli.CPF.Equals(cpf))));
        }

        public bool Update(string cpf,Cliente cliente)
        {
            
            if (cliente == null)
            {
                throw new ArgumentNullException("cliente");
            }
            var search = clientes.Find(c => c.CPF == cpf);
    
            clientes.RemoveAt(clientes.IndexOf(clientes.First(cli => cli.CPF.Equals(cpf))));
            clientes.Add(cliente);
            return true;
        }
    }
}