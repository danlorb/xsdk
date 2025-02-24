//using xSdk.Shared;

//namespace xSdk.Extensions.Variable
//{
//    public sealed partial class EnvironmentSetup
//    {
//        private bool IsSlimMode = false;
//        private Dictionary<string, object> SetupBag = new Dictionary<string, object>();

//        internal static EnvironmentSetup CreateSlimSetup()
//        {
//            var setup = new EnvironmentSetup();
//            setup.IsSlimMode = true;
//            setup.InitializePlugin();
//            return setup;
//        }

//        private TValue ReadSlimValue<TValue>(string name, TValue defaultValue = default)
//        {
//            if (IsSlimMode)
//            {
//                if(SetupBag.TryGetValue(name, out var value))
//                {
//                    return (TValue)value;
//                }
//                else
//                {
//                    return defaultValue;
//                }
//            }
//            else
//            {
//                return this.ReadValue<TValue>(name);
//            }
//        }

//        private void SetSlimValue<TValue>(string name, TValue value)
//        {
//            if(IsSlimMode)
//            {
//                SetupBag.AddOrNew(name, value);
//            }
//            else
//            {
//                this.SetValue(name, value);
//            }
//        }
//    }
//}
