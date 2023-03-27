using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValuteConvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public string[] VallName = { "EUR", "RUB", "USD", "AUD", "AZN", "GBP", "AMD" , "BYN" , "GEL", "CNY", "CZK" };
        public string[] VallId = { "Евро", "Рубли", "Доллар США", "Австралийский доллар", "Азербайджанский манат" , "Фунт стерлингов Соединенного королевства" , "Армянских драмов", "Белорусский рубль", "Грузинский лари", "Китайский юань" , "Чешских крон" };
        
        public MainWindow()
        {
            InitializeComponent();
            string[] VallIdPlusName = new string[VallName.Length];
            
            for(int i = 0; i < VallIdPlusName.Length; i++)
            {
                VallIdPlusName[i] = $"{VallId[i]} ({VallName[i]})";
            }
            ToComboBox.ItemsSource= VallIdPlusName;
            FromComboBox.ItemsSource= VallIdPlusName;
            FromComboBox.SelectedIndex=0;
            ToComboBox.SelectedIndex=0;
            textBoxFrom.IsEnabled=false;
          //  comboboxvalute.itemssource = vallid;
          //  comboboxvalute1.itemssource = vallid;
        }
        public int IndexOut, IndexIn;

        

        private void DragWindow(object sender, MouseButtonEventArgs e) //Метод для перемещения окна
        {

            try
            {
                DragMove();
            }
            catch (Exception ex) { }
        }

        private void closeApp(object sender, MouseButtonEventArgs e)//Метод для закрытия окна
        {

            try
            {
                Close();
            }
            catch (Exception ex) { }
        }

        private void RollUp(object sender, MouseButtonEventArgs e)//метод для сворачивания окна
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex) { }
        }
        public string result;
        public string GetValute(string Input, string Output,string amount)
        {
 
            var client = new RestClient(@$"https://api.apilayer.com/exchangerates_data/convert?to={Input}&from={Output}&amount={amount}");
            var request = new RestRequest();
            request.AddHeader("apikey", "iWXKEEjwEFZljmKAjFvEsLhcWLO0SRPj");
            RestResponse response = client.Execute(request);
            Rootobject rootobject = JsonConvert.DeserializeObject<Rootobject>(response.Content);
            result =  rootobject.result.ToString();
            return result;
        }

        private void FromComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IndexIn = FromComboBox.SelectedIndex;
        }

        private void ToComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IndexOut = ToComboBox.SelectedIndex;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string amount = textBoxTo.Text;
            textBoxFrom.Text = GetValute(VallName[IndexOut],  VallName[IndexIn], amount);
        }

        //private void comboBoxValute1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    IndexOut = ToComboBox.SelectedIndex;
        //}

        //private void comboBoxValute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //   IndexIn = FromComboBox.SelectedIndex;
        //}
    }
}
