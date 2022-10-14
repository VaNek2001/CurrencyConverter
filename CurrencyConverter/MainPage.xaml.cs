using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace CurrencyConverter;

public partial class MainPage : ContentPage
{
    public string ChCode;

    private static int size = 0;

    public int index = 0;

    public double[] arrValue;

    public MainPage()
    {
        InitializeComponent();

        String URLString = "https://www.cbr-xml-daily.ru/daily_utf8.xml";
        XmlTextReader reader = new XmlTextReader(URLString);

        Curs_1.Items.Add("Российский рубль (RUB)");
        Curs_2.Items.Add("Российский рубль (RUB)");

        size++;

        while (reader.Read())
        {
            if (reader.IsStartElement() && reader.Name == "CharCode")
            {
                reader.Read();
                ChCode = reader.Value;
            }

            if (reader.IsStartElement() && reader.Name == "Name")
            {
                reader.Read();
                Curs_1.Items.Add(reader.Value + " (" + ChCode + ")");
                Curs_2.Items.Add(reader.Value + " (" + ChCode + ")");

                size++;
            }
        }

        XmlTextReader reader_2 = new XmlTextReader(URLString);

        arrValue = new double[size+1];

        arrValue[index] = 1;

        index++;

        while (reader_2.Read())
        {
            if (reader_2.IsStartElement() && reader_2.Name == "Value")
            {
                reader_2.Read();
                arrValue[index] = double.Parse(reader_2.Value.Replace('.',','));
                index++;
            }
        }

        LabelDate.Text = "Курс на " + DP.Date.ToString("d");

        Curs_1.SelectedIndexChanged -= PickerSelectedIndexChanged_1;
        Curs_1.SelectedIndex = 11;
        Curs_1.SelectedIndexChanged += PickerSelectedIndexChanged_1;
        Curs_2.SelectedIndexChanged -= PickerSelectedIndexChanged_2;
        Curs_2.SelectedIndex = 0;
        Curs_2.SelectedIndexChanged += PickerSelectedIndexChanged_2;
        EntryNote_1.Text = "1";
    }

    void DateSelected(System.Object sender, Microsoft.Maui.Controls.DateChangedEventArgs e)
    {
        LabelDate.Text = "Курс на " + e.NewDate.ToString("d");
    }


    void EntryTextChanged_1(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (EntryNote_1.Text == "" || Regex.IsMatch(EntryNote_1.Text, @"^-*[0-9,\.]+$"))
        {
            if (EntryNote_1.Text != "")
            {
                double currCurs = arrValue[Curs_1.SelectedIndex] / arrValue[Curs_2.SelectedIndex];
                EntryNote_2.TextChanged -= EntryTextChanged_2;
                EntryNote_2.Text = Math.Round((currCurs * double.Parse(EntryNote_1.Text)), 3).ToString();
                EntryNote_2.TextChanged += EntryTextChanged_2;
            }
            else
            {
                EntryNote_1.Text = "";
                EntryNote_2.Text = "";
            }
        }
    }

    void EntryTextChanged_2(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (EntryNote_1.Text == "" || Regex.IsMatch(EntryNote_1.Text, @"^-*[0-9,\.]+$"))
        {
            if (EntryNote_2.Text != "")
            {
                double currCurs = arrValue[Curs_2.SelectedIndex] / arrValue[Curs_1.SelectedIndex];
                EntryNote_1.TextChanged -= EntryTextChanged_1;
                EntryNote_1.Text = Math.Round((currCurs * double.Parse(EntryNote_2.Text)), 3).ToString();
                EntryNote_1.TextChanged += EntryTextChanged_1;
            }
            else
            {
                EntryNote_1.Text = "";
                EntryNote_2.Text = "";
            }
        }
    }

    void PickerSelectedIndexChanged_1(System.Object sender, System.EventArgs e)
    {
        if (EntryNote_2.Text != "" && EntryNote_1.Text != "")
        {
            double currCurs = arrValue[Curs_2.SelectedIndex] / arrValue[Curs_1.SelectedIndex];
            EntryNote_1.Text = Math.Round((currCurs * double.Parse(EntryNote_2.Text)), 3).ToString();
        }
    }

    void PickerSelectedIndexChanged_2(System.Object sender, System.EventArgs e)
    {
        if (EntryNote_1.Text != "" && EntryNote_2.Text != "")
        {
            double currCurs = arrValue[Curs_1.SelectedIndex] / arrValue[Curs_2.SelectedIndex];
            EntryNote_2.Text = Math.Round((currCurs * double.Parse(EntryNote_1.Text)), 3).ToString();
        }
    }
}