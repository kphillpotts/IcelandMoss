using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IcelandMoss.ViewModels
{
    public class ShoppingCartViewModel : ObservableObject
    {
        private decimal total;
        private int itemCount;

        public IList<ICartItem> Items { get; set; }

        public decimal Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }

        public int ItemCount => Items.OfType<ShoppingCartItem>().Count();


        public ShoppingCartViewModel()
        {
            Items = new ObservableCollection<ICartItem>();
        }

        private ShoppingCartItem FindItem(ProductViewModel itemToFind)
        {
            foreach (var item in Items)
            {
                // only if it's a shopping cart item
                if (item is ShoppingCartItem productItem)
                {
                    if (productItem.Product == itemToFind)
                        return productItem;
                }
            }
            return null;
        }

        private FreightItem GetFreightItem()
        {
            foreach (var item in Items)
            {
                if (item is FreightItem freight)
                    return freight;
            }
            return null;
        }

        private void UpdateTotal()
        {
            decimal calculatedTotal = 0;

            foreach (var item in Items)
            {
                if (item is ShoppingCartItem productItem)
                {
                    calculatedTotal += productItem.Total;
                }
            }

            // calculate freight
            var freight = GetFreightItem();
            freight.CalculateFreight(calculatedTotal);

            Total = calculatedTotal + freight.FreightCharge;

            // tell the binding engine that the number of items may be different.
            OnPropertyChanged(nameof(ItemCount));
        }

        public void IncrementOrder(ProductViewModel item)
        {
            // find out if the product is already in the shopping cart
            var foundItem = FindItem(item);

            // if it is, increment the order count
            if (foundItem != null)
            {
                foundItem.Count++;
            }
            else
            {
                // add the item to the shoping cart with a qty of 1
                var cartItem = new ShoppingCartItem()
                {
                    Product = item,
                    Count = 1
                };
                Items.Insert(0, cartItem);
            }

            UpdateTotal();
        }

        public void RemoveItem(ShoppingCartItem item)
        {
            Items.Remove(item);
            UpdateTotal();
        }
    }
}
