using System;

namespace CashBook.Data.Exceptions
{
    public class InvalidMonthException:ApplicationException
    {
        public InvalidMonthException()
        {

        }

        public InvalidMonthException( string message):base(message) 
        {

        }
    }
}
