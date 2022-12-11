namespace Checkout.Tests
{
  using System.Collections.Generic;
  public class Checkout_Tests
  {
    private IDictionary<string, IDictionary<int, int>>? _pricing;

    public void Setup()
    {
      _pricing = new Dictionary<string, IDictionary<int, int>>();


      _pricing.Add("A99", new Dictionary<int, int>
      {
        { 1, 50 },

      });
      _pricing.Add("B15", new Dictionary<int, int>
      {
        { 1, 30 }

      });
      _pricing.Add("C40", new Dictionary<int, int>
      {
        { 1, 60 }

      });
      _pricing.Add("T34", new Dictionary<int, int>
      {
        { 1,99 }

      });
    }

// Not sure if this is valid way of testing code. 
    public void TotalPrice_OneItem_ReturnsExpected()
    {
      var checkout = Create();

      checkout.Add("A99");

      //Should be 50. AssertEquals(50, checkout.TotalPrice)
    }

    public void TotalPrice_MixedItem_ReturnsExpected()
    {
      var checkout = Create();

      checkout.Add("A99");
      checkout.Add("A99");

      checkout.Add("B15");
      checkout.Add("B15");

      checkout.Add("C40");
      checkout.Add("C40");

      checkout.Add("T34");

      // (1*50) + (1*50) + (1*30) + (1*30) + (1*60) + (1*60) + (1*99) = 379. But B15 has special offer, so 364
      // Should be 364. AssertEquals(364, checkout.TotalPrice)
    }

    // Include more edge cases.

    private Checkout Create() => new(_pricing);
  }
}