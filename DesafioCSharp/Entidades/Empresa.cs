using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCSharp.Entidades
{
    internal class Empresa
    {
        public string cnpj { get; }
        public string nome { get; }
        private List<NotaFiscal> notasFiscais = new List<NotaFiscal>();

        public Empresa(string numeroCnpj, string nomeDaEmpresa)
        {
            if (Utils.ValidaCnpj(numeroCnpj) && Utils.ValidaNome(nomeDaEmpresa))
            {
                cnpj = numeroCnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                nome = nomeDaEmpresa;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public void cadastrarNotaFiscal(string cnpjEmpresaEmissora, string cnpjEmpresaCliente, string nomeCliente, double valor, DateTime dataDeEmissao)
        {
            try
            {
                NotaFiscal notaFiscal = new NotaFiscal(cnpjEmpresaEmissora, cnpjEmpresaCliente, nomeCliente, valor, dataDeEmissao);
                notasFiscais.Add(notaFiscal);
            }
            catch
            {
                throw new ArgumentException();
            }
        }
        public List<NotaFiscal> consultaNotasAnteriores(string tipoDeConsulta, string dadoParaConsulta)
        {
            try
            {
                if (tipoDeConsulta == "cliente")
                {
                    return notasFiscais.FindAll(notaFiscal => notaFiscal.cnpj == dadoParaConsulta);
                }
                else if (tipoDeConsulta == "mes" && Utils.ValidaData(dadoParaConsulta))
                {
                    return notasFiscais.FindAll(notaFiscal => notaFiscal.data.Month == DateTime.Parse(dadoParaConsulta).Month);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch
            {
                throw new ArgumentException();
            }
        }
        public void CalculaImposto(double valorNotaFiscal)
        {
            List<NotaFiscal> notasFiscaisDozeMeses = notasFiscais.FindAll(notasFiscais => notasFiscais.data > DateTime.Now.AddMonths(-12));
            double rtb12;
            double aliq;
            double vb;
            double vad;
            double vd;
            double aliqf;
            double val;

            rtb12 = notasFiscaisDozeMeses.Sum(notaFiscal => notaFiscal.valor);
            aliq = rtb12 <= 180000.00 ? 0.06 : 0.112;
            vb = rtb12 * aliq;
            vad = rtb12 <= 180000.00 ? 0.00 : 9360.00;
            vd = vb - vad;
            aliqf = vd / rtb12;
            val = valorNotaFiscal * aliqf;

            double irpj = val * 0.04;
            double csll = val * 0.035;
            double cofins = rtb12 <= 180000.00 ? val * 0.1282 : val * 0.1405;
            double pis = rtb12 <= 180000.00 ? val * 0.0278 : val * 0.0305;
            double inss = val * 0.4340;
            double iss = val * 0.3350;

            Console.WriteLine($"IRPJ: {irpj.ToString("C")}");
            Console.WriteLine($"CSLL: {csll.ToString("C")}");
            Console.WriteLine($"COFINS: {cofins.ToString("C")}");
            Console.WriteLine($"PIS: {pis.ToString("C")}");
            Console.WriteLine($"INSS: {inss.ToString("C")}");
            Console.WriteLine($"ISS: {iss.ToString("C")}");
            Console.WriteLine($"VALOR TOTAL: {val.ToString("C")}");
        }

        public override string ToString()
        {
            return ($"OBJETO do {cnpj}").ToString();
        }
    }
}
