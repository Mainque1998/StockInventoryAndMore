using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentFiltersController : MonoBehaviour
{
    public ContentManager stockManager;
    public TMP_InputField codeInput;
    public TMP_InputField nameInput;
    public TMP_InputField brandInput;
    public TMP_InputField categoryInput;

    public void NewCodeFilter()
    {
        Filter filter = new FilterCode(codeInput.text);

        if (!nameInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterName(nameInput.text));

        if (!brandInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterBrand(brandInput.text));

        if (!categoryInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterCategory(categoryInput.text));

        stockManager.SetFilter(filter);
    }

    public void NewNameFilter()
    {
        Filter filter = new FilterName(nameInput.text);

        if (!codeInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterCode(codeInput.text));

        if (!brandInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterBrand(brandInput.text));

        if (!categoryInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterCategory(categoryInput.text));

        stockManager.SetFilter(filter);
    }

    public void NewBrandFilter()
    {
        Filter filter = new FilterBrand(brandInput.text);

        if (!codeInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterCode(codeInput.text));

        if (!nameInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterName(nameInput.text));

        if (!categoryInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterCategory(categoryInput.text));

        stockManager.SetFilter(filter);
    }

    public void NewCategoryFilter()
    {
        Filter filter = new FilterCategory(categoryInput.text);

        if (!codeInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterCode(codeInput.text));

        if (!nameInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterName(nameInput.text));

        if (!brandInput.text.Equals(""))
            filter = new FilterAnd(filter, new FilterBrand(brandInput.text));

        stockManager.SetFilter(filter);
    }

    public void ClearFilters()
    {
        codeInput.text = "";
        nameInput.text = "";
        brandInput.text = "";
        categoryInput.text = "";

        stockManager.SetFilter(null);
    }
}
