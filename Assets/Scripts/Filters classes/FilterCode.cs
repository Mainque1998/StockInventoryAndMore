using System;

public class FilterCode : Filter
{
    string code;
    public FilterCode(string c)
    {
        code = c;
    }

    public bool Satisfy(Product p)
    {
        return p.Code.Contains(code);
    }
}