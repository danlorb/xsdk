using MongoDB.Bson;
using xSdk.Data.Converters.Mapper;

namespace xSdk.Data
{
    internal sealed class MongoDbEntityPK : PrimaryKey<ObjectId>
    {
        private object syncObject = new();

        public MongoDbEntityPK()
            : base(ObjectId.GenerateNewId()) { }

        public MongoDbEntityPK(ObjectId initialValue)
            : base(initialValue) { }

        public MongoDbEntityPK(string intialValue)
            : base(ObjectId.Parse(intialValue)) { }

        protected override TType Convert<TType>(object value)
        {
            lock (syncObject)
            {
                if (PKStringToObjectId.TryConvert(value, out ObjectId result))
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
                else if (PKObjectIdToString.TryConvert(value, out string resultString))
                {
                    if (typeof(TType) == typeof(ObjectId))
                    {
                        return (TType)(object)ObjectId.Parse(resultString);
                    }
                    else if (typeof(TType) == typeof(string))
                    {
                        return (TType)(object)resultString;
                    }
                }
            }
            return default;
        }
    }
}
