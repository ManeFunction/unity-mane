using System;
using UnityEngine;

namespace Mane.Yield
{
    public abstract class WaitForSecondsUntilBase : CustomYieldInstruction
    {
        private readonly Func<bool> _predicate;
        private readonly float _waitSeconds;

        private bool _checkPredicateFirst;
        private float _elapsedTime;

        protected WaitForSecondsUntilBase(Func<bool> predicate, float waitSeconds, bool checkPredicateFirst = false)
        {
            _predicate = predicate;
            _waitSeconds = waitSeconds;
            _checkPredicateFirst = checkPredicateFirst;

            _elapsedTime = 0f;
        }

        protected bool IsKeepWaiting(bool isUntil, bool isRealtime)
        {
            if (_checkPredicateFirst)
            {
                _checkPredicateFirst = false;

                if (_predicate() == isUntil)
                    return false;
            }

            _elapsedTime += isRealtime ? Time.unscaledDeltaTime : Time.deltaTime;
            if (_elapsedTime >= _waitSeconds)
            {
                _elapsedTime -= _waitSeconds;

                if (_predicate() == isUntil)
                    return false;
            }

            return true;
        }
    }
}