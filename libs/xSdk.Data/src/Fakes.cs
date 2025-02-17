using System;
using System.Collections.Generic;
using Bogus;

namespace xSdk.Data
{
    public abstract class Fakes<TEntity>
        where TEntity : class, IEntity
    {
        protected bool RepeatableData { get; private set; }

        protected bool StrictMode { get; private set; }

        protected string Context { get; private set; }

        internal void BuildInternal(
            Faker<TEntity> builder,
            string context,
            bool repeatableData,
            bool strictMode
        )
        {
            RepeatableData = repeatableData;
            StrictMode = strictMode;
            Context = context;

            Build(builder);
        }

        protected abstract void Build(Faker<TEntity> builder);

        protected TFakeEntity Generate<TFake, TFakeEntity>()
            where TFake : Fakes<TFakeEntity>, new()
            where TFakeEntity : class, IEntity =>
            FakeGenerator.Generate<TFake, TFakeEntity>(Context, RepeatableData, StrictMode);

        protected IEnumerable<TFakeEntity> GenerateList<TFake, TFakeEntity>()
            where TFake : Fakes<TFakeEntity>, new()
            where TFakeEntity : class, IEntity =>
            FakeGenerator.GenerateList<TFake, TFakeEntity>(
                new Random().Next(1, 10),
                Context,
                RepeatableData,
                StrictMode
            );

        protected IEnumerable<TFakeEntity> GenerateList<TFake, TFakeEntity>(int count)
            where TFake : Fakes<TFakeEntity>, new()
            where TFakeEntity : class, IEntity =>
            FakeGenerator.GenerateList<TFake, TFakeEntity>(
                count,
                Context,
                RepeatableData,
                StrictMode
            );

        protected bool IsContext(string context)
        {
            if (Context == context)
                return true;

            return false;
        }
    }
}
