using MongoDB.Bson;
using xSdk.Data.Converters.Mapper;

namespace xSdk.Data
{
    internal sealed class MongoDbModelPK : PrimaryKey<string>
    {
        private object syncObject = new();

        public MongoDbModelPK()
            : base(ObjectId.GenerateNewId().ToString()) { }

        public MongoDbModelPK(ObjectId initialValue)
            : base(initialValue.ToString()) { }

        public MongoDbModelPK(string intialValue)
            : base(ObjectId.Parse(intialValue).ToString()) { }

        protected override TType Convert<TType>(object value)
        {
            lock (syncObject)
            {
                if (PKObjectIdToString.TryConvert(value, out string resultAsString))
                {
                    if (typeof(TType) == typeof(ObjectId))
                    {
                        return (TType)(object)ObjectId.Parse(resultAsString);
                    }
                    else if (typeof(TType) == typeof(string))
                    {
                        return (TType)(object)resultAsString;
                    }
                }
                else if (PKStringToObjectId.TryConvert(value, out ObjectId result))
                {
                    if (typeof(TType) == typeof(ObjectId))
                    {
                        return (TType)(object)result;
                    }
                    else if (typeof(TType) == typeof(string))
                    {
                        return (TType)(object)result.ToString();
                    }
                }
            }
            return default;
        }
    }
}
