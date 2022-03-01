using System;

namespace CashBook.Data.Exceptions
{
    public class InvalidYearException:ApplicationException
    {
        public InvalidYearException()
        {

        }
        public InvalidYearException(string message) : base(message)
        {

        }
    }
}
