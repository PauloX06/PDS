using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Dtos;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("/")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
            private static List<Cliente> listaClientes = new List<Cliente>();

            
            [HttpGet]
            public IActionResult Get()
            {
                return Ok(listaClientes);
            }

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var cliente = listaClientes.FirstOrDefault(item => item.Id == id);

                if (cliente == null)
                {
                    return NotFound();
                }

                return Ok(cliente);
            }

            [HttpPost]
            public IActionResult Post([FromBody] ClienteDTO item)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

               
                var contador = listaClientes.Count();
                var cliente = new Cliente()
                {
                    Id = contador + 1,
                    Nome = item.Nome,
                    DataNascimento = item.DataNascimento,
                    Sexo = item.Sexo,
                    RG = item.RG,
                    CPF = item.CPF,
                    Endereco = item.Endereco,
                    Cidade = item.Cidade,
                    Estado = item.Estado,
                    Telefone = item.Telefone,
                    Email = item.Email
                };

                listaClientes.Add(cliente);

                return StatusCode(StatusCodes.Status201Created, cliente);
            }

            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] ClienteDTO item)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cliente = listaClientes.FirstOrDefault(c => c.Id == id);

                if (cliente == null)
                {
                    return NotFound();
                }

                cliente.Nome = item.Nome;
                cliente.DataNascimento = item.DataNascimento;
                cliente.Sexo = item.Sexo;
                cliente.RG = item.RG;
                cliente.CPF = item.CPF;
                cliente.Endereco = item.Endereco;
                cliente.Cidade = item.Cidade;
                cliente.Estado = item.Estado;
                cliente.Telefone = item.Telefone;
                cliente.Email = item.Email;

                return Ok(cliente);
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var cliente = listaClientes.FirstOrDefault(c => c.Id == id);

                if (cliente == null)
                {
                    return NotFound();
                }

                listaClientes.Remove(cliente);

                return Ok(cliente);
            }
        
    }
}  

