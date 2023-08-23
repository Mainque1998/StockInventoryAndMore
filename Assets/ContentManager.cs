using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ContentManager : MonoBehaviour
{
    public GameObject productPrefab;

    private List<Product> products = new List<Product>();

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/Stock.txt";
        //TODO: chequear si existe el archivo y si no existe, crearlo
        loadContent();
    }

    private void loadContent()
    {
        StreamReader sr = new StreamReader(filePath);
        string p = sr.ReadLine();
        while (p!= null)
        {
            string[] vars = p.Split(' ');

            products.Add(new Product(vars[0], vars[1], vars[2], vars[3], int.Parse(vars[4]), double.Parse(vars[5])));
            
            GameObject newP = (GameObject)Instantiate(productPrefab);
            newP.transform.SetParent(this.transform);
            loadProduct(newP, vars[0], vars[1], vars[2], vars[3], vars[4], vars[5]);

            p = sr.ReadLine();
        }
        sr.Close();
    }

    private void loadFile()
    {
        StreamWriter sw = new StreamWriter(filePath);

        foreach (Product pr in products)
            sw.WriteLine( pr.ToString() );

        sw.Close();
    }

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

        GameObject newP = (GameObject)Instantiate(productPrefab);
        newP.transform.SetParent(this.transform);
        loadProduct(newP, codigo, producto, marca, categoria, cant, precio);

        loadFile();
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

        loadFile();
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