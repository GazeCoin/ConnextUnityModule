using System;

public abstract class NatsResponse<T>
{
    public string err;
    public T response;

    public bool IsError()
    {
        return !String.IsNullOrEmpty(err);
    }
}
