using CashBook.DAL.Entity;
using System.Collections.Generic;
using System.Linq;

namespace CashBook.Data.DAL
{
    public class EntryRepository : IEntryRepository
    {
        private readonly CashBookDbContext _db;
        public EntryRepository(CashBookDbContext context)
        {
            _db = context;
        }

        public Entry AddEntry(Entry entry)
        {
            _db.Entries.Add(entry);
            _db.SaveChanges();
            return entry;
        }

        public List<Entry> GetEntries(string username)
        {
            return _db.Entries.Where(i=>i.user.UserName == username).ToList();
        }

        public List<Entry> GetEntriesByYear(string username, int year)
        {
            return _db.Entries.Where<Entry>(x => x.dateTime.Year == year && x.user.UserName == username).ToList();
        }
        public List<Entry> GetEntriesByMonth(string username,int year, int month)
        {
            return _db.Entries.Where<Entry>(x => x.dateTime.Year == year && x.dateTime.Month == month && 
                    x.user.UserName == username).ToList();
        }


    }
}
