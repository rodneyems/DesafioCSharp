using DesafioCSharp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DesafioCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Empresa> empresas = new List<Empresa>();
            bool encerra = false;
            int abaMenu = 0;
            Empresa empresa = empresas.FirstOrDefault(empresa => empresa.cnpj == "");
            while (!encerra)
            {
                if (abaMenu == 0)
                {
                    string nome;
                    string cnpj = Menu.Inicio();
                    empresa = empresas.FirstOrDefault(empresa => empresa.cnpj == cnpj);
                    if (empresa != null)
                    {
                        abaMenu++;
                    }
                    else
                    {
                        nome = Menu.EmpresaNaoCadastrada();
                        Empresa empresaCadastro = new Empresa(cnpj, nome);
                        empresas.Add(empresaCadastro);
                        empresa = empresas.Find(empresa => empresa.cnpj == cnpj);
                        Console.WriteLine(empresa);
                        abaMenu++;
                    }
                }
                if (abaMenu == 1)
                {
                    int opcao = Menu.EmpresaCadastrada();
                    switch (opcao)
                    {
                        case 1:
                            {
                                double valor;
                                string cnpjCliente;
                                string nomeCliente;
                                bool emiteNota;
                                int retorno = Menu.CadastraNota(false, out valor, out cnpjCliente, out nomeCliente, out emiteNota);
                                switch (retorno)
                                {
                                    case 0:
                                        empresa.cadastrarNotaFiscal(empresa.cnpj, cnpjCliente, nomeCliente, valor, DateTime.Now);
                                        abaMenu = 3;
                                        if (emiteNota)
                                        {
                                            empresa.CalculaImposto(valor);
                                            Console.ReadLine();
                                            abaMenu = 1;
                                            continue;
                                        }
                                        else
                                        {
                                            abaMenu = 1;
                                            continue;
                                        }
                                    case 88:
                                        abaMenu = 1;
                                        continue;
                                    case 99:
                                        abaMenu = 0;
                                        continue;
                                }
                                break;
                            }
                        case 2:
                            {
                                double valor;
                                string cnpjCliente;
                                string nomeCliente;
                                bool emiteNota;
                                int retorno = Menu.CadastraNota(true, out valor, out cnpjCliente, out nomeCliente, out emiteNota);
                                switch (retorno)
                                {
                                    case 0:
                                        DateTime data = Menu.CadastrarNotasAnteriores();
                                        empresa.cadastrarNotaFiscal(empresa.cnpj, cnpjCliente, nomeCliente, valor, data);
                                        abaMenu = 3;
                                        if (emiteNota)
                                        {
                                            empresa.CalculaImposto(valor);
                                            Console.ReadLine();
                                            abaMenu = 1;
                                            continue;
                                        }
                                        else
                                        {
                                            abaMenu = 1;
                                            continue;
                                        }
                                    case 88:
                                        abaMenu = 1;
                                        continue;
                                    case 99:
                                        abaMenu = 0;
                                        continue;
                                }
                                break;
                            }
                        case 3:
                            string dadosParaConsulta;
                            int tipoDeconsulta = Menu.ConsultaNotasAnteriores(out dadosParaConsulta);
                            switch (tipoDeconsulta)
                            {
                                case 1:
                                    {

                                        List<NotaFiscal> notas = empresa.consultaNotasAnteriores("cliente", dadosParaConsulta);
                                        if (notas.Count == 0)
                                        {
                                            Console.WriteLine("Cliente não encontrado");
                                            Thread.Sleep(2000);
                                            abaMenu = 0;
                                            break;
                                        }
                                        Console.WriteLine("**********************************************");
                                        Console.WriteLine("**** Mes de Emissão      |       Valor    ****");
                                        Console.WriteLine("**********************************************");
                                        notas.ForEach(notaFiscal =>
                                        {
                                            Console.WriteLine($"**** {notaFiscal.data.ToString("mm/yyyy")}      |       {notaFiscal.valor.ToString("C")}    ****");
                                            Console.WriteLine("----------------------------------------------");
                                        });
                                        Console.WriteLine("**********************************************");
                                        abaMenu = 0;
                                        Console.ReadLine();
                                        break;
                                    }
                                case 2:
                                    {
                                        List<NotaFiscal> notas = empresa.consultaNotasAnteriores("mes", dadosParaConsulta);
                                        if (notas.Count == 0)
                                        {
                                            Console.WriteLine("Cliente não encontrado");
                                            Thread.Sleep(2000);
                                            abaMenu = 1;
                                            break;
                                        }
                                        Console.WriteLine("**********************************************");
                                        Console.WriteLine("** Cliente |   Mês de Emissão  |   Valor    **");
                                        Console.WriteLine("**********************************************");
                                        notas.ForEach(notaFiscal =>
                                        {
                                            Console.WriteLine($"** {notaFiscal.nome}  |   {notaFiscal.data.ToString("MM/yyyy")} |       {notaFiscal.valor}    ****");
                                            Console.WriteLine("----------------------------------------------");
                                        });
                                        Console.WriteLine("**********************************************");
                                        abaMenu = 1;
                                        Console.ReadLine();
                                        break;
                                    }
                                case 88:
                                    abaMenu = 2;
                                    break;
                                case 99:
                                    abaMenu = 0;
                                    break;
                            }
                            abaMenu = 1;
                            break;
                        case 88:
                            abaMenu--;
                            continue;
                        case 99:
                            abaMenu = 1;
                            continue;
                    }
                }
            }

        }
    }
}
