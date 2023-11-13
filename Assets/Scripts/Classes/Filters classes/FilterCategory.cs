using System;

public class FilterCategory : Filter
{
    string category;
    public FilterCategory(string c)
    {
        category = c;
    }

    public bool Satisfy(Product p)
    {
        return p.Category.Contains(category);
    }
}