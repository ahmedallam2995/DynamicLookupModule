# DynamicLookupModule

This Project Is A .NET CORE 2.2 MVC Project With Code-First Database, That Provides You A way To Manage All Your Lookup's CRUD Operation In A Very Simple Way.

**_Let me simplify what this project really does,_**

If you have a table in your database called for example **Countries** that has an (Id and Name) coulmns and you want to create a page that manage CRUD operations for it, first you will create an (API/Controller) with five actions Get(Id), GetAll(), Add(country), Update(Country), Delete(Id),
and then you will go to views to create some or one view to operate with created (API/Controller),

Okay, Thats fine, I have no problem with that, 
But if you have another table lets call it **Cities** and you want to create a page to manange it's crud operations, what will you do ? repeat all pervois steps!, Lets not do that.

Using this project you will be able to add any number of lookups tables you want with a vey simple way, _Let's follow the steps_.

## Entity Section
- Go to _Models_ >> _EntityModels_ and add new class , Lets call it **NewLookupEmaple** it should be inherit from **BaseLookup**
```
public class NewLookupEmaple : BaseLookup
{
}
```
- Go to _Context_ >> _DynamicLookupContext.cs_ and add your DbSet of your Created Class
```
public class DynamicLookupContext : DbContext
{
    public DynamicLookupContext(DbContextOptions<DynamicLookupContext> options) : base(options)
    {
        Database.Migrate();
    }

    ...
    public DbSet<LookupEmaple2> NewLookupEmaple { get; set; }
}
```
- Open package manager console and type 
```add-migration migrationName```


## Enum Section
- Go to _Enums_ >> _LookupEnums.cs_ and add new item with the same name of the class you created and assign it to unique value
```
public enum LookupTypeEnum
{
    ...
    NewLookupEmaple = 3,
}
```


## UI Section
- Go to _Views_ >> _Lookups_ >> _Lookups.cshtml_
- add a new _li_ related to your new enum property you created
```
<ul class="lookups">
    ...
    <li class="link" onclick="LoadLookupInfoPartial(this, '@((int)LookupTypeEnum.NewLookupEmaple)')"><b>NewLookupEmaple</b></li>
</ul>
```

## Done!
thats all, now you can run your application and see the magic happens.

