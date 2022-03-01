using CashBook.DAL.Entity;
using CashBook.Data.DAL;
using CashBook.Data.Exceptions;
using System;
using System.Collections.Generic;

namespace CashBook.Business
{
    public class EntryServices : IEntryServices
    {
        private readonly IEntryRepository _repository;

        public EntryServices(IEntryRepository repository)
        {
            _repository = repository;
        }

        public Entry AddEntry(Entry entry)
        {
            try
            {
                return _repository.AddEntry(entry);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Entry> GetEntries(string username)
        {

            var entires = _repository.GetEntries(username);
            if (entires == null) throw new EntryNotFoundException("Entries not found");
            return entires;

        }

        public List<Entry> GetEntriesByMonth(string username, int month, int year)
        {
            if (month <= 0 || month > 12)
            {
                throw new InvalidMonthException("month value must ranges between 1 and 12");
            }
            if (year < 1900 || year > DateTime.Now.Year)
                throw new InvalidYearException($"year value must ranges between 1900 and {DateTime.Now.Year}");
            return _repository.GetEntriesByMonth(username,year, month);
        }

        public List<Entry> GetEntriesByYear( string username,int year)
        {
            if (year < 1900 || year > DateTime.Now.Year)
                throw new InvalidYearException($"year value must ranges between 1900 and {DateTime.Now.Year}");
            return _repository.GetEntriesByYear(username,year);
        }



    }
}
