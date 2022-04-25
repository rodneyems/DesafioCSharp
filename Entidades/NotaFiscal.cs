using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCSharp.Entidades
{
    internal class NotaFiscal
    {
        public string emissor { get; }
        public string cnpj { get; }
        public string nome { get; }
        public double valor { get; }
        public DateTime data { get; }

        public NotaFiscal(string empresaEmissora, string cnpjCliente, string nomeCliente, double valorDaNotaFiscal, DateTime dataDeEmissao)
        {
            if (Utils.ValidaCnpj(cnpjCliente) && Utils.ValidaNome(nomeCliente))
            {
                emissor = empresaEmissora;
                cnpj = cnpjCliente;
                nome = nomeCliente;
                valor = valorDaNotaFiscal;
                data = dataDeEmissao;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
