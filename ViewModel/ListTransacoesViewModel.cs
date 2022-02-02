using System;
using ContaCorrente.ApiExtrato.Enums;
using ContaCorrente.ApiExtrato.Models;

namespace ContaCorrente.ApiExtrato.ViewModels
{
    public class ListTransacoesViewModel
    {
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public string TipoTransacao { get; set; }
    }
}
