# Menistar.EntityFrameworkCore.SoftDelete
Extension for Entity Framework Core to implement soft delete for specific entities without using interfaces.

## Installation
Menistar.EntityFrameworkCore.SoftDelete is available on [NuGet](https://www.nuget.org/packages/Menistar.EntityFrameworkCore.SoftDelete/). 

```sh
dotnet add package Menistar.EntityFrameworkCore.SoftDelete
```
## Usage

###### 1. Enable soft delete for DbContext
Enable the soft delete extention for a `DbContext` by calling the extention method `UseSoftDelete` in the constructor of the `DbContext`. Because extention methods are only available on object instance you have to use `this`.

```c#
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContextDbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        this.UseSoftDelete();  
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
```

###### 2.Configure soft delete for a entity type
Soft delete must be configured for every specific entity in the model by calling the extention method `HasSoftDelete` on the entity builder. The method contains overloads to change the default name used for the shadow property or to use a specific property of the entity. The type of the property must be `DateTime?`.

```c#
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContextDbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        this.UseSoftDelete();  
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure soft delete for blogs using the default shadow property.
        builder.Entity<Blog>()
             .HasSoftDelete();
             
        // Configure soft delete for posts by specifying the name of the shadow property to use.
        builder.Entity<Post>
            .HasSoftDelete("DeleteAt");
            
        // Configure soft delete for comments by specifying a property of type DateTime? on the Comment entity to use.
        builder.Entity<Comment>
            .HasSoftDelete(c => c.DeletedAt);      
    }
}
```

###### 3. (Optional) Add a migration containing the changes to your model

```sh
dotnet ef migrations add AddSoftDelete
```

or 

```sh
Add-Migration AddBlogCreatedTimestamp
```

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the MIT-0 License.

## Contact

Melvin Nijholt - melvin@menistar.nl

Project Link: [https://github.com/Menistar.EntityFrameworkCore.SoftDelete](https://github.com/Menistar.EntityFrameworkCore.SoftDelete)
