using System.Collections.Generic;

namespace WebAPI.Models.Interfaces
{
    public interface IClienteRepository
    {
        //Retorna todos os Clientes e seus endereços
        IEnumerable<Cliente> GetAll();
        //Filtra por CPF
        Cliente Get(string CPF);
        //Adiciona Cliente
        Cliente Add(Cliente cliente);
        //Remove Cliente
        void Remove(string CPF);
        //Edita Cliente
        bool Update(string CPF,Cliente cliente);
    }
}
