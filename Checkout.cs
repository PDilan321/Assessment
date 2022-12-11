namespace Checkout
{
  using System;
  using System.Collections.Generic;

  public class Checkout
  {
    // IDictionary used to access elements by keys. Will store quantity and total price
    private IDictionary<string, IDictionary<int, int>> _price;
    private Dictionary<string, int> _currentBasket = new();


    public Checkout(
      IDictionary<string, IDictionary<int, int>> pricing)
    {
      _price = pricing;
    }


// Key/value pairs, allows them to be enumerated
    public Dictionary<string, int> Basket => _currentBasket;


    public void Add(string item)
    {
      // make sure string passed is valid
      if (string.IsNullOrWhiteSpace(item))
      {
        throw new ArgumentNullException("item cannot be blank");
      }

      // add item to current basket
      if (!_currentBasket.ContainsKey(item))
      {
        _currentBasket[item] = 0;
      }

      _currentBasket[item]++;
    }
    
    private int itemPrice(string item, int quantity)
    {
      var itemPrices = _price[item];

    // found an exact match for this quantity
      if (itemPrices.ContainsKey(quantity))
      {
        return itemPrices[quantity];
      }

      // get closest exact match
      var closestQuantity = itemPrices.Keys.First(x => x < quantity);

      // work out how many items left to price from exact match quantity
      var diffQuantity = quantity - closestQuantity;

      // recursively calculate price for remaining items until there are no more items left to price
      var price = itemPrices[closestQuantity] + itemPrice(item, diffQuantity);
      return price;
    }

    public int TotalPrice
    {
      get{ var totalPrice = _currentBasket.Keys.Sum(item => itemPrice(item, _currentBasket[item])); return totalPrice; }
    }
  }
}