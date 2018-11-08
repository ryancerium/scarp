# Scarp

[![Build Status](https://travis-ci.com/ryancerium/scarp.svg?branch=master)](https://travis-ci.com/ryancerium/scarp)

This library was inspired by Jonathan Müller's `type_safe` library, and the general advice to avoid primitive obsession.
It contains `struct`s meant to add type safety for fields that are represented by built-in types, like `int`, `float` values, and `string`s.

Typically, there's nothing the compiler can do to prevent you from assigning a last name value to a first name field, because they're both `string`s.
This library allows you to use a tagged type, `String<Tag>`, to tell the compiler that they're different:

```c#
class FirstNameTag{}
class LastNameTag{}
class AgeTag{}
class MeterTag{}

using FirstName = Scarp.Primitive.String<FirstNameTag>;
using LastName = Scarp.Primitive.String<LastNameTag>;
using Age = Scarp.Primitive.Int<AgeTag>;
using Height = Scarp.Primitive.Float<MeterTag>;

namespace PeopleSoft {i
    public class Person {
        FirstName FirstName { get; set; }
        LastName LastName { get; set; }
        Age Age { get; set; }
        Height { get; set; }

    public static void main(string args[]) {
        FirstName firstName = "Ryan";
        LastName lastName = "Phelps";
        Age = "39";
        Height = 1.6;

        // firstName = lastName; compile-error
    }
}
```
