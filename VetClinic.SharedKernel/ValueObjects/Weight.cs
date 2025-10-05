
namespace VetClinic.SharedKernel.ValueObjects
{
    public record Weight
    {
        public double Value { get; set; }
        public Weight(double value)
        {
            if(value <= 0) 
                throw new ArgumentOutOfRangeException("Weight value can not be less than or equal to 0");
            
            Value = value;
        }

        public static implicit operator Weight(double value) { 
            return new Weight(value);
        }

        public static implicit operator double(Weight value)
        {
            return value.Value;
        }
    }
}
