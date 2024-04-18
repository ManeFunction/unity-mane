using System;

namespace Mane.Yield
{
    public class WaitForSecondsUntil : WaitForSecondsUntilBase
    {
        public WaitForSecondsUntil(Func<bool> predicate, float waitSeconds, bool checkPredicateFirst = false)
            : base(predicate, waitSeconds, checkPredicateFirst) { }

        public override bool keepWaiting => IsKeepWaiting(true, false);
    }
}