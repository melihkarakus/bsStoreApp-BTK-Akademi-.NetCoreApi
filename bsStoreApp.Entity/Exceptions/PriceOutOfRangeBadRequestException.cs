namespace bsStoreApp.Entity.Exceptions
{
        public class PriceOutOfRangeBadRequestException : BadRequestException
        {
            public PriceOutOfRangeBadRequestException() : base("Maximum price should be less then 1000 and greater than 10")
            {

            }
        }
    }
