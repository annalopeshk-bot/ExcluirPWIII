using Microsoft.EntityFrameworkCore;
using ProjetoCadastroMVC.Data;
using ProjetoCadastroMVC.Models;

namespace ProjetoCadastroMVC.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DatabaseContext dbContext;

        public LivroRepository(DatabaseContext contexto)
        {
            dbContext = contexto;
        }

        public List<Livro> BuscarTodos()
        {
            return dbContext.Livros.ToList();
        }

        public Livro Adicionar(Livro livro)
        {
            dbContext.Livros.Add(livro);
            dbContext.SaveChanges();
            return livro;
        }

        public Livro? BuscarPorId(int id)
        {
            return dbContext.Livros.FirstOrDefault(l => l.Id == id);
        }

        public Livro Atualizar(Livro livro)
        {
            Livro? livroExistente = BuscarPorId(livro.Id);

            if (livroExistente == null)
            {
                throw new Exception("Houve um problema ao atualizar!");
            }

            livroExistente.Titulo = livro.Titulo;
            livroExistente.Autor = livro.Autor;
            livroExistente.Genero = livro.Genero;
            livroExistente.AnoPublicacao = livro.AnoPublicacao;
            livroExistente.CapaUrl = livro.CapaUrl;

            dbContext.Livros.Update(livroExistente);
            dbContext.SaveChanges();

            return livroExistente;
        }

        public bool Excluir(int id)
        {
            Livro? livro = BuscarPorId(id);

            if (livro == null)
            {
                throw new Exception("Houve um problema na exclusão do livro!");
            }

            dbContext.Livros.Remove(livro);
            dbContext.SaveChanges();

            return true;
        }
    }
}