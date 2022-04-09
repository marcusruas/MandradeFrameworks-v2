using System;
using System.Collections.Generic;
using Bogus;

namespace MandradeFrameworks.Tests.Models
{
    public abstract class StubGenerator<T> where T : class
    {
        public StubGenerator()
        {
            _fakerPTBR = new Faker("pt_BR");
            _random = new Random();
        }
        protected Faker _fakerPTBR;
        protected Random _random;

        public abstract Faker<T> CriarObjeto();
        public IEnumerable<T> CriarListaObjeto(int quantidadeRegistros = 10)
            => CriarObjeto().Generate(quantidadeRegistros);
    }
}