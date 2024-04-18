using System;

namespace Mane.Yield
{
    public class WaitForSecondsRealtimeUntil : WaitForSecondsUntilBase
    {
        public WaitForSecondsRealtimeUntil(Func<bool> predicate, float waitSeconds, bool checkPredicateFirst = false)
            : base(predicate, waitSeconds, checkPredicateFirst) { }

        public override bool keepWaiting => IsKeepWaiting(true, true);
    }
}