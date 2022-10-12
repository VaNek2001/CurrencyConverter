namespace CurrencyConverter;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
        

		LabelDate.Text = "Курс на " + DP.Date.ToString("d");
	}

    private void GetValCurs()
    {
        
    }

    void DateSelected(System.Object sender, Microsoft.Maui.Controls.DateChangedEventArgs e)
    {
		LabelDate.Text = "Курс на " + e.NewDate.ToString("d");
    }

    void PickerSelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
    }

    void EntryTextChanged_1(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }

    void EntryTextChanged_2(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }
}


