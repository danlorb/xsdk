using Bogus;
using xSdk.Shared;

namespace xSdk.Data
{
    public static class FakeGenerator
    {
        private const string DefaultContext = "default";
        private static Dictionary<string, object> _fakers;
        private static int GlobalCount = new Random().Next(1, 10);

        public static TEntity Generate<TFake, TEntity>(bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateAsync<TFake, TEntity>(DefaultContext, repeatableData, strictMode).GetAwaiter().GetResult();

        public static TEntity Generate<TFake, TEntity>(string context, bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateAsync<TFake, TEntity>(context, repeatableData, strictMode).GetAwaiter().GetResult();

        public static IEnumerable<TEntity> GenerateList<TFake, TEntity>(bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity =>
            GenerateListAsync<TFake, TEntity>(GlobalCount, DefaultContext, repeatableData, strictMode).GetAwaiter().GetResult();

        public static IEnumerable<TEntity> GenerateList<TFake, TEntity>(string context, bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateListAsync<TFake, TEntity>(GlobalCount, context, repeatableData, strictMode).GetAwaiter().GetResult();

        public static IEnumerable<TEntity> GenerateList<TFake, TEntity>(int count, bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateListAsync<TFake, TEntity>(count, DefaultContext, repeatableData, strictMode).GetAwaiter().GetResult();

        public static IEnumerable<TEntity> GenerateList<TFake, TEntity>(int count, string context, bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateListAsync<TFake, TEntity>(count, context, repeatableData, strictMode).GetAwaiter().GetResult();

        public static Task<TEntity> GenerateAsync<TFake, TEntity>(bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity =>
            GenerateListAsync<TFake, TEntity>(1, DefaultContext, repeatableData, strictMode).ContinueWith(task => task.Result.SingleOrDefault());

        public static Task<TEntity> GenerateAsync<TFake, TEntity>(string context, bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity =>
            GenerateListAsync<TFake, TEntity>(1, context, repeatableData, strictMode).ContinueWith(task => task.Result.SingleOrDefault());

        public static Task<IEnumerable<TEntity>> GenerateListAsync<TFake, TEntity>(bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateListAsync<TFake, TEntity>(GlobalCount, DefaultContext, repeatableData, strictMode);

        public static Task<IEnumerable<TEntity>> GenerateListAsync<TFake, TEntity>(int count, bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateListAsync<TFake, TEntity>(count, DefaultContext, repeatableData, strictMode);

        public static Task<IEnumerable<TEntity>> GenerateListAsync<TFake, TEntity>(string context, bool repeatableData = false, bool strictMode = false)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity => GenerateListAsync<TFake, TEntity>(GlobalCount, context, repeatableData, strictMode);

        public static Task<IEnumerable<TEntity>> GenerateListAsync<TFake, TEntity>(
            int count,
            string context,
            bool repeatableData = false,
            bool strictMode = false
        )
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity
        {
            if (_fakers == null)
                _fakers = new Dictionary<string, object>();

            var baseContext = typeof(TFake).FullName;
            if (string.IsNullOrEmpty(context))
                context = DefaultContext;

            var completeContext = $"{baseContext}_{context}";

            if (!_fakers.ContainsKey(completeContext))
            {
                var faker = InitFaker<TFake, TEntity>(context, repeatableData, strictMode);
                _fakers.AddOrNew(completeContext, faker);
            }

            var bogus = _fakers[completeContext] as Faker<TEntity>;
            if (bogus != null)
                return Task.FromResult<IEnumerable<TEntity>>(bogus.Generate(count));

            return Task.FromResult<IEnumerable<TEntity>>(new List<TEntity>());
        }

        private static object InitFaker<TFake, TEntity>(string context, bool repeatableData, bool strictMode)
            where TFake : Fakes<TEntity>, new()
            where TEntity : class, IEntity
        {
            var fakes = new TFake();
            var bogus = new Faker<TEntity>();

            if (repeatableData)
                Randomizer.Seed = new Random(85416985); // DevSkim: ignore DS148264

            if (strictMode)
                bogus.StrictMode(strictMode);

            fakes.BuildInternal(bogus, context, repeatableData, strictMode);

            return bogus;
        }
    }
}
