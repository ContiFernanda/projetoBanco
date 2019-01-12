using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBanco
{
    internal class Endereco
    {
        public string Logradouro { get; private set; }
        public short Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public Cidade Cidade { get; private set; }

        public Endereco(string logradouro, short numero, string bairro, string cep, Cidade cidade)
        {
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
        }

    }
}
