# Scarp

[![Build Status](https://travis-ci.com/ryancerium/scarp.svg?branch=master)](https://travis-ci.com/ryancerium/scarp)

## Scarp.Primitive

This library was inspired by Jonathan Müller's `type_safe` library, and the general advice to avoid primitive obsession.
It contains `struct`s meant to add type safety for fields that are represented by built-in types, like `int`, `float` values, and `string`s, by adding a `Tag` field to separate the types.

Typically, there's nothing the compiler can do to prevent you from comparing a Post row ID to a Blog row ID, because they're both `int`s,
or assigning a post's `Title` text to the `Content` field because they're both `string`s.
This library allows you to use tagged primitive types, like `Long<Post>`, `Long<Blog>`, `String<TitleTag>`, and `String<ContentTag>` to tell the compiler that they're different:

```c#
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scarp.AspNetCore;
using Scarp.EntityFrameworkCore.Storage.ValueConversion;
using Scarp.Primitive;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Scarp.Blogging {
    public class Blog {
        public Long<Blog> Id { get; set; }
        public String<UrlTag> Url { get; set; }

        [InverseProperty(nameof(Post.Blog))]
        public ICollection<Post> Posts { get; set; }

        public class UrlTag { }
    }

    public class Post {
        public Long<Post> Id { get; set; }
        public String<TitleTag> Title { get; set; }
        public String<ContentTag> Content { get; set; }
        public Long<Blog> BlogId { get; set; }

        [ForeignKey(nameof(BlogId))]
        public Blog Blog { get; set; }

        public class TitleTag { }
        public class ContentTag { }
    }

    public class BloggingContext : DbContext {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Model creation is annoying, not gonna lie :-/
            // Some future version of EF Core will let you specify this as an attribute on the class instead
            modelBuilder.Entity<Blog>(e => {
                e.Property(p => p.Id).HasConversion(new LongValueConverter<Blog>());
                e.Property(p => p.Url).HasConversion(new StringValueConverter<Blog.UrlTag>());
            });

            modelBuilder.Entity<Post>(e => {
                e.Property(p => p.Id).HasConversion(new LongValueConverter<Post>());
                e.Property(p => p.Title).HasConversion(new StringValueConverter<Post.TitleTag>());
                e.Property(p => p.Content).HasConversion(new StringValueConverter<Post.ContentTag>());
                e.Property(p => p.BlogId).HasConversion(new LongValueConverter<Blog>());
            });
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }

    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services) {
            services
                .AddDbContext<BloggingContext>(options => {
                    options.UseSqlite("Data Source=blogging.db");
                })
                .AddMvc(options => {
                    // Add parsers for AspInt<Tag>, AspLong<Tag>, etc. as route parameters
                    options.ModelBinderProviders.Insert(0, new ScarpModelBinderProvider());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseHttpsRedirection()
                .UseMvc();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public partial class BlogController : ControllerBase {
        private BloggingContext BloggingContext { get; set; }

        public BlogController(BloggingContext bloggingContext) => BloggingContext = bloggingContext;

        [HttpPost]
        public ActionResult<Blog> CreateBlog(Blog blog) {
            // ValueConverters muck up auto-increment primary key declaration :-/
            blog.Id = BloggingContext.Blogs
                .OrderBy(b => b.Id)
                .Select(b => b.Id)
                .LastOrDefault() + 1;

            BloggingContext.Blogs.Add(blog);
            BloggingContext.SaveChanges();
            return Ok(blog);
        }

        [HttpGet]
        public ActionResult<Blog> RetrieveBlogs() {
            return Ok(BloggingContext.Blogs);
        }

        [HttpGet("{blogId}")]
        public ActionResult<Blog> RetrieveBlog(AspLong<Blog> blogId) {
            // Evaluated locally :-/
            return Ok(BloggingContext.Blogs.SingleOrDefault(blog => blog.Id == blogId.Value));
        }

        [HttpPost("post")]
        public ActionResult<Post> CreatePost(Post post) {
            // ValueConverters muck up auto-increment primary key declaration :-/
            post.Id = BloggingContext.Posts
                .OrderBy(p => p.Id)
                .Select(p => p.Id)
                .LastOrDefault() + 1;

            BloggingContext.Posts.Add(post);
            BloggingContext.SaveChanges();
            return Ok(post);
        }

        [HttpGet("{blogId}/posts")]
        public ActionResult<Post> RetrievePosts(AspLong<Blog> blogId) {
            // Evaluated locally :-/
            return Ok(BloggingContext.Posts.Where(p => p.BlogId == blogId.Value));
        }

        [HttpGet("post/{postId}")]
        public ActionResult<Post> RetrievePost(AspLong<Post> postId) {
            // Evaluated locally :-/
            return Ok(BloggingContext.Posts.SingleOrDefault(post => post.Id == postId.Value));
        }
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
protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<Blog>(e => {
        e.Property(p => p.Id).HasConversion(new LongValueConverter<Blog>());
        e.Property(p => p.Url).HasConversion(new StringValueConverter<Blog.UrlTag>());
    });

    modelBuilder.Entity<Post>(e => {
        e.Property(p => p.Id).HasConversion(new LongValueConverter<Post>());
        e.Property(p => p.Title).HasConversion(new StringValueConverter<Post.TitleTag>());
        e.Property(p => p.Content).HasConversion(new StringValueConverter<Post.ContentTag>());
        e.Property(p => p.BlogId).HasConversion(new LongValueConverter<Blog>());
    });
}
```

### ASP.Net Core

ASP.Net Core doesn't allow `struct`s to be route function parameters.
You can use the classes in the `Scarp.AspNetCore` namespace like `AspInt<Tag>`, `AspString<Tag>`, etc. to specify strongly typed parameters to your route functions.
You need to add the `ScarpModelBinderProvider` (suuuuper object-oriented) to the MVC options in your `ConfigureServices()` function.

```c#
public void ConfigureServices(IServiceCollection services) {
    services.AddMvc(options => {
        // Add parsers for AspInt<Tag>, AspLong<Tag>, etc. as route parameters
        options.ModelBinderProviders.Insert(0, new ScarpModelBinderProvider());
    });
}
```

## Scarp.Result

This class was inspired by Rust's `Result<T, E>` enum type.
C# has limitations without a robust `match()` statement, but c'est la vie.

### `static ResultOk<T> Result.Ok<T>(T t)`

Returns a `ResultOk<T>`, which is implicitly convertible to a `Result<T, E>` for any E.

### `static ResultError<E> Result.Error<E>(E e)`

Returns a `ResultError<E>`, which is implicitly convertible to a `Result<T, E>` for any T.

### `void Result<T, E>.Handle(Action<T> onOk, Action<E> onError)`

### `void Result<T, E>.Then(Action<T> onOk, Action<E> onError)`

If the `Result<T, E>` contains an ok, then executes `onOk` with the ok value as the parameter.
If the `Result<T, E>` contains an error, then executes `onError` with the error value as the parameter.

### `R Result<T, E>.Handle<R>(Func<T, R> onOk, Func<E, R> onError)`

### `R Result<T, E>.Then<R>(Func<T, R> onOk, Func<E, R> onError)`

If the `Result<T, E>` contains an ok, then returns the value of `onOk` with the ok value as the parameter.
If the `Result<T, E>` contains an error, then returns the value of `onError` with the error value as the parameter.
I use this to return different `IActionResult` subclasses in my ASP.Net Core controllers.

### `Result<R, E> Result<T, E>.Bind<R>(Func<T, Result<R, E>> onOk)`

If the `Result<T, E>` contains an ok, then returns the value of `onOk` with the ok value as the parameter.
If the `Result<T, E>` contains an error, then returns a `Result<R, E>` with that error.

### `Result<R, E> Result<T, E>.Map<R>(Func<T, R> onOk)`

If the `Result<T, E>` contains an ok, then returns a `Result<R, E>` with the return value of `onOk` with the ok value as the parameter.
If the `Result<T, E>` contains an error, then returns a `Result<R, E>` with that error.

### `Result<T, E2> Result<T, E>.MapError<E2>(Func<E, E2> onError)`

If the `Result<T, E>` contains an ok, then returns a `Result<T, E2>` with that ok.
If the `Result<T, E>` contains an error, then returns a `Result<T, E2>` with the return value of `onError` with the error value as the parameter.

### `bool Result<T, E>.TryOk(out T t)`

If this is an Ok Result, assigns the ok return value to the t parameter.
Returns true if this is an ok result.

### `bool Result<T, E>.TryError(out E e)`

If this is an Error Result, assigns the error value to the e parameter.
Returns true if this is an error result.

```c#
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scarp.AspNetCore;
using Scarp.Results;

namespace Scarp.Blogging.Api.Controllers {
    public partial class BlogController : ControllerBase {
        /// <summary>
        /// Returns an Ok(t) if t is not null, otherwise an Error(error)
        /// </summary>
        public static Result<T, string> NotNullOrError<T>(T t, string error) where T : class {
            if (object.ReferenceEquals(t, null)) {
                return Result.Error(error);
            }

            return Result.Ok(t);
        }

        [HttpGet("/name/{name}/post/{postNumber}")]
        public ActionResult<Post> RetrievePost(AspString<Blog.UrlTag> blogUrl, int postNumber) {
            return NotNullOrError(
                BloggingContext.Blogs.Include(blog => blog.Posts).FirstOrDefault(b => b.Url.Contains(blogUrl.Value)),
                $"Error: No blog named {blogUrl}"
            ).Bind<Post>(blog => NotNullOrError(
                blog.Posts.Skip(postNumber - 1).FirstOrDefault(),
                $"{blogUrl} does not have {postNumber} posts")
            ).Handle<ActionResult<Post>>(
                post => Ok(post),
                error => BadRequest(error));
        }
    }
}
```

## Release History

-   0.2
    -   **Breaking Change:** `Scarp.Result` namespace renamed `Scarp.Results`
    -   Add ASP.Net Core support
    -   Add EF Core support for nullable values and `String<Tag>`
    -   Add F# `Result<T, E>` APIs: `Bind()`, `Map()`, `MapError()`
    -   Add JavaScript-esque API: `Result<T, E>.Then()`
-   0.1
    -   `Result<T, E>`
    -   Primitive types like `Long<Blog>`
    -   Basic EF Core and JSON.Net support
