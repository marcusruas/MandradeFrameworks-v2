# MandradeFrameworks

Framework de pacotes [nuget](https://docs.microsoft.com/pt-br/nuget/what-is-nuget) com o intuito de facilitar a implementação do scaffold de uma API ou de um projeto de API diretamente.

## O que é esse projeto?

Esse framework contém a implementação de diversas bibliotecas amplamente utilizados no mercado e práticas de _design patterns_ que irão ajudar ser concisa e de fácil uso e incremento de features, ajudando desde o início de uma requisição, com sua autenticação, até a sua ponta mais externa, como acesso a banco de dados e comunicação externa.

## Qual a arquitetura deste framework?
Este projeto foi criado para dar suporte ao .NET Core.
Cada projeto deste framework possui a estrutura .NET Standard 2.1 (clique [aqui](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard?tabs=net-standard-2-1) para acessar as versões do .NET Core suportadas por essa estrutura)

## Como implementar cada pacote em minha API ou meu scaffold?

Atualmente cada pacote possui uma class estática (_static class_) que já possui toda a configuração do pacote em alguns poucos métodos, normalmente que devem ser executados passando como parâmetro a classe IServiceCollection, para que certas classes auxiliares possam ser injetadas via [injeção de dependência](https://docs.microsoft.com/pt-br/dotnet/core/extensions/dependency-injection) na sua aplicação. Essas classes normalmente são encontradas no namespace "NomeDoPacote.Configuration" de cada pacote.

## Um pouco sobre o que cada pacote oferece

### Autenticacao

O pacote de autenticação possui uma estrutura inicial bem simples para limitar o acesso de certos endpoints e para leitura de tokens passados no header de _Authorization_ de uma requisição. Ao ler um token enviado pelo Header da requisição, a implementação deste pacote irá inserir as informações obtidas em um objeto pronto para uso do usuário em toda aplicação.

### Logs

Pacote com implementação de logs de erros/informação/alerta utilizando a biblioteca Serilog.

### Mensagens

Adiciona um sistema de mensageria a aplicação, podendo adicionar mensagens de erro ou informativas e retornar para o frontend exatamente o que houve durante a solicitação.

### Repositorios

Auxiliares para execução de queries, manuseio de DBContexts do Entity Framework, Melhor armazenamentos de queries SQL e muito mais. Vale lembrar que a arquitetura para este pacote funcione corretamente pode ser encontrada [aqui](https://github.com/marcusruas/ScaffoldApi).

### Retornos

Auxiliares para retorno formatado de informações em qualquer parte da aplicação. Neste pacote há classes que irão auxilia-lo a retornar objetos padronizados nas areas de Endpoints, manuseio de serviços, handlers do mediador, comunicação externa com outras apis/serviços entre outros.

### SharedKernel

Neste projeto há classes gerais (como CPF, Email, Enum etc), Exceptions padronizadas, modelos entre outros objetos utilizados por todos os pacotes deste framework e pela API em si.

### Tests

Auxiliares na criação de testes de integração e unitários. Ele também possui formas de criação de bancos de dados temporários de SQL Server utilizando Entity Framework e assim ter uma estrutura inicial para testes de integração.

## Algumas bibliotecas utilizadas neste framework

[Dapper](https://dapper-tutorial.net/): Para manuseio de banco de dados (CRUD de queries SQL por exemplo);
[Entity Framework](https://docs.microsoft.com/pt-br/ef/): Mesmo motivo acima, porém fornece maior abstração para manuseio de queries SQL no seu banco de dados (usuário pode optar por usar ou o Dapper, ou o EF ou ambos).
[Mediatr](https://github.com/jbogard/MediatR): Para isolação da implementação de serviços e endpoints em handlers e requests, afim de tornar a isolação de implementação e manuseio mais fácil;
[Newtonsoft.Json](https://www.newtonsoft.com/json): Para manusear objetos no formato JSON;
[Serilog](https://serilog.net/): Para gravação de logs da aplicação.

## Como gerar os pacotes para publicação na source?

Na pasta raiz deste projeto, há um script no formato do [Microsoft Power Shell](https://docs.microsoft.com/pt-br/powershell/scripting/overview?view=powershell-7.2) para gerar os arquivos de pacote nuget (extensão .nupkg) e torna-los prontos para publicação na source de sua empresa.

## Sugestões?

Fique a vontade para criar um fork deste repositório ou criar um pull request para nós, assim podemos evoluir cada vez mais as boas práticas deste framework.