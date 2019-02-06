using Microsoft.AspNetCore.Mvc;
using Scarp.Primitive;

namespace Scarp.AspNetCore {
    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspInt<Tag> {
        public Int<Tag> Value { get; private set; }

        public AspInt(string value) => Value = new Int<Tag>(int.Parse(value));
    }

    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspUInt<Tag> {
        public UInt<Tag> Value { get; private set; }

        public AspUInt(string value) => Value = new UInt<Tag>(uint.Parse(value));
    }

    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspLong<Tag> {
        public Long<Tag> Value { get; private set; }

        public AspLong(string value) => Value = new Long<Tag>(long.Parse(value));
    }

    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspULong<Tag> {
        public ULong<Tag> Value { get; private set; }

        public AspULong(string value) => Value = new ULong<Tag>(ulong.Parse(value));
    }

    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspFloat<Tag> {
        public Float<Tag> Value { get; private set; }

        public AspFloat(string value) => Value = new Float<Tag>(float.Parse(value));
    }

    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspDouble<Tag> {
        public Double<Tag> Value { get; private set; }

        public AspDouble(string value) => Value = new Double<Tag>(double.Parse(value));
    }

    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspDecimal<Tag> {
        public Decimal<Tag> Value { get; private set; }

        public AspDecimal(string value) => Value = new Decimal<Tag>(decimal.Parse(value));
    }

    [ModelBinder(BinderType = typeof(ScarpModelBinder))]
    public class AspString<Tag> {
        public String<Tag> Value { get; private set; }

        public AspString(string value) => Value = new String<Tag>(value);
    }
}