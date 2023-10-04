using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class ContentManager : MonoBehaviour
{
    public GameObject productPrefab;

    private List<Product> products = new List<Product>();

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/Stock.txt";
        //TODO: chequear si existe el archivo y si no existe, crearlo
        if (File.Exists(filePath))
            LoadContent();
        else
            File.Create(filePath);
    }

    private void LoadContent()
    {
        StreamReader sr = new StreamReader(filePath);
        string p = sr.ReadLine();
        while (p!= null)
        {
            string[] vars = p.Split(' ');//TODO: Change this bcs the name can contains ' '

            products.Add(new Product(vars[0], vars[1], vars[2], vars[3], int.Parse(vars[4]), double.Parse(vars[5]), double.Parse(vars[6])));
            
            GameObject newP = (GameObject)Instantiate(productPrefab);
            newP.transform.SetParent(this.transform);
            LoadProduct(newP, vars[0], vars[1], vars[2], vars[3], vars[4], vars[5], vars[6]);

            p = sr.ReadLine();
        }
        sr.Close();
    }

    private void ReLoadContent()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Product pr in products)
        {
            GameObject newP = (GameObject)Instantiate(productPrefab);
            newP.transform.SetParent(this.transform);
            LoadProduct(newP, pr.Codigo, pr.Producto, pr.Marca, pr.Categoria, pr.Cant.ToString(), pr.Costo.ToString(), pr.Precio.ToString());
        }
    }

    private void LoadFile()
    {
        StreamWriter sw = new StreamWriter(filePath);

        foreach (Product pr in products)
            sw.WriteLine( pr.ToString() );

        sw.Close();
    }

    public void AddNewProduct(string codigo, string producto, string marca, string categoria, string cant, string costo, string precio)
    {
        if (products.Contains(new Product(codigo)))
        {
            Debug.Log("ERROR: Ya existe producto con el código " + codigo);
            //TODO: devolver el error al usuario
            return;
        }
        products.Add(new Product(codigo, producto, marca, categoria, int.Parse(cant), double.Parse(costo), double.Parse(precio)));
        Debug.Log("Se agregó producto nuevo de código " + codigo);

        GameObject newP = (GameObject)Instantiate(productPrefab);
        newP.transform.SetParent(this.transform);
        LoadProduct(newP, codigo, producto, marca, categoria, cant, costo, precio);

        LoadFile();
    }

    public void UpdateProduct(GameObject p, string codigo, string producto, string marca, string categoria, string cant, string costo, string precio)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        if ((vars[0].text != codigo) && products.Contains(new Product(codigo)))
        {
            Debug.Log("ERROR: Ya existe producto con el código " + codigo);
            //TODO: devolver el error al usuario
            return;
        }

        int posP = products.IndexOf(new Product(vars[0].text));
        products[posP].SetAll(codigo, producto, marca, categoria, int.Parse(cant), double.Parse(costo), double.Parse(precio));
        Debug.Log("Se modificó el producto con codigo " + vars[0].text);

        LoadProduct(p, codigo, producto, marca, categoria, cant, costo, precio);

        LoadFile();
    }

    public void DeleteProduct(GameObject p)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        products.Remove(new Product(vars[0].text));
        Destroy(p);

        LoadFile();
    }

    private void LoadProduct(GameObject p, string codigo, string producto, string marca, string categoria, string cant, string costo, string precio)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        vars[0].text = codigo;
        vars[1].text = producto;
        vars[2].text = marca;
        vars[3].text = categoria;
        vars[4].text = cant;
        vars[5].text = costo;
        vars[6].text = precio;
    }

    public void UpdatePriceByFilters(int typeFilter, string filter, int avg)
    {
        avg += 100;
        double average = double.Parse(avg.ToString()) / 100;
        if (typeFilter == 0)//Change all
        {
            foreach (Product pr in products)
                pr.Precio = pr.Precio * average;
        }
        if (typeFilter == 1)//Change by Categoria
        {
            foreach (Product pr in products)
                if (pr.Categoria == filter)
                {
                    pr.Precio = pr.Precio * average;
                }
        }
        if (typeFilter == 2)//Change by Marca
        {
            foreach (Product pr in products)
                if (pr.Marca == filter)
                {
                    pr.Precio = pr.Precio * average;
                }
        }
        ReLoadContent();

        LoadFile();
    }

    public void ReOrderContentByCodigo()//TODO: REPLICAR ESTO PARA TODOS LOS CAMPOS
    {
        products.Sort(CompareProductsByCodigo);
        ReLoadContent();

    }
    private static int CompareProductsByCodigo(Product p1, Product p2)
    {
        return p1.Codigo.CompareTo(p2.Codigo);
    }
    public void ReOrderContentByProducto()
    {
        products.Sort(CompareProductsByProducto);
        ReLoadContent();
    }
    private static int CompareProductsByProducto(Product p1, Product p2)
    {
        return p1.Producto.CompareTo(p2.Producto);
    }
    public void ReOrderContentByMarca()
    {
        products.Sort(CompareProductsByMarca);
        ReLoadContent();
    }
    private static int CompareProductsByMarca(Product p1, Product p2)
    {
        return p1.Marca.CompareTo(p2.Marca);
    }
    public void ReOrderContentByCategoria()
    {
        products.Sort(CompareProductsByCategoria);
        ReLoadContent();
    }
    private static int CompareProductsByCategoria(Product p1, Product p2)
    {
        return p1.Categoria.CompareTo(p2.Categoria);
    }
    public void ReOrderContentByCant()
    {
        products.Sort(CompareProductsByCant);
        ReLoadContent();
    }
    private static int CompareProductsByCant(Product p1, Product p2)
    {
        return p1.Cant.CompareTo(p2.Cant);
    }
    public void ReOrderContentByCosto()
    {
        products.Sort(CompareProductsByCosto);
        ReLoadContent();
    }
    private static int CompareProductsByCosto(Product p1, Product p2)
    {
        return p1.Costo.CompareTo(p2.Costo);
    }
    public void ReOrderContentByPrecio()
    {
        products.Sort(CompareProductsByPrecio);
        ReLoadContent();
    }
    private static int CompareProductsByPrecio(Product p1, Product p2)
    {
        return p1.Precio.CompareTo(p2.Precio);
    }

    public void DebugProducts()
    {
        string debug = "Lista de productos:";
        foreach (Product pr in products)
            debug += "\n" + pr.ToString();
        Debug.Log(debug);
    }

}