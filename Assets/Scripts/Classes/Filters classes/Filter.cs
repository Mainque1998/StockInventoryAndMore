using System;

public interface Filter
{
    public abstract bool Satisfy(Product p);
}