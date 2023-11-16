using System;

public class FilterBrand : Filter
{
    string brand;
    public FilterBrand(string b)
    {
        brand = b;
    }

    public bool Satisfy(Product p)
    {
        return p.Brand.Contains(brand);
    }
}