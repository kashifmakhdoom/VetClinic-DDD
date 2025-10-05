namespace VetClinic.Management.Domain.ValueObjects
{
    public record WeightRange
    {
        public double From { get; set; }
        public double To { get; set; }

        public WeightRange(double from, double to)
        {
            From = from;
            To = to;
        }
    }
}
