using Scarp.Primitive;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Scarp.EntityFrameworkCore.Storage.ValueConversion {
    public class IntValueConverter<Tag> : ValueConverter<Int<Tag>, int> {
        public IntValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Int<Tag>(e), mappingHints) { }
    }

    public class NullableIntValueConverter<Tag> : ValueConverter<Int<Tag>?, int?> {
        public NullableIntValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (int?) null,
                e => e.HasValue ? new Int<Tag>(e.Value) : (Int<Tag>?) null,
                mappingHints) { }
    }

    public class UIntValueConverter<Tag> : ValueConverter<UInt<Tag>, uint> {
        public UIntValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new UInt<Tag>(e), mappingHints) { }
    }

    public class NullableUIntValueConverter<Tag> : ValueConverter<UInt<Tag>?, uint?> {
        public NullableUIntValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (uint?) null,
                e => e.HasValue ? new UInt<Tag>(e.Value) : (UInt<Tag>?) null,
                mappingHints) { }
    }

    public class LongValueConverter<Tag> : ValueConverter<Long<Tag>, long> {
        public LongValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Long<Tag>(e), mappingHints) { }
    }

    public class NullableLongValueConverter<Tag> : ValueConverter<Long<Tag>?, long?> {
        public NullableLongValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (long?) null,
                e => e.HasValue ? new Long<Tag>(e.Value) : (Long<Tag>?) null,
                mappingHints) { }
    }

    public class ULongValueConverter<Tag> : ValueConverter<ULong<Tag>, ulong> {
        public ULongValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new ULong<Tag>(e), mappingHints) { }
    }

    public class NullableULongValueConverter<Tag> : ValueConverter<ULong<Tag>?, ulong?> {
        public NullableULongValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (ulong?) null,
                e => e.HasValue ? new ULong<Tag>(e.Value) : (ULong<Tag>?) null,
                mappingHints) { }
    }

    public class FloatValueConverter<Tag> : ValueConverter<Float<Tag>, float> {
        public FloatValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Float<Tag>(e), mappingHints) { }
    }

    public class NullableFloatValueConverter<Tag> : ValueConverter<Float<Tag>?, float?> {
        public NullableFloatValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (float?) null,
                e => e.HasValue ? new Float<Tag>(e.Value) : (Float<Tag>?) null,
                mappingHints) { }
    }

    public class DoubleValueConverter<Tag> : ValueConverter<Double<Tag>, double> {
        public DoubleValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Double<Tag>(e), mappingHints) { }
    }

    public class NullableDoubleValueConverter<Tag> : ValueConverter<Double<Tag>?, double?> {
        public NullableDoubleValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (double?) null,
                e => e.HasValue ? new Double<Tag>(e.Value) : (Double<Tag>?) null,
                mappingHints) { }
    }

    public class DecimalValueConverter<Tag> : ValueConverter<Decimal<Tag>, decimal> {
        public DecimalValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new Decimal<Tag>(e), mappingHints) { }
    }

    public class NullableDecimalValueConverter<Tag> : ValueConverter<Decimal<Tag>?, decimal?> {
        public NullableDecimalValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (decimal?) null,
                e => e.HasValue ? new Decimal<Tag>(e.Value) : (Decimal<Tag>?) null,
                mappingHints) { }
    }

    public class StringValueConverter<Tag> : ValueConverter<String<Tag>, string> {
        public StringValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new String<Tag>(e), mappingHints) { }
    }

    public class NullableStringValueConverter<Tag> : ValueConverter<String<Tag>?, string> {
        public NullableStringValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : null,
                e => new String<Tag>(e),
                mappingHints) { }
    }
}