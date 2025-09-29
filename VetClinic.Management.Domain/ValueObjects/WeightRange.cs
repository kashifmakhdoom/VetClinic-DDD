namespace VetClinic.Management.Domain.ValueObjects
{
    public record WeightRange
    {
        public decimal From { get; set; }
        public decimal To { get; set; }

        public WeightRange(decimal from, decimal to)
        {
            From = from;
            To = to;
        }
    }
}
