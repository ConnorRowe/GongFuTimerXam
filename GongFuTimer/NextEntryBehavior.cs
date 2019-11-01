using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GongFuTimer
{
    class NextEntryBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty NextEntryProperty = BindableProperty.Create(nameof(NextView), typeof(Entry), typeof(Entry), defaultBindingMode: BindingMode.OneTime, defaultValue: null);

        public View NextView
        {
            get => (View)GetValue(NextEntryProperty);
            set => SetValue(NextEntryProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Completed += Bindable_Completed;
            base.OnAttachedTo(bindable);
        }

        private void Bindable_Completed(object sender, EventArgs e)
        {
            if (NextView != null)
            {
                NextView.Focus();
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Completed -= Bindable_Completed;
            base.OnDetachingFrom(bindable);
        }
    }
}
