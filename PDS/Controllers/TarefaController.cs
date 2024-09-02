using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Models;
using PDS.Dtos;

namespace PDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {

        List<Cliente> listaTarefas = new List<Cliente>();

        public TarefaController()
        {
            var tarefa1 = new Cliente()
            {
                Id = 1,
                Descricao = "Estudo de API part 1"
            };


            var tarefa2 = new Cliente()
            {
                Id = 2,
                Descricao = "Estudo de API part 2"
            };
            var tarefa3 = new Cliente()
            {
                Id = 3,
                Descricao = "Estudo de API part 3"
            };

            listaTarefas.Add(tarefa1);
            listaTarefas.Add(tarefa2);
            listaTarefas.Add(tarefa3);
        }

    }
}
