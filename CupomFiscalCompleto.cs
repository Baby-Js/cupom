using System;
using System.Collections.Generic;

// Define a classe Produto para armazenar informações de cada item no cupom.
public class Produto
{
    public string Nome { get; set; } // Nome do produto.
    public double Preco { get; set; } // Preço unitário do produto.
    public double DescontoPorcentagem { get; set; } // Desconto em porcentagem aplicado ao produto.

    // Construtor da classe Produto.
    public Produto(string nome, double preco, double descontoPorcentagem)
    {
        Nome = nome;
        Preco = preco;
        DescontoPorcentagem = descontoPorcentagem;
    }
}

// Classe principal do programa.
class Program
{
    // Método principal, ponto de entrada da aplicação.
    static void Main()
    {
        Console.WriteLine("\n************* CUPOM FISCAL *************");
        // 1. Personalização do Cupom Fiscal: Adiciona data e hora da compra.
        Console.WriteLine($"Data/Hora: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
        Console.WriteLine("----------------------------------------");

        List<Produto> produtos = new List<Produto>(); // Lista para armazenar os produtos adicionados.
        bool adicionarMaisProdutos = true;

        // 2. Adicionar Mais Produtos ao Cupom: Permite que o usuário adicione múltiplos produtos.
        while (adicionarMaisProdutos)
        {
            Console.Write("Digite o nome do produto (ou 'fim' para encerrar): ");
            string nomeProduto = Console.ReadLine();

            if (nomeProduto.ToLower() == "fim")
            {
                adicionarMaisProdutos = false; // Encerra o loop se o usuário digitar 'fim'.
                break;
            }

            double precoProduto = 0;
            double descontoPorcentagem = 0;

            // 5. Adicionar Validação de Entrada: Valida o preço do produto.
            while (true)
            {
                Console.Write($"Digite o preço de {nomeProduto}: R$ ");
                string precoInput = Console.ReadLine();
                // Tenta converter a entrada para double e verifica se é um valor não negativo.
                if (double.TryParse(precoInput, out precoProduto) && precoProduto >= 0)
                {
                    break; // Sai do loop se a entrada for válida.
                }
                Console.WriteLine("Preço inválido. Por favor, digite um número válido e não negativo.");
            }

            // 5. Adicionar Validação de Entrada: Valida o desconto em porcentagem.
            while (true)
            {
                Console.Write($"Digite o desconto em porcentagem para {nomeProduto} (ex: 10 para 10%): ");
                string descontoInput = Console.ReadLine();
                // Tenta converter a entrada para double e verifica se está entre 0 e 100.
                if (double.TryParse(descontoInput, out descontoPorcentagem) && descontoPorcentagem >= 0 && descontoPorcentagem <= 100)
                {
                    break; // Sai do loop se a entrada for válida.
                }
                Console.WriteLine("Desconto inválido. Por favor, digite um número entre 0 e 100.");
            }

            // Adiciona o produto à lista.
            produtos.Add(new Produto(nomeProduto, precoProduto, descontoPorcentagem));
        }

        // Verifica se algum produto foi adicionado.
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto adicionado. Encerrando o programa.");
            Console.ReadLine();
            return;
        }

        double totalBruto = 0; // Soma dos preços originais.
        double totalDescontoAplicado = 0; // Soma dos valores de desconto.
        double totalLiquido = 0; // Soma dos preços finais após desconto.

        Console.WriteLine("\n--- Detalhes dos Produtos ---");
        // Itera sobre cada produto na lista para calcular e exibir seus detalhes.
        foreach (var produto in produtos)
        {
            double valorDescontoProduto = produto.Preco * (produto.DescontoPorcentagem / 100); // Calcula o valor do desconto para o produto.
            double precoFinalProduto = produto.Preco - valorDescontoProduto; // Calcula o preço final do produto.

            totalBruto += produto.Preco;
            totalDescontoAplicado += valorDescontoProduto;
            totalLiquido += precoFinalProduto;

            Console.WriteLine($"Produto: {produto.Nome}");
            Console.WriteLine($"  Preço Original: {produto.Preco:C2}");
            Console.WriteLine($"  Desconto Aplicado: {produto.DescontoPorcentagem:F2}%");
            Console.WriteLine($"  Valor do Desconto: {valorDescontoProduto:C2}");
            Console.WriteLine($"  Preço Final: {precoFinalProduto:C2}");
            Console.WriteLine("----------------------------------------");
        }

        Console.WriteLine("\n--- Resumo da Compra ---");
        Console.WriteLine($"Total Bruto: {totalBruto:C2}");
        Console.WriteLine($"Total Desconto Aplicado: {totalDescontoAplicado:C2}");
        Console.WriteLine($"Total Líquido (antes de impostos/descontos adicionais): {totalLiquido:C2}");

        // 3. Implementar Taxa de Impostos
        double taxaImposto = 0;
        while (true)
        {
            Console.Write("Digite a taxa de imposto a ser aplicada (ex: 5 para 5%): ");
            string impostoInput = Console.ReadLine();
            // Valida a entrada da taxa de imposto.
            if (double.TryParse(impostoInput, out taxaImposto) && taxaImposto >= 0)
            {
                break;
            }
            Console.WriteLine("Taxa de imposto inválida. Por favor, digite um número válido e não negativo.");
        }
        double valorImposto = totalLiquido * (taxaImposto / 100); // Calcula o valor do imposto.
        totalLiquido += valorImposto; // Adiciona o imposto ao total líquido.
        Console.WriteLine($"Imposto ({taxaImposto:F2}%): {valorImposto:C2}");
        Console.WriteLine($"Total com Imposto: {totalLiquido:C2}");

        // 6. Implementar Desconto para Clientes VIP
        Console.Write("O cliente é VIP? (s/n): ");
        string isVip = Console.ReadLine().ToLower();
        if (isVip == "s")
        {
            double descontoVipPorcentagem = 0;
            while (true)
            {
                Console.Write("Digite o desconto VIP em porcentagem (ex: 5 para 5%): ");
                string vipDescontoInput = Console.ReadLine();
                // Valida a entrada do desconto VIP.
                if (double.TryParse(vipDescontoInput, out descontoVipPorcentagem) && descontoVipPorcentagem >= 0 && descontoVipPorcentagem <= 100)
                {
                    break;
                }
                Console.WriteLine("Desconto VIP inválido. Por favor, digite um número entre 0 e 100.");
            }
            double valorDescontoVip = totalLiquido * (descontoVipPorcentagem / 100); // Calcula o valor do desconto VIP.
            totalLiquido -= valorDescontoVip; // Aplica o desconto VIP.
            Console.WriteLine($"Desconto VIP ({descontoVipPorcentagem:F2}%): {valorDescontoVip:C2}");
            Console.WriteLine($"Total após Desconto VIP: {totalLiquido:C2}");
        }

        // 7. Adicionar Cupom de Desconto
        Console.Write("Possui um cupom de desconto? Digite o código (ou 'não' para pular): ");
        string cupomCodigo = Console.ReadLine();
        // Verifica se o cupom é 'PROMO10'.
        if (cupomCodigo.ToUpper() == "PROMO10")
        {
            double descontoCupomPorcentagem = 10; // Desconto fixo para o cupom 'PROMO10'.
            double valorDescontoCupom = totalLiquido * (descontoCupomPorcentagem / 100); // Calcula o valor do desconto do cupom.
            totalLiquido -= valorDescontoCupom; // Aplica o desconto do cupom.
            Console.WriteLine($"Cupom 'PROMO10' aplicado! Desconto de {descontoCupomPorcentagem:F2}%: {valorDescontoCupom:C2}");
            Console.WriteLine($"Total após Cupom: {totalLiquido:C2}");
        }
        else if (cupomCodigo.ToLower() != "não")
        {
            Console.WriteLine("Cupom inválido."); // Mensagem para cupom não reconhecido.
        }

        // 4. Adicionar Opção de Pagamento em Parcelas
        Console.Write("Deseja pagar em parcelas? (s/n): ");
        string pagarParcelado = Console.ReadLine().ToLower();
        if (pagarParcelado == "s")
        {
            int numParcelas = 0;
            while (true)
            {
                Console.Write("Digite o número de parcelas: ");
                string parcelasInput = Console.ReadLine();
                // Valida a entrada do número de parcelas.
                if (int.TryParse(parcelasInput, out numParcelas) && numParcelas > 0)
                {
                    break;
                }
                Console.WriteLine("Número de parcelas inválido. Por favor, digite um número inteiro positivo.");
            }
            double valorParcela = totalLiquido / numParcelas; // Calcula o valor de cada parcela.
            Console.WriteLine($"Total a pagar: {totalLiquido:C2} em {numParcelas}x de {valorParcela:C2}");
        }
        else
        {
            Console.WriteLine($"Total a pagar: {totalLiquido:C2} (à vista)"); // Opção de pagamento à vista.
        }

        Console.WriteLine("\n----------------------------------------");
        Console.WriteLine("Obrigado por comprar conosco!");
        Console.WriteLine("************* FIM DO CUPOM *************");

        Console.WriteLine("Pressione Enter para sair.");
        Console.ReadLine(); // Mantém o console aberto até o usuário pressionar Enter.
    }
}


