using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Management.Domain.ValueObjects
{
    public record Weight
    {
        public decimal Value { get; set; }
        public Weight(decimal value)
        {
            if(value <= 0) 
                throw new ArgumentOutOfRangeException("Weight value can not be less than or equal to 0");
            
            Value = value;
        }

        public static implicit operator Weight(double value) { 
            return new Weight(value);
        }
    }
}
