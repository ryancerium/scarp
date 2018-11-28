# Scarp

[![Build Status](https://travis-ci.com/ryancerium/scarp.svg?branch=master)](https://travis-ci.com/ryancerium/scarp)

## `Scarp.Primitive`

This library was inspired by Jonathan Müller's `type_safe` library, and the general advice to avoid primitive obsession.
It contains `struct`s meant to add type safety for fields that are represented by built-in types, like `int`, `float` values, and `string`s, by adding a `Tag` field to separate the types.

Typically, there's nothing the compiler can do to prevent you from assigning a last name value to a first name field, because they're both `string`s, or assigning a velocity to a mass because they're both `double`s.
This library allows you to use a tagged primitive type, like `String<Tag>` or `Double<Tag>`, to tell the compiler that they're different:

```c#
using FirstName = Scarp.Primitive.String<FirstNameTag>;
using MiddleName = Scarp.Primitive.String<MiddleNameTag>;
using LastName = Scarp.Primitive.String<LastNameTag>;

class FirstNameTag { }
class MiddleNameTag { }
class LastNameTag { }

public class Person {
    FirstName FirstName { get; set; }
    MiddleName? MiddleName { get; set; }
    LastName LastName { get; set; }

    public static void Main(string args[]) {
        var person = new Person() {
            FirstName firstName = "Ryan";
            MiddleName middleName = null;
            LastName lastName = "Phelps";
        };
    }
}
```

All the available operators have been overloaded to work as expected.
Arithmetic, logical, and unary operators all do what you want them to.
They're formattable using the underlying `ToString()` implementation.

`String<Tag>` re-implements every single `System.String` method, with versions taking `string` and `String<Tag>`.
Functions that return a `string[]` return a `String<Tag>[]`.

I haven't come up with a best practice for how to define the resulting types.
Right now it's either a `using MiddleName = Scarp.Primitive.String<Company.Product.MiddleNameTag>;` statement in each file, or a longer `String<MiddleNameTag>` everywhere.

### JSON

The primitive types all map to JSON.Net properly as near as I can tell, with the exception of `String<Tag>`.
Because `String<Tag>` is a struct, you need to explicitly make it nullable.
Please file an issue if you find a problem with serialization.

### Entity Framework Core

You can use the EntityFrameworkCore Value Converters in `Scarp.EntityFrameworkCore.Storage.ValueConversion` to store Scarp primitive types in a database.
You need to explicitly specify the value converter for each property, until EF Core adds support for specifying value conversions for a type instead of for a property.

Read the Entity Framework documentation for the [`ValueConverter`](https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions#the-valueconverter-class) class for more information.

```c#
modelBuilder
    .Entity<Person>()
    .Property(p => p.FirstName)
    .HasConversion(new StringValueConverter<FirstNameTag>());

modelBuilder
    .Entity<Person>()
    .Property(p => p.Height)
    .HasConversion(new IntValueConverter<MeterTag>());
```

## `Scarp.Result`

This class was inspired by Rust's `Result<T, E>` enum type.
C# has limitations without a robust `match()` statement, but c'est la vie.

### `static ResultOk<T> Result.Ok<T>(T t)`

Returns a `ResultOk<T>`, which is implicitly convertible to a `Result<T, E>` for any E.

### `static ResultError<E> Result.Error<E>(E e)`

Returns a `ResultError<E>`, which is implicitly convertible to a `Result<T, E>` for any T.

### `void Result<T, E>.Handle(Action<T> onOk, Action<E> onError)`

If the `Result<T, E>` contains an ok, then executes `onOk` with the ok value as the parameter.
If the `Result<T, E>` contains an error, then executes `onError` with the error value as the parameter.

### `R Result<T, E>.Handle<R>(Func<T, R> onOk, Func<E, R> onError)`

If the `Result<T, E>` contains an ok, then returns the value of `onOk` with the ok value as the parameter.
If the `Result<T, E>` contains an error, then returns the value of `onError` with the error value as the parameter.
I use this to return different `IActionResult` subclasses in my ASP.Net Core controllers.

### `bool Result<T, E>.TryOk(out T t)`

If this is an Ok Result, assigns the ok return value to the t parameter.
Returns true if this is an ok result.

### `bool Result<T, E>.TryError(out E e)`

If this is an Error Result, assigns the error value to the e parameter.
Returns true if this is an error result.

```c#
using Scarp.Result;
using System;

using MiddleName = Scarp.Primitive.String<MiddleNameTag>;

public class MiddleNameTag { };

public class Person {
    public static Result<MiddleName, string> GetMiddleName(string fullName) {
        var names = fullName.Split(' ');
        if (names.Length == 3) {
            return Result.Ok<MiddleName>(names[1]);
        }

        return Result.Error("No middle name.");
    }

    public static void Main(string[] args) {
        Console.Write("Please enter your full name: ");

        var result = GetMiddleName(Console.ReadLine());

        if (result.TryOk(out var middleName)) {
            Console.WriteLine($"Your middle name is: {middleName}");
        }

        if (result.TryError(out var errorMessage)) {
            Console.WriteLine(errorMessage);
        }

        result.Handle(
            ok => Console.WriteLine($"Your middle name is: {ok}"),
            error => Console.WriteLine(error));

        Console.WriteLine(
            result.Handle(
                ok => $"Your middle name is: {ok}",
                error => error));
    }
}
```
