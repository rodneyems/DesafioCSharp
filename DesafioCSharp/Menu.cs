using DesafioCSharp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioCSharp
{
    internal class Menu
    {
        public static string Inicio()
        {
            bool converteu = false;
            string cnpj;
            do
            {
                Console.Clear();
                Console.WriteLine("**********************************************");
                Console.WriteLine("****     Digite o CNPJ da sua empresa     ****");
                Console.WriteLine("**********************************************");
                Console.Write("> ");
                cnpj = Console.ReadLine();
                converteu = Utils.ValidaCnpj(cnpj);
                if (!converteu)
                {
                    Console.WriteLine("Nome inválido");
                    Thread.Sleep(2000);
                }
            } while (!converteu);
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            return cnpj;
        }
        public static string EmpresaNaoCadastrada()
        {
            bool converteu = false;
            string nome;
            do
            {
                Console.Clear();
                Console.WriteLine("**********************************************");
                Console.WriteLine("****     Digite o nome da sua empresa     ****");
                Console.WriteLine("**********************************************");
                Console.Write("> ");
                nome = Console.ReadLine();
                converteu = Utils.ValidaNome(nome);
                if (!converteu)
                {
                    Console.WriteLine("Nome inválido");
                    Thread.Sleep(2000);
                }
            } while (!converteu);

            return nome;
        }
        public static int EmpresaCadastrada()
        {
            int opcao = 0;
            bool converteu = false;
            do
            {
                Console.Clear();
                Console.WriteLine("**********************************************");
                Console.WriteLine("**** Digite o número ref a opção desejada ****");
                Console.WriteLine("**********************************************");
                Console.WriteLine("> 1 - Emitir nova nota fiscal");
                Console.WriteLine("> 2 - Cadastrar notas anteriores");
                Console.WriteLine("> 3 - Consultar notas anteriores");
                Console.Write("> ");
                string opcaoDigitada = Console.ReadLine();
                if (opcaoDigitada == "1" || opcaoDigitada == "2" || opcaoDigitada == "3")
                {
                    converteu = int.TryParse(opcaoDigitada, out opcao);
                }
                else if (opcaoDigitada == "voltar")
                {
                    converteu = true;
                    opcao = 88;
                }
                else if (opcaoDigitada == "cancelar")
                {
                    converteu = true;
                    opcao = 99;
                }
                else if (opcaoDigitada == "encerrar")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Valor inválido");
                    Thread.Sleep(2000);
                }
            } while (!converteu);
            return opcao;
        }
        public static int CadastraNota(bool notaAnterior, out double valor, out string cnpjCliente, out string nomeCliente, out bool emiteNota)
        {
            valor = 0;
            cnpjCliente = null;
            nomeCliente = null;
            emiteNota = false;
            int indexTab = 0;
            bool fluxoCompleto = false;
            while (!fluxoCompleto)
            {

                if (indexTab == 0)
                {
                    bool converteu = false;
                    do
                    {

                        Console.Clear();
                        Console.WriteLine("**********************************************");
                        Console.WriteLine("****    Digite o valor da nota fiscal     ****");
                        Console.WriteLine("**********************************************");
                        Console.Write("> ");
                        string leitura = Console.ReadLine();
                        switch (leitura)
                        {
                            case "voltar":
                                valor = 0;
                                cnpjCliente = "";
                                nomeCliente = "";
                                emiteNota = false;
                                return 88;
                            case "cancelar":
                                valor = 0;
                                cnpjCliente = "";
                                nomeCliente = "";
                                emiteNota = false;
                                return 99;
                            case "encerrar":
                                Environment.Exit(0);
                                break;
                            default:
                                converteu = double.TryParse(leitura, out valor);
                                if (!converteu)
                                {
                                    Console.WriteLine("Valor inválido. Tente Novamente");
                                    Thread.Sleep(2000);
                                }
                                break;
                        }
                    } while (!converteu);
                    indexTab++;
                }

                if (indexTab == 1)
                {
                    bool converteu = false;
                    bool voltar = false;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("**********************************************");
                        Console.WriteLine("****       Digite o CNPJ do cliente       ****");
                        Console.WriteLine("**********************************************");
                        Console.Write("> ");
                        string leitura = Console.ReadLine();
                        switch (leitura)
                        {
                            case "voltar":
                                voltar = true;
                                indexTab = 0;
                                break;
                            case "cancelar":
                                valor = 0;
                                cnpjCliente = "";
                                nomeCliente = "";
                                emiteNota = false;
                                return 99;
                            case "encerrar":
                                Environment.Exit(0);
                                break;
                            default:
                                converteu = Utils.ValidaCnpj(leitura);
                                if (!converteu)
                                {
                                    Console.WriteLine("CNPJ inválido. Tente Novamente");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    cnpjCliente = leitura;
                                }
                                break;
                        }
                        if (voltar == true)
                        {
                            break;
                        }
                    } while (!converteu);
                    if (voltar == true)
                    {
                        continue;
                    }
                    indexTab++;
                }
                if (indexTab == 2)
                {
                    bool converteu = false;
                    bool voltar = false;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("**********************************************");
                        Console.WriteLine("****       Digite o Nome do cliente       ****");
                        Console.WriteLine("**********************************************");
                        Console.Write("> ");
                        string leitura = Console.ReadLine();
                        switch (leitura)
                        {
                            case "voltar":
                                voltar = true;
                                indexTab--;
                                break;
                            case "cancelar":
                                valor = 0;
                                cnpjCliente = "";
                                nomeCliente = "";
                                emiteNota = false;
                                return 99;
                            case "encerrar":
                                Environment.Exit(0);
                                break;
                            default:
                                converteu = Utils.ValidaNome(leitura);
                                if (!converteu)
                                {
                                    Console.WriteLine("Nome inválido. Tente Novamente");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    nomeCliente = leitura;
                                }
                                break;
                        }
                        if (voltar)
                        {
                            break;
                        }
                    } while (!converteu);
                    if (voltar)
                    {
                        continue;
                    }
                    if (notaAnterior)
                    {
                        fluxoCompleto = true;
                    }
                    indexTab++;
                }
                if (indexTab == 3)
                {
                    bool voltar = false;
                    bool converteu = false;
                    if (!notaAnterior)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("**********************************************");
                            Console.WriteLine("****     Deseja emitir a nota fiscal      ****");
                            Console.WriteLine("**********************************************");
                            Console.Write("> ");
                            string leitura = Console.ReadLine();
                            switch (leitura)
                            {
                                case "voltar":
                                    voltar = true;
                                    indexTab--;
                                    break;
                                case "cancelar":
                                    valor = 0;
                                    cnpjCliente = "";
                                    nomeCliente = "";
                                    emiteNota = false;
                                    return 99;
                                case "encerrar":
                                    Environment.Exit(0);
                                    break;
                                default:
                                    if (leitura.ToLower() == "s" || leitura.ToLower() == "sim")
                                    {
                                        fluxoCompleto = true;
                                        emiteNota = true;
                                        converteu = true;
                                        break;
                                    }
                                    else
                                    {
                                        fluxoCompleto = true;
                                        emiteNota = false;
                                        converteu = true;
                                        break;
                                    }
                            }
                            if (voltar)
                            {
                                break;
                            }
                        } while (!converteu);
                        if (voltar)
                        {
                            continue;
                        }
                    }
                }
            }
            return 0;
        }
        public static DateTime CadastrarNotasAnteriores()
        {
            bool converteu = false;
            DateTime dataConvertida;
            do
            {
                Console.Clear();
                Console.WriteLine("**********************************************");
                Console.WriteLine("****     Digite a data de emissão         ****");
                Console.WriteLine("**********************************************");
                Console.Write("> ");
                string data = Console.ReadLine();
                converteu = Utils.ValidaData(data);
                DateTime.TryParse(data, out dataConvertida);
                if (!converteu)
                {
                    Console.WriteLine("Data inválida");
                    Thread.Sleep(2000);
                }
            } while (!converteu);

            return dataConvertida;
        }
        public static int ConsultaNotasAnteriores(out string itemParaConsulta)
        {
            bool voltar = false;
            itemParaConsulta = "";
            int opcaoDeRetorno = 0;
            int indexMenu = 0;
            bool consulta = true;
            while (consulta)
            {
                if (indexMenu == 0)
                {
                    bool converteu = false;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("**********************************************");
                        Console.WriteLine("**** Digite o número ref a opção desejada ****");
                        Console.WriteLine("**********************************************");
                        Console.WriteLine("> 1 - Consultar por cliente");
                        Console.WriteLine("> 2 - Consultar por Mês");
                        Console.Write("> ");
                        string opcao = Console.ReadLine();
                        if (opcao == "voltar")
                        {
                            converteu = true;
                            return 88;
                        }
                        else if (opcao == "cancelar")
                        {
                            converteu = true;
                            return 99;
                        }
                        else if (opcao == "encerrar")
                        {
                            Environment.Exit(0);
                        }
                        if (opcao == "1")
                        {
                            bool converteuCnpj = false;
                            opcaoDeRetorno = int.Parse(opcao);
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("**********************************************");
                                Console.WriteLine("****      Digite o CNPJ do cliente        ****");
                                Console.WriteLine("**********************************************");
                                Console.Write("> ");
                                string cnpj = Console.ReadLine();
                                if (Utils.ValidaCnpj(cnpj))
                                {
                                    converteuCnpj = true;
                                    itemParaConsulta = cnpj;
                                    return opcaoDeRetorno;
                                }
                                else if (cnpj == "voltar")
                                {
                                    voltar = true;
                                    break;
                                }
                                else if (cnpj == "cancelar")
                                {
                                    return 99;
                                }
                                else if (cnpj == "encerrar")
                                {
                                    Environment.Exit(0);
                                }
                                else if (voltar)
                                {
                                    break;
                                }
                                else if (!Utils.ValidaCnpj(cnpj))
                                {
                                    Console.WriteLine("CNPJ inálido");
                                    Thread.Sleep(2000);
                                }

                            } while (!converteuCnpj);
                            if (voltar)
                            {
                                indexMenu = 0;
                                continue;
                            }
                            converteu = true;
                        }
                        else if (opcao == "2")
                        {
                            bool converteuData = false;
                            opcaoDeRetorno = int.Parse(opcao);
                            do
                            {
                                converteu = true;
                                opcaoDeRetorno = int.Parse(opcao);
                                Console.Clear();
                                Console.WriteLine("**********************************************");
                                Console.WriteLine("****      Digite a Data desejada          ****");
                                Console.WriteLine("**********************************************");
                                Console.Write("> ");
                                string data = Console.ReadLine();
                                if (DateTime.TryParse(data, out DateTime result))
                                {
                                    converteuData = true;
                                    itemParaConsulta = data;
                                    return opcaoDeRetorno;
                                }
                                else if (data == "voltar")
                                {
                                    voltar = true;
                                    break;
                                }
                                else if (data == "cancelar")
                                {
                                    return 99;
                                }
                                else if (data == "encerrar")
                                {
                                    Environment.Exit(0);
                                }
                                else if (voltar)
                                {
                                    break;
                                }
                                else if (!converteuData)
                                {
                                    Console.WriteLine("Data inválida, digite algo como 01/10/2010");
                                    Thread.Sleep(2000);
                                }
                            } while (!converteuData);
                            if (voltar)
                            {
                                indexMenu = 0;
                                continue;
                            }
                            converteu = true;
                        }
                        else if (opcao == "voltar")
                        {
                            return 88;
                        }
                        else if (opcao == "cancelar")
                        {
                            return 99;
                        }
                        else if (opcao == "encerrar")
                        {
                            Environment.Exit(0);
                        }
                        if (!converteu)
                        {
                            Console.WriteLine("Opção inválida");
                            Thread.Sleep(2000);
                        }
                    } while (!converteu);
                }

            }
            return opcaoDeRetorno;
        }
    }
}
