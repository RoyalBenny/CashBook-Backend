using CashBook.DAL.Entity;
using System.Collections.Generic;

namespace CashBook.Data.DAL
{
    public interface IEntryRepository
    {
        Entry AddEntry(Entry entry);
        List<Entry> GetEntries(string username);
        List<Entry> GetEntriesByMonth(string username,int year, int month);
        List<Entry> GetEntriesByYear(string username,int year);
    }
}