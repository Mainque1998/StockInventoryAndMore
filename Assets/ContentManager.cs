using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentManager : MonoBehaviour
{
    public GameObject productPrefab;

    private List<Product> products = new List<Product>();

    public void addNewProduct(string codigo, string producto, string marca, string categoria, string cant, string precio)
    {
        if (products.Contains(new Product(codigo)))
        {
            Debug.Log("ERROR: Ya existe producto con el código " + codigo);
            //TODO: devolver el error al usuario
            return;
        }
        products.Add(new Product(codigo, producto, marca, categoria, int.Parse(cant), double.Parse(precio)));
        Debug.Log("Se agregó producto nuevo de código " + codigo);

        debugProducts();

        GameObject newG = (GameObject)Instantiate(productPrefab);
        newG.transform.SetParent(this.transform);
        loadProduct(newG, codigo, producto, marca, categoria, cant, precio);
    }

    public void updateProduct(GameObject p, string codigo, string producto, string marca, string categoria, string cant, string precio)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        if ((vars[0].text != codigo) && products.Contains(new Product(codigo)))
        {
            Debug.Log("ERROR: Ya existe producto con el código " + codigo);
            //TODO: devolver el error al usuario
            return;
        }

        int posP = products.IndexOf(new Product(vars[0].text));
        products[posP].setAll(codigo, producto, marca, categoria, int.Parse(cant), double.Parse(precio));
        Debug.Log("Se modificó el producto con codigo " + vars[0].text);

        debugProducts();

        loadProduct(p, codigo, producto, marca, categoria, cant, precio);
    }

    private void loadProduct(GameObject p, string codigo, string producto, string marca, string categoria, string cant, string precio)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        vars[0].text = codigo;
        vars[1].text = producto;
        vars[2].text = marca;
        vars[3].text = categoria;
        vars[4].text = cant;
        vars[5].text = precio;
    }

    public void debugProducts()
    {
        string debug = "Lista de productos:";
        foreach (Product pr in products)
            debug += "\n" + pr.ToString();
        Debug.Log(debug);
    }
}

 public class Product
{
    private string codigo= "00000000";
    private string producto= "Producto";
    private string marca= "Marca";
    private string categoria= "Categoría";
    private int cant =0;
    private double precio =0;

    public Product(string codigo, string producto, string marca, string categoria, int cant, double precio)
    {
        this.codigo = codigo;
        this.producto = producto;
        this.marca = marca;
        this.categoria = categoria;
        this.cant = cant;
        this.precio = precio;
    }

    public Product(string codigo)
    {
        this.codigo = codigo;
    }

    public string Codigo { get => codigo; set => codigo = value; }

    public void setAll(string codigo, string producto, string marca, string categoria, int cant, double precio)
    {
        this.codigo = codigo;
        this.producto = producto;
        this.marca = marca;
        this.categoria = categoria;
        this.cant = cant;
        this.precio = precio;
    }

    public override bool Equals(object obj)
    {
        return obj is Product product &&
               Codigo == product.Codigo;
    }

    public override string ToString()
    {
        return this.codigo + ": "+ this.producto + ", " + this.marca + ", " + this.categoria + ", " + this.cant + ", " + this.precio + ".";
    }
}