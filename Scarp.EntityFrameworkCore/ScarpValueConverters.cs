using Scarp.Primitive;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Scarp.EntityFrameworkCore.Storage.ValueConversion {
    public class IntValueConverter<Tag> : ValueConverter<Int<Tag>, int> {
        public IntValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Int<Tag>(e), mappingHints) { }
    }

    public class UIntValueConverter<Tag> : ValueConverter<UInt<Tag>, uint> {
        public UIntValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new UInt<Tag>(e), mappingHints) { }
    }

    public class LongValueConverter<Tag> : ValueConverter<Long<Tag>, long> {
        public LongValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Long<Tag>(e), mappingHints) { }
    }

    public class ULongValueConverter<Tag> : ValueConverter<ULong<Tag>, ulong> {
        public ULongValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new ULong<Tag>(e), mappingHints) { }
    }

    public class FloatValueConverter<Tag> : ValueConverter<Float<Tag>, float> {
        public FloatValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Float<Tag>(e), mappingHints) { }
    }

    public class DoubleValueConverter<Tag> : ValueConverter<Double<Tag>, double> {
        public DoubleValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Double<Tag>(e), mappingHints) { }
    }

    public class DecimalValueConverter<Tag> : ValueConverter<Decimal<Tag>, decimal> {
        public DecimalValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Decimal<Tag>(e), mappingHints) { }
    }
}