using System;
using System.IO;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Maui.Graphics;

namespace Test
{
    public partial class MainPage : ContentPage
    {
        public readonly Animation animation;

        public MainPage()
        {
            InitializeComponent();

            this.animation = new Animation(b => Indicator.Color = Color.FromHsla(b, 1, 0.5), 0, 1);
        }

        private async void Animate2()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000));

                //await ButtonLoad.ScaleTo(2, 2000, Easing.BounceIn);
                //await ButtonLoad.FadeTo(1, 2000, Easing.SpringOut);

                await FrameLoad.ScaleTo(2, 1000, Easing.SpringIn);
                await FrameLoad.ScaleTo(1, 1000, Easing.BounceOut);
            }
        }

        private async void Animate()
        {
            Animation parent = new Animation();
            Animation UpAnimation = new Animation(v => ButtonLoad.Scale = v, 1, 1.5, Easing.BounceIn);
            Animation RotationAnimation = new Animation(v => ButtonLoad.Rotation = v, 0, 0);
            Animation DownAnimation = new Animation(v => ButtonLoad.Scale = v, 1.5, 1, Easing.SpringOut);

            parent.Add(0, 0.5, UpAnimation);
            parent.Add(0, 1, RotationAnimation);
            parent.Add(0.5, 1, DownAnimation);

            parent.Commit(this, "my", 16, 4000, Easing.Linear, (v, c) => ButtonLoad.Rotation = 0, () => true);

            Animation parent2 = new Animation();
            Animation UpAnimation2 = new Animation(v => FrameLoad.Scale = v, 1, 2, Easing.SpringIn);
            Animation RotationAnimation2 = new Animation(v => FrameLoad.Rotation = v, 0, 0);
            Animation DownAnimation2 = new Animation(v => FrameLoad.Scale = v, 2, 1, Easing.BounceOut);

            parent.Add(0, 0.5, UpAnimation2);
            parent.Add(0, 1, RotationAnimation2);
            parent.Add(0.5, 1, DownAnimation2);

            parent.Commit(this, "my", 16, 4000, Easing.Linear, (v, c) => FrameLoad.Rotation = 0, () => true);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Animate2();
            animation.Commit(this, "my2", 16, 2000, Easing.Linear, (b, c) => Indicator.Color = Colors.Black, () => true);
        }

        private async void Btn_Clicked(object sender, EventArgs args)
        {
            //this.AbortAnimation("my");

            FrameLoad.CancelAnimations();
            FrameLoad.Scale = 1;
        }
    }
}