using System;

namespace CashBook.Data.Exceptions
{
    public class EntryNotFoundException:ApplicationException
    {
        public EntryNotFoundException()
        {


        }
        public EntryNotFoundException(string message):base(message)
        {

        }
    }
}
