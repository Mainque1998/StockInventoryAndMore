using System;

public class Product
{
    private string code= "00000000";
    private string name= "Producto";
    private string brand= "Marca";
    private string category= "Categoría";
    private int quant =0;
    private double cost = 0;
    private double price =0;

    public Product(string code, string name, string brand, string category, int quant, double cost, double price)
    {
        Code = code;
        Name = name;
        Brand = brand;
        Category = category;
        Quant = quant;
        Cost = cost;
        Price = price;
    }

    public Product(string code)
    {
        Code = code;
    }

    public string Code { get => code; set => code = value; }
    public string Name { get => name; set => name = value; }
    public string Brand { get => brand; set => brand = value; }
    public string Category { get => category; set => category = value; }
    public int Quant { get => quant; set => quant = value; }
    public double Cost { get => cost; set => cost = value; }
    public double Price { get => price; set => price = value; }

    public void SetAll(string code, string name, string brand, string category, int quant, double cost, double price)
    {
        Code = code;
        Name = name;
        Brand = brand;
        Category = category;
        Quant = quant;
        Cost = cost;
        Price = price;
    }

    public override bool Equals(object obj)
    {
        return obj is Product p &&
               Code == p.Code;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code);
    }

    public override string ToString()
    {
        return Code + ";" + Name + ";" + Brand + ";" + Category + ";" + Quant + ";" + Cost + ";" + Price;
    }
}