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
            Debug.Log("ERROR: Ya existe producto con el c�digo " + codigo);
            //TODO: devolver el error al usuario
            return;
        }
        products.Add(new Product(codigo, producto, marca, categoria, int.Parse(cant), double.Parse(precio)));
        Debug.Log("Se agreg� producto nuevo de c�digo " + codigo);

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
            Debug.Log("ERROR: Ya existe producto con el c�digo " + codigo);
            //TODO: devolver el error al usuario
            return;
        }

        int posP = products.IndexOf(new Product(vars[0].text));
        products[posP].setAll(codigo, producto, marca, categoria, int.Parse(cant), double.Parse(precio));
        Debug.Log("Se modific� el producto con codigo " + vars[0].text);

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