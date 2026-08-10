using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookApp
{
    public class book
    {
        public int id { get; set; }
        public string title { get; set; }
        public int price { get; set; }
    }
    public class bookDto
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class myDatabase : DbContext
    {
        public myDatabase(DbContextOptions<myDatabase> options) : base(options) { }
        public DbSet<book> books { get; set; }
    }

    public interface iBookRepo
    {
        Task addBook(book b);
        Task<book> getBook(int id);
        Task updateBook(book b);
        Task deleteBook(int id);
    }

    public class bookRepository : iBookRepo
    {
        myDatabase db;
        public bookRepository(myDatabase _db) { db = _db; }
        public async Task addBook(book b) { db.books.Add(b); await db.SaveChangesAsync(); }
        public async Task<book> getBook(int id) { var findbook = await db.books.FindAsync(id); return findbook; }
        public async Task updateBook(book b) { db.books.Update(b); await db.SaveChangesAsync(); }
        public async Task deleteBook(int id) { var findbook = await db.books.FindAsync(id); if (findbook != null) { db.books.Remove(findbook); await db.SaveChangesAsync(); } }
    }

    public interface iMyMapper { bookDto convertToDto(book b); }

    public class BookManager
    {
        iBookRepo repo;
        iMyMapper mapper;
        public BookManager(iBookRepo r, iMyMapper m) { repo = r; mapper = m; }

        public async Task<bookDto> findBookProcess(int bookId)
        {
            if (bookId == 0) { throw new Exception("id cant be zero"); }
            if (bookId < 0) { throw new Exception("id cant be negative"); }

            book result = await repo.getBook(bookId);
            if (result == null) { return null; }

            bookDto finalDto = mapper.convertToDto(result);
            return finalDto;
        }
    }
}