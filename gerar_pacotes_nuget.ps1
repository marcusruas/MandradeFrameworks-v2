Write-Host "Removendo pasta temporaria de pacotes compilados e criando novamente"

Remove-Item PacotesCompilados/ -Force

md PacotesCompilados/

Write-Host "========================================="
Write-Host "Criando pacote nuget de Autenticacao"
Write-Host "========================================="

cd MandradeFrameworks.Autenticacao
dotnet clean
dotnet build
dotnet pack -o ../PacotesCompilados/
cd ../

Write-Host "========================================="
Write-Host "Criando pacote nuget de Logs"
Write-Host "========================================="

cd MandradeFrameworks.Logs
dotnet clean
dotnet build
dotnet pack -o ../PacotesCompilados/
cd ../

Write-Host "========================================="
Write-Host "Criando pacote nuget de Mensagens"
Write-Host "========================================="

cd MandradeFrameworks.Mensagens
dotnet clean
dotnet build
dotnet pack -o ../PacotesCompilados/
cd ../

Write-Host "========================================="
Write-Host "Criando pacote nuget de Repositorios"
Write-Host "========================================="

cd MandradeFrameworks.Repositorios
dotnet clean
dotnet build
dotnet pack -o ../PacotesCompilados/
cd ../

Write-Host "========================================="
Write-Host "Criando pacote nuget de Retornos"
Write-Host "========================================="

cd MandradeFrameworks.Retornos
dotnet clean
dotnet build
dotnet pack -o ../PacotesCompilados/
cd ../

Write-Host "========================================="
Write-Host "Criando pacote nuget de SharedKernel"
Write-Host "========================================="

cd MandradeFrameworks.SharedKernel
dotnet clean
dotnet build
dotnet pack -o ../PacotesCompilados/
cd ../

Write-Host "========================================="
Write-Host "Criando pacote nuget de Tests"
Write-Host "========================================="

cd MandradeFrameworks.Tests
dotnet clean
dotnet build
dotnet pack -o ../PacotesCompilados/
cd ../