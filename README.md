# Generate Model for Database

dotnet-ef dbcontext scaffold "Host=localhost;Database=healthcare;Username=mhassanin;Password=magical_password" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models --context-dir Models --force