using System;

namespace Mane.Yield
{
    public class WaitForSecondsWhile : WaitForSecondsUntilBase
    {
        public WaitForSecondsWhile(Func<bool> predicate, float waitSeconds, bool checkPredicateFirst = false)
            : base(predicate, waitSeconds, checkPredicateFirst) { }

        public override bool keepWaiting => IsKeepWaiting(false, false);
    }
}