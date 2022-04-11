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
            _fakerObjeto = new Faker<T>();
            _random = new Random();
        }

        protected Faker _fakerPTBR;
        protected Faker<T> _fakerObjeto;
        protected Random _random;

        public abstract void CriarObjeto();

        public T BuildFirst()
            => _fakerObjeto.Generate();
            
        public IEnumerable<T> BuildToList(int quantidadeRegistros = 10)
            => _fakerObjeto.Generate(quantidadeRegistros);
    }
}