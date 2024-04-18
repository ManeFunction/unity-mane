using System;

namespace Mane.Yield
{
    public class WaitForSecondsRealtimeWhile : WaitForSecondsUntilBase
    {
        public WaitForSecondsRealtimeWhile(Func<bool> predicate, float waitSeconds, bool checkPredicateFirst = false)
            : base(predicate, waitSeconds, checkPredicateFirst) { }

        public override bool keepWaiting => IsKeepWaiting(false, false);
    }
}