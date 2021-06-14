Entity Framework

Working with the entity framework requires following a set procedure. The database is built up from migrations that are defined through the entity framework. To include a new table/object in the database, you'll have to do the following:

1. Create an object class in the ./Data/Models/ folder

    Be sure to be include the DataAnnotations library so that you can use decorators. These decorators help you define what type of row you want to build into that table

2. Include the object in ./Data/ApplicationDbContext.cs

    This can be achieved by inserting the following code:

public DbSet<MODELNAME> MODELNAME { get; set; }

3. (OPTIONAL) Insert default entries in ./Data/DbInitializer.cs

    These entries will always be included when the database is created freshly.
    This can be achieved by doing the following:

context.MODELNAME.Add( new MODELNAME( arguments[] ) )

4. Create the migration in the CLI

    Using the dotnet command you can automatically create the migration based on the changes you've just applied.\
    This can be achieved by entering the followin line in the CLI (NOTE: Make sure you're in the following folder <.\AdminBeheer\AdminManagement>)

dotnet ef migrations add MIGRATIONMESSAGE

If done correctly, a success message should appear and rebuild the database on the next run.
