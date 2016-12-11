﻿namespace Core.Builders
{
    public interface IBuilder<T> where T : class
    {
        T Build();
    }
}