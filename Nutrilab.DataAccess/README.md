cd ..

cd Nutrilab.DataAccess

>> Dodavanje nove migracije
dotnet ef --startup-project ..\Nutrilab.WebApi\Nutrilab.WebApi.csproj migrations add MIGRATION_NAME

>> Update na odredjenu migraciju
dotnet ef --startup-project ..\Nutrilab.WebApi\Nutrilab.WebApi.csproj database update

>> Delete migration
dotnet ef --startup-project ..\Nutrilab.WebApi\Nutrilab.WebApi.csproj migrations remove

Note: pri brisanju migracije postaraj se da ta nije primenjena nad bazom
