

namespace MicroNutritionist.Common.Behaviors
{
    public class DoubleValidationBehavior : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = double.TryParse(args.NewTextValue, out var value);

                if (!isValid)
                {
                    ((Entry)sender).Text = args.OldTextValue;
                }
            }
        }
    }
}
