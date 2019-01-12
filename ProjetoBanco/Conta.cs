using System;
using System.Collections.Generic;
using System.Text;

using ProjetoBanco.Enums;

namespace ProjetoBanco
{
    internal class Conta
    {
        public TipoConta TipoConta { get; private set; }
        public int Agencia { get; private set; }
        public int Numero { get; private set; }
        public decimal Saldo { get; private set; }
        public Banco Banco { get; set; }
        public List<Transacao> Transacoes { get; private set; }

        public Conta(TipoConta tipoConta, int agencia, int numero, Banco banco)
        {
            TipoConta = tipoConta;
            Agencia = agencia;
            Numero = numero;
            Banco = banco;
            Transacoes = new List<Transacao>();
        }

        public void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new SystemException("Valor inválido.");
            if (valor > Saldo)
                throw new SystemException("Saldo insuficiente para realizar essa operação.");

            Debitar("Retirar", valor);

            Console.WriteLine("Saque realizado com sucesso.");
        }

        public void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new SystemException("Valor inválido.");

            Creditar("Deposito", valor);

            Console.WriteLine("Deposito realizado com sucesso.");
        }

        public void Transferir(int agencia, int numeroConta, decimal valor)
        {
            if (valor <= 0)
                throw new SystemException("Valor inválido.");
            if (valor > Saldo)
                throw new SystemException("Saldo insuficiente para realizar essa operação.");

            var contaDestino = Banco.ObterConta(agencia, numeroConta);

            contaDestino.Creditar("Transferencia", valor);

            Debitar("Transerencia", valor);

            Console.WriteLine("Transferencia realizada com sucesso.");
        }

        public void TirarExtrato()
        {
            if (Transacoes.Count > 0)
            {
                foreach (var transacao in Transacoes)
                {
                    var cor = transacao.TipoTransacao == TipoTransacao.Debito ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.ForegroundColor = cor;

                    var descricao = transacao.Descricao.PadRight(20, '-') + transacao.Valor.ToString("C");
                    Console.WriteLine(descricao);
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(string.Empty);

                var saldoDescricao = "Saldo".PadRight(20, '-') + Saldo.ToString("C");
                Console.WriteLine(saldoDescricao);
            }
        }

        private void Creditar(string descricao, decimal valor)
        {
            var transacao = new Transacao(TipoTransacao.Credito, "Creditado", valor);
            Transacoes.Add(transacao);
            Saldo = Saldo + valor;
        }

        private void Debitar(string descricao, decimal valor)
        {
            var transacao = new Transacao(TipoTransacao.Debito, "Debitado", valor);
            Transacoes.Add(transacao);
            Saldo = Saldo - valor;
        }
    }
}
