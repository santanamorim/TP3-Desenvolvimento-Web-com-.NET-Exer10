using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        using (var db = new ProdutoContext())
        {
            db.Database.EnsureCreated();

            int opcao = 0;
            do
            {
                Console.WriteLine("\n1. Adicionar Produto");
                Console.WriteLine("2. Listar Produtos");
                Console.WriteLine("3. Editar Produto");
                Console.WriteLine("4. Excluir Produto");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out opcao))
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira um número.");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        AdicionarProduto(db);
                        break;
                    case 2:
                        ListarProdutos(db);
                        break;
                    case 3:
                        EditarProduto(db);
                        break;
                    case 4:
                        ExcluirProduto(db);
                        break;
                }
            } while (opcao != 5);
        }
    }

    static void AdicionarProduto(ProdutoContext db)
    {
        Console.Write("Digite o nome do produto: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o preço do produto: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
        {
            Console.WriteLine("Preço inválido.");
            return;
        }

        var produto = new Produto { Nome = nome, Preco = preco };
        db.Produtos.Add(produto);
        db.SaveChanges();
        Console.WriteLine("Produto adicionado com sucesso!");
    }

    static void ListarProdutos(ProdutoContext db)
    {
        var produtos = db.Produtos.ToList();
        Console.WriteLine("\nLista de Produtos:");
        foreach (var produto in produtos)
        {
            Console.WriteLine($"ID: {produto.ProdutoId}, Nome: {produto.Nome}, Preço: {produto.Preco:C}");
        }
    }

    static void EditarProduto(ProdutoContext db)
    {
        Console.Write("Digite o ID do produto a ser editado: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var produto = db.Produtos.Find(id);

        if (produto != null)
        {
            Console.Write("Digite o novo nome do produto: ");
            produto.Nome = Console.ReadLine() ?? produto.Nome;

            Console.Write("Digite o novo preço do produto: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal novoPreco))
            {
                Console.WriteLine("Preço inválido.");
                return;
            }

            produto.Preco = novoPreco;
            db.SaveChanges();
            Console.WriteLine("Produto editado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }

    static void ExcluirProduto(ProdutoContext db)
    {
        Console.Write("Digite o ID do produto a ser excluído: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var produto = db.Produtos.Find(id);

        if (produto != null)
        {
            db.Produtos.Remove(produto);
            db.SaveChanges();
            Console.WriteLine("Produto excluído com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }
}
