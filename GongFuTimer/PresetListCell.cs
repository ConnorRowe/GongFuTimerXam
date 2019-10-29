using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GongFuTimer
{
    class PresetListCell : ViewCell
    {
        Label teaName, teaType, teaBaseSecs, teaPlusSecs, teaTemp, teaMaxInf, teaAltName;
        BoxView teaColourBox;
        

        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(PresetListCell), "Name");
        public static readonly BindableProperty TypeProperty = BindableProperty.Create("Type", typeof(ViewModel.Preset.TeaType), typeof(PresetListCell), ViewModel.Preset.TeaType.Black);
        public static readonly BindableProperty BaseSecsProperty = BindableProperty.Create("BaseSecs", typeof(ushort), typeof(PresetListCell), ushort.MinValue);
        public static readonly BindableProperty PlusSecsProperty = BindableProperty.Create("PlusSecs", typeof(ushort), typeof(PresetListCell), ushort.MinValue);
        public static readonly BindableProperty TempProperty = BindableProperty.Create("Temp", typeof(ushort), typeof(PresetListCell), ushort.MinValue);
        public static readonly BindableProperty MaxInfProperty = BindableProperty.Create("MaxInf", typeof(ushort), typeof(PresetListCell), ushort.MinValue);
        public static readonly BindableProperty AltNameProperty = BindableProperty.Create("AltName", typeof(string), typeof(PresetListCell), "AltName");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        
        public ViewModel.Preset.TeaType Type
        {
            get { return (ViewModel.Preset.TeaType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public ushort BaseSecs
        {
            get { return (ushort)GetValue(BaseSecsProperty); }
            set { SetValue(BaseSecsProperty, value); }
        }
        public ushort PlusSecs
        {
            get { return (ushort)GetValue(PlusSecsProperty); }
            set { SetValue(PlusSecsProperty, value); }
        }
        public ushort Temp
        {
            get { return (ushort)GetValue(TempProperty); }
            set { SetValue(TempProperty, value); }
        }
        public ushort MaxInf
        {
            get { return (ushort)GetValue(MaxInfProperty); }
            set { SetValue(MaxInfProperty, value); }
        }
        public string AltName
        {
            get { return (string)GetValue(AltNameProperty); }
            set { SetValue(AltNameProperty, value); }
        }

        public PresetListCell()
        {
            //instantiate each of our views
            teaColourBox = new BoxView();
            teaName = new Label();
            teaType = new Label();
            teaBaseSecs = new Label();
            teaPlusSecs = new Label();
            teaTemp = new Label();
            teaMaxInf = new Label();
            teaAltName = new Label();

            teaBaseSecs.MaxLines = 1;
            teaPlusSecs.MaxLines = 1;
            teaTemp.MaxLines = 1;
            teaMaxInf.MaxLines = 1;

            StackLayout cellWrapper = new StackLayout();
            Grid gridLayout = new Grid();
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) });

            //Set properties for desired design
            cellWrapper.BackgroundColor = Color.FromHex("#eee");
            teaAltName.HorizontalOptions = LayoutOptions.EndAndExpand;
            teaAltName.TextColor = Color.FromHex("503026");
            teaName.TextColor = Color.FromHex("503026");
            teaColourBox.WidthRequest = 8;
            teaColourBox.VerticalOptions = LayoutOptions.FillAndExpand;
            teaColourBox.HeightRequest = 50;

            //add views to the view hierarchy
            gridLayout.Children.Add(teaColourBox, 0, 0);
            gridLayout.Children.Add(teaName, 1, 0);
            gridLayout.Children.Add(teaType, 2, 0);
            gridLayout.Children.Add(teaBaseSecs, 3, 0);
            gridLayout.Children.Add(teaPlusSecs, 4, 0);
            gridLayout.Children.Add(teaTemp, 5, 0);
            gridLayout.Children.Add(teaMaxInf, 6, 0);
            gridLayout.Children.Add(teaAltName, 7, 0);

            cellWrapper.Children.Add(gridLayout);
            View = cellWrapper;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                Name = ((ViewModel.Preset)BindingContext).name;
                BaseSecs = ((ViewModel.Preset)BindingContext).baseseconds;
                PlusSecs = ((ViewModel.Preset)BindingContext).plusseconds;
                Temp = ((ViewModel.Preset)BindingContext).temp;
                MaxInf = ((ViewModel.Preset)BindingContext).maxinfusions;
                AltName = ((ViewModel.Preset)BindingContext).altname;
                Type = ((ViewModel.Preset)BindingContext).type;

                teaName.Text = Name;
                teaColourBox.Color = App.teaColoursDarkTxt[(int)Type];
                teaBaseSecs.Text = string.Format("{0}s", BaseSecs);
                teaPlusSecs.Text = string.Format("+{0}s", PlusSecs);
                teaTemp.Text = string.Format("{0}°C", Temp);
                teaMaxInf.Text = string.Format("{0} Infs", MaxInf);
                teaAltName.Text = AltName;
                teaType.Text = App.teaTypeNames[(int)Type];
            }
        }
    }
}
