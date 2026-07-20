using System;

public class PantryItem
{
    protected string _name;
    protected double _quantity;

    public PantryItem(string name, double quantity)
    {
        _name = name;
        _quantity = quantity;
    }

    public string GetName()
    {
        return _name;
    }

    public double GetQuantity()
    {
        return _quantity;
    }
}