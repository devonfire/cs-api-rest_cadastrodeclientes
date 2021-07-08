using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Models.Interfaces;
using WebAPI.Models.Repositories;

namespace WebAPI.Controllers
{

    public class ClientesController : ApiController
    {
        
        static readonly IClienteRepository repository = new ClienteRepository();
        public ClientesController(){
        
        }

   

        // GET: api/Clientes
        
        public IEnumerable<Cliente> GetAllClientes()
        {
            return repository.GetAll();
        }

        // GET: api/Clientes/?cpf=seu_Cpf
        
        public Cliente GetCliente(string cpf)
        {
            Cliente cliente = repository.Get(cpf);

            if (cliente == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return cliente;
        }

        // POST: api/Clientes
        public HttpResponseMessage Post(Cliente cliente)
        {
            
            repository.Add(cliente);

            var response = Request.CreateResponse<Cliente>(HttpStatusCode.Created,cliente);
           
            return response;
            
        }

        // PUT: api/Clientes/?cpf=seu_Cpf
        public void Put(string cpf, Cliente cliente)
        {
            cliente.CPF = cpf;
            if (!repository.Update(cpf,cliente))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Update(cpf,cliente);
        }

        // DELETE: api/Clientes/?cpf=seu_Cpf
        public void Delete(string cpf)
        {
            Cliente cliente = repository.Get(cpf);
            if (cliente == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(cpf);
        }
    }
}
