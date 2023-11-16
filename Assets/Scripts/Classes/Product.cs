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
    private string update = DateTime.Now.ToString("dd-MM-yyyy"); //Last price update

    public string Code { get => code; set => code = value; }
    public string Name { get => name; set => name = value; }
    public string Brand { get => brand; set => brand = value; }
    public string Category { get => category; set => category = value; }
    public int Quant { get => quant; set => quant = value; }
    public double Cost { get => cost; set => cost = value; }
    public double Price { get => price; set => price = value; }
    public string Update { get => update; set => update = value; }

    public Product(string code, string name, string brand, string category, int quant, double cost, double price, string update)
    {
        Code = code;
        Name = name;
        Brand = brand;
        Category = category;
        Quant = quant;
        Cost = cost;
        Price = price;
        Update = update;
    }

    public Product(string code)
    {
        Code = code;
    }
    public Product(string code, string name, string brand)
    {
        Code = code;
        Name = name;
        Brand = brand;
    }
    public void SetAll(string code, string name, string brand, string category, int quant, double cost, double price)
    {
        Code = code;
        Name = name;
        Brand = brand;
        Category = category;
        Quant = quant;
        Cost = cost;
        if(Price != price)
        {
            Update = DateTime.Now.ToString("dd-MM-yyyy");
            Price = price;
        }
    }

    public override bool Equals(object obj)
    {
        return obj is Product p &&
               (Code == p.Code //Code is the key
                || (Name == p.Name && Brand == p.Brand) //But name and brand are also secundary keys
               );
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code);
    }

    public override string ToString()
    {
        return Code + ";" + Name + ";" + Brand + ";" + Category + ";" + Quant + ";" + Cost + ";" + Price + ";" + Update;
    }
}