using log4net;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace ProgressManage
{
    [PSerializable]
    public class LoggingAspect : OnMethodBoundaryAspect
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoggingAspect));

        public override void OnEntry(MethodExecutionArgs args)
        {
            Log.Debug($"{args.Method.Name} call started");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Log.Debug($"{args.Method.Name} execution has ended");
        }

    }
}
