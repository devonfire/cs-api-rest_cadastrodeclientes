using System.Collections.Generic;

namespace WebAPI.Models
{
    public class Cliente
    {
        public Cliente()
        {
            this.Enderecos = new HashSet<Endereco>();
        }

        public int IdCliente { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }

        
    }
}