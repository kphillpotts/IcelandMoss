using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcelandMoss.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IList<ProductViewModel> Products { get; set; }

        public MainViewModel()
        {
            Products = new ObservableRangeCollection<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Name = "Sky Blue",
                    HeroColor = "#96C9F8",
                    ImageUrl = "moss",
                    Price = 12,
                    IsFeatured = true
                },
                new ProductViewModel()
                {
                    Name = "Yellow Sun",
                    HeroColor = "#FFCA81",
                    ImageUrl = "moss",
                    Price = 17,
                    IsFeatured = false
                },
                new ProductViewModel()
                {
                    Name = "Lavender",
                    HeroColor = "#D69DFB",
                    ImageUrl = "moss",
                    Price = 19,
                    IsFeatured = false
                },

                new ProductViewModel()
                {
                    Name = "Green Life",
                    HeroColor = "#74D59E",
                    ImageUrl = "moss",
                    Price = 14,
                    IsFeatured = true
                }

            };
        }
    }
}
