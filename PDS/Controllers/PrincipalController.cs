using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Dtos;
using PDS.Models;
using System.Text.RegularExpressions;

namespace PDS.Controllers
{
    [Route("Cpf/[Controller]")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        private const string Arquivo = "Cliente.txt";

        public PrincipalController()
        {
            if (!System.IO.File.Exists(Arquivo))
            {
                System.IO.File.Create(Arquivo).Dispose();
            }
        }

        private List<Cliente> listaClientes()
        {
            var clientes = new List<Cliente>();

            if (!System.IO.File.Exists(Arquivo))
            {
                return clientes;
            }

            var linhas = System.IO.File.ReadAllLines(Arquivo);
            foreach (var linha in linhas)
            {
                var dados = linha.Split('|');
                if (dados.Length == 10)
                {
                    clientes.Add(new Cliente
                    {
                        Nome = dados[0],
                        DataNascimento = DateTime.Parse(dados[1]),
                        Sexo = dados[2],
                        RG = dados[3],
                        CPF = dados[4],
                        Endereco = dados[5],
                        Cidade = dados[6],
                        Estado = dados[7],
                        Telefone = dados[8],
                        Email = dados[9]
                    });
                }
            }
            return clientes;
        }

        private bool Validacao(string cpf)
        {
            cpf = Regex.Replace(cpf, @"\D", "");
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
            {
                return false;
            }

            var digito1 = 0;
            var digito2 = 0;
            var peso = 10;

            for (var i = 0; i < 9; i++)
            {
                digito1 += int.Parse(cpf[i].ToString()) * peso--;
            }

            digito1 = (digito1 % 11) < 2 ? 0 : 11 - (digito1 % 11);

            peso = 11;
            for (var i = 0; i < 10; i++)
            {
                digito2 += int.Parse(cpf[i].ToString()) * peso--;
            }

            digito2 = (digito2 % 11) < 2 ? 0 : 11 - (digito2 % 11);

            return cpf[9] == digito1.ToString()[0] && cpf[10] == digito2.ToString()[0];
        }

        private void GravarArquivo(List<Cliente> clientes)
        {
            var linhas = clientes.Select(c => $"{c.Nome}|{c.DataNascimento:dd-MM-yyyy}|{c.Sexo}|{c.RG}|{c.CPF}|{c.Endereco}|{c.Cidade}|{c.Estado}|{c.Telefone}|{c.Email}");
            System.IO.File.WriteAllLines(Arquivo, linhas);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clientes = listaClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = listaClientes().FirstOrDefault(item => item.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClienteDTO item)
        {
            if (item == null || !Validacao(item.CPF))
            {
                return BadRequest("Incorreto");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientes = listaClientes();
            var cliente = new Cliente()
            {
                Id = clientes.Count + 1,
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

            clientes.Add(cliente);
            GravarArquivo(clientes);

            return StatusCode(StatusCodes.Status201Created, cliente);
        }

        [HttpPut("{cpf}")]
        public IActionResult Put(string cpf, [FromBody] ClienteDTO item)
        {
            if (!Validacao(item.CPF))
            {
                return BadRequest("Cpf incorreto");
            }

            var clientes = listaClientes();
            var cliente = clientes.FirstOrDefault(c => c.CPF == cpf);

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

            GravarArquivo(clientes);

            return Ok(cliente);
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            if (!Validacao(cpf))
            {
                return BadRequest("CPF inválido.");
            }

            var clientes = listaClientes();
            var cliente = clientes.FirstOrDefault(c => c.CPF == cpf);

            if (cliente == null)
            {
                return NotFound();
            }

            clientes.Remove(cliente);
            GravarArquivo(clientes);

            return Ok(cliente);
        }
    }
}
