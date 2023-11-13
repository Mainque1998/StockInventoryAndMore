using System;

public class FilterName : Filter
{
    string name;
    public FilterName(string n)
    {
        name = n;
    }

    public bool Satisfy(Product p)
    {
        return p.Name.Contains(name);
    }
}