using System;
using System.Reflection.Metadata;

class Program
{
    static void Main()
    {
        Console.WriteLine("************* CUPOM FISCAL *************");
        Console.Write("Digite o nome do produto: ");
        string nomeProduto = Console.ReadLine();

        Console.Write("Digite o preço do produto: R$ ");
        double preco = double.Parse(Console.ReadLine());

        Console.Write("Digite o desconto em porsentagem: ");
        double desconto = double.Parse(Console.ReadLine());

        double valorDesconto = preco * (desconto / 100);

        double precoFinal = preco - valorDesconto;

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Produto: {0}",nomeProduto);
        Console.WriteLine("Preço original: R$ {0}",preco.ToString("F2"));
        Console.WriteLine("Desconto: {0}%",desconto);
        Console.WriteLine("Valor do desconto: R$ {0}",valorDesconto.ToString("F2"));
        Console.WriteLine("Preçomcom desconto: R$ {0}",precoFinal.ToString("F2"));


        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Obrigado por comprar conosco!");
        Console.WriteLine("************* FIM DO CUPOM *************");

        Console.ReadLine();
    }
}