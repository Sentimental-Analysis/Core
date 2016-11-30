using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Result<T>
    {
        public T Value { get; }

        public bool IsSuccess { get; }

        public IEnumerable<string> Messages { get; }

        public Result(T val, bool isSuccess, params string[] messages)
        {
            this.Value = val;
            this.IsSuccess = isSuccess;
            this.Messages = messages;
        }

        public static Result<TResult> Wrap<TResult>(TResult result, params string[] messages) where TResult : class
        {
            return new Result<TResult>(result, result != null, messages);
        }

        public static Result<TResult> Wrap<TResult>(TResult value, Func<TResult, bool> predicate,
            params string[] messages)
        {
            return new Result<TResult>(value, predicate?.Invoke(value) ?? default(bool), messages);
        }

        public static Result<TResult> WrapValue<TResult>(TResult result, params string[] messages)
            where TResult : struct
        {
            return new Result<TResult>(result, true, messages);
        }

        public static Result<T> Error(params string[] messages)
        {
            return new Result<T>(default(T), false, messages);
        }

        public static Result<T> Success(T value, params string[] messages)
        {
            return new Result<T>(value, true, messages);
        }
    }
}