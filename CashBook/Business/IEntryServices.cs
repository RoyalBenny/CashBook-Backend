using CashBook.DAL.Entity;
using System.Collections.Generic;

namespace CashBook.Business
{
    public interface IEntryServices
    {
        Entry AddEntry(Entry entry);
        List<Entry> GetEntries(string username);
        List<Entry> GetEntriesByMonth(string username,int month, int year);
        List<Entry> GetEntriesByYear(string username,int year);
    }
}