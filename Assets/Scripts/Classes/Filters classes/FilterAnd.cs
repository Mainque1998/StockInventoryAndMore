using System;

public class FilterAnd : Filter
{
    private Filter f1;
    private Filter f2;

    public FilterAnd(Filter filter1, Filter filter2)
    {
        f1 = filter1;
        f2 = filter2;
    }

    public bool Satisfy(Product p)
    {
        return f1.Satisfy(p) && f2.Satisfy(p);
    }
}