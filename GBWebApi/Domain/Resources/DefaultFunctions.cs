namespace Domain.Resources
{
    public class DefaultFunctions
    {
        public decimal CalculateDiscount(decimal value, decimal percentageDiscount)
        {
            decimal valueDiscount = value * (percentageDiscount / 100);
            return Math.Round(valueDiscount, 2);
        }
    }
}
