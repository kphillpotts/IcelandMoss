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
                    HeroColor = "#95C9F7",
                    Name="Sky Blue",
                    Price = 12,
                    ImageUrl = "blue_moss",
                    IsFeatured = true
                },
                new ProductViewModel()
                {
                    HeroColor = "#FFCA81",
                    Name="Yellow Sun",
                    Price = 17,
                    ImageUrl = "yellow_moss",
                    IsFeatured = true
                },

                new ProductViewModel()
                {
                    HeroColor = "#A2BAD3",
                    Name="Grey Blue",
                    Price = 19,
                    ImageUrl = "grey_moss",
                    IsFeatured = true
                },

                new ProductViewModel()
                {
                    HeroColor = "#F796DD",
                    Name="Pink",
                    Price = 21,
                    ImageUrl = "pink_moss",
                    IsFeatured = false
                },

                 new ProductViewModel()
                {
                    HeroColor = "#95C9F7",
                    Name="Sky Blue",
                    Price = 12,
                    ImageUrl = "blue_moss",
                    IsFeatured = false
                },

                new ProductViewModel()
                {
                    HeroColor = "#D69EFC",
                    Name="Lavender",
                    Price = 19,
                    ImageUrl = "lavender_moss",
                    IsFeatured = false
                },
                new ProductViewModel()
                {
                    HeroColor = "#74D69E",
                    Name="Green Life",
                    Price = 14,
                    ImageUrl = "green_moss",
                    IsFeatured = true
                },
                new ProductViewModel()
                {
                    HeroColor = "#FB8183",
                    Name="Red",
                    Price = 21,
                    ImageUrl = "red_moss",
                    IsFeatured = false
                },
                new ProductViewModel()
                {
                    HeroColor = "#FB9B64",
                    Name="Orange",
                    Price = 14,
                    ImageUrl = "orange_moss",
                    IsFeatured = false
                },
                new ProductViewModel()
                {
                    HeroColor = "#D69EFC",
                    Name="Lavender",
                    Price = 19,
                    ImageUrl = "lavender_moss",
                    IsFeatured = false
                },

            };
        }
    }
}
