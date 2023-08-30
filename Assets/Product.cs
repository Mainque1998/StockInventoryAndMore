using System;

public class Product
{
    private string codigo= "00000000";
    private string producto= "Producto";
    private string marca= "Marca";
    private string categoria= "Categoría";
    private int cant =0;
    private double costo = 0;
    private double precio =0;

    public Product(string codigo, string producto, string marca, string categoria, int cant, double costo, double precio)
    {
        this.codigo = codigo;
        this.producto = producto;
        this.marca = marca;
        this.categoria = categoria;
        this.cant = cant;
        this.costo = costo;
        this.precio = precio;
    }

    public Product(string codigo)
    {
        this.codigo = codigo;
    }

    public string Codigo { get => codigo; set => codigo = value; }
    public string Producto { get => producto; set => producto = value; }
    public string Marca { get => marca; set => marca = value; }
    public string Categoria { get => categoria; set => categoria = value; }
    public int Cant { get => cant; set => cant = value; }
    public double Costo { get => costo; set => costo = value; }
    public double Precio { get => precio; set => precio = value; }

    public void setAll(string codigo, string producto, string marca, string categoria, int cant, double costo, double precio)
    {
        this.codigo = codigo;
        this.producto = producto;
        this.marca = marca;
        this.categoria = categoria;
        this.cant = cant;
        this.costo = costo;
        this.precio = precio;
    }

    public override bool Equals(object obj)
    {
        return obj is Product product &&
               Codigo == product.Codigo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Codigo);
    }

    public override string ToString()
    {
        return this.codigo + " " + this.producto + " " + this.marca + " " + this.categoria + " " + this.cant + " " + this.costo + " " + this.precio;
    }
}