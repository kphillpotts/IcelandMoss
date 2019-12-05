using IcelandMoss.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace IcelandMoss
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        enum States
        {
            SearchExpanded,
            SearchHidden
        }
            

        public MainPage()
        {
            InitializeComponent();
        }

        Storyboard _storyboard = new Storyboard();

        const int margin = 20;
        const int animationSpeed = 250;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SizeChanged += MainPage_SizeChanged;
            ScrollContainer.Scrolled += ScrollContainer_Scrolled;
        }

        private async void ScrollContainer_Scrolled(object sender, ScrolledEventArgs e)
        {
            if ((e.ScrollY > 0) && (CurrentState != States.SearchHidden))
            {
                _storyboard.Go(States.SearchHidden);
                CurrentState = States.SearchHidden;
                ScrollContainer.IsEnabled = false;
                await Task.Delay(animationSpeed);
                ScrollContainer.IsEnabled = true;
            }
            else if ((e.ScrollY == 0) && (CurrentState != States.SearchExpanded))
            {
                _storyboard.Go(States.SearchExpanded);
                CurrentState = States.SearchExpanded;
                ScrollContainer.IsEnabled = false;
                await Task.Delay(animationSpeed);
                ScrollContainer.IsEnabled = true;
            }


        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {

            _storyboard = new Storyboard();
            var width = this.Width;
            var height = this.Height;

            // shopping cart
            Rectangle basketRect = new Rectangle(
                x: width - (BasketIcon.Width + margin),
                y: margin,
                width: BasketIcon.Width,
                height: BasketIcon.Height
                );
            AbsoluteLayout.SetLayoutBounds(BasketIcon, basketRect);

            // search icon
            Rectangle searchRect = new Rectangle(
                x: margin,
                y: 200,
                width: SearchIcon.Width,
                height: SearchIcon.Height
                );
            AbsoluteLayout.SetLayoutBounds(SearchIcon, searchRect);

            Rectangle searchRectCollapsed = new Rectangle(
                x: BasketIcon.Bounds.Left - (margin + SettingsIcon.Width + margin + SearchIcon.Width),
                y: margin,
                width: SearchIcon.Width,
                height: SearchIcon.Height
            );

            // settings icon
            Rectangle settingsRect = new Rectangle(
                x: width - (SettingsIcon.Width + margin),
                y: 200,
                width: SettingsIcon.Width,
                height: SettingsIcon.Height
                );
            AbsoluteLayout.SetLayoutBounds(SettingsIcon, settingsRect);

            Rectangle settingsRectCollapsed = new Rectangle(
                x: BasketIcon.Bounds.Left - (margin + SettingsIcon.Width),
                y: margin,
                width: SettingsIcon.Width,
                height: SettingsIcon.Height
                );



            Rectangle searchBackgroundRect = new Rectangle(
                x: margin,
                y: 200,
                width: SettingsIcon.Bounds.X - (margin + margin),
                height: SearchBackground.Height
                );
            AbsoluteLayout.SetLayoutBounds(SearchBackground, searchBackgroundRect);

            Rectangle searchBackgroundCollapsedRect = new Rectangle(
                x: BasketIcon.Bounds.Left - (margin + SettingsIcon.Width + margin + SearchIcon.Width),
                y: margin,
                width: SettingsIcon.Width,
                height: SettingsIcon.Height
            );


            // ScrollContainer
            Rectangle scrollContainerRect = new Rectangle(
                x: margin,
                y: SearchIcon.Bounds.Bottom + margin,
                width: width - (2 * margin),
                height: height - (SearchIcon.Bounds.Bottom + margin)
                );
            AbsoluteLayout.SetLayoutBounds(ScrollContainer, scrollContainerRect);

            Rectangle scrollContainerRectCollapsed = new Rectangle(
                x: margin,
                y: margin + BasketIcon.Height + margin,
                width: width - (2 * margin),
                height: height - (margin + BasketIcon.Height + margin)
                );

            // add the positions to the state machine
            _storyboard.Add(States.SearchExpanded, new[]
            {
                new ViewTransition(Header, AnimationType.Opacity, 1, animationSpeed),
                new ViewTransition(SearchEntry, AnimationType.Opacity, 1, animationSpeed),
                new ViewTransition(SettingsIcon, AnimationType.Layout, settingsRect, animationSpeed),
                new ViewTransition(SearchIcon, AnimationType.Layout, searchRect, animationSpeed),
                new ViewTransition(SearchBackground, AnimationType.Layout, searchBackgroundRect, animationSpeed),
                new ViewTransition(ScrollContainer, AnimationType.Layout, scrollContainerRect, animationSpeed)
            });

            _storyboard.Add(States.SearchHidden, new[]
            {
                new ViewTransition(Header, AnimationType.Opacity, 0.01, animationSpeed),
                new ViewTransition(SearchEntry, AnimationType.Opacity, 0.01),
                new ViewTransition(SettingsIcon, AnimationType.Layout, settingsRectCollapsed, animationSpeed),
                new ViewTransition(SearchIcon, AnimationType.Layout, searchRectCollapsed, animationSpeed),
                new ViewTransition(SearchBackground, AnimationType.Layout, searchBackgroundCollapsedRect, animationSpeed),
                new ViewTransition(ScrollContainer, AnimationType.Layout, scrollContainerRectCollapsed, animationSpeed)
            });



        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SizeChanged -= MainPage_SizeChanged;
        }


        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

        }

        States CurrentState = States.SearchExpanded;

        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {
            States newState;
            if (CurrentState == States.SearchExpanded)
                newState = States.SearchHidden;
            else
                newState = States.SearchExpanded;

            _storyboard.Go(newState);
            CurrentState = newState;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // the user has tapped on an element
            ProductDisplay element = sender as ProductDisplay;

            // set the binding context to the selected cell
            FakeProductCell.BindingContext = element.BindingContext;
            FakeProductCell.ImageOffsetX = element.ImageOffsetX;
            FakeProductCell.ImageOffsetY = element.ImageOffsetY;
            FakeProductCell.IsVisible = true;

            // set the layout to the same postion
            var yScroll = ScrollContainer.ScrollY;
            Rectangle rect = new Rectangle(
                x: ScrollContainer.X + element.X,
                y: ScrollContainer.Y + element.Y - yScroll,
                width: element.Width,
            height: element.Height);
            AbsoluteLayout.SetLayoutBounds(FakeProductCell, rect);

            // hide the cell we clicked on
            element.Opacity = 0.01;
            await FakeProductCell.ExpandToFill(this.Bounds);
            element.Opacity = 1;
            // redisplay

         }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            ((View)sender).IsVisible = false;
        }
    }
}
