using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace ProjetoBanco
{
    internal class Banco
    {
        public string Nome { get; private set; }
        public short Numero { get; private set; }
        //public Endereco Endereco { get; private set; }
        public List<Conta> Contas { get; private set; }

        public Banco()
        {
            Contas = new List<Conta>();
        }


        public Conta AbrirConta(Cliente cliente)
        {
            if (cliente.MaiorDeIdade() == false)
                throw new Exception("Somente maiores de 18 anos podem abrir uma conta.");

            var numeroConta = Contas.Count + 1;
            var conta = new Conta(Enums.TipoConta.Corrente, 1, numeroConta, this);

            Contas.Add(conta);

            return conta;

        }

        public void FecharConta(Conta conta)
        {
            Contas.Remove(conta);
        }

        public Conta ObterConta(int agencia, int numeroConta)
        {
            var conta = Contas.FirstOrDefault(c => c.Agencia == agencia && c.Numero == numeroConta);

            if (conta == null)
                throw new Exception("Conta não encontrada.");

            return conta;
        }
    }
}