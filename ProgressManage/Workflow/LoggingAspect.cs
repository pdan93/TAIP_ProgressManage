using System;
using System.Diagnostics;
using log4net;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Workflow
{
    [PSerializable]
    public class LoggingAspect : OnMethodBoundaryAspect
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoggingAspect));

        public override void OnEntry(MethodExecutionArgs args)
        {
            Log.Debug($"Before method: {args.Method.Name} execution");
            args.MethodExecutionTag = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();
            Log.Debug($"After method: {args.Method.Name} was executed in {sw.ElapsedMilliseconds} miliseconds");
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Log.Debug($"Method: {args.Method.Name} throw an excetion {args}");
        }

    }
}
