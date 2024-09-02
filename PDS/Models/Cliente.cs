﻿using System.ComponentModel.DataAnnotations;
using System;

namespace PDS.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Descricao { get; set; }

        public bool Feito { get; set; } = false;
    }


}
