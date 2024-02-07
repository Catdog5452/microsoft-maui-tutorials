namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        string translatedNumber;

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";
            }
        }

        async void OnCall(object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                    {
                        PhoneDialer.Default.Open(translatedNumber);
                    }
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dail", "Phone number was not valid.", "Ok");
                }
                catch (Exception)
                {
                    await DisplayAlert("Unable to dail", "Phone dialing failed.", "Ok");
                }
            }
        }
    }

}
