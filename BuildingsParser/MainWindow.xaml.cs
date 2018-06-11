using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
using BuildingsParser.FilesService;
using BuildingsParser.Models;
using BuildingsParser.ViewModel;

namespace BuildingsParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Container
    {
        public List<ParserItem> Items = new List<ParserItem>();
    }
    public partial class MainWindow : Window
    {
        Container Items = new Container();

        DefaultDialogService dialogService = new DefaultDialogService();
        XMLSerialization<Container> fileService = new XMLSerialization<Container>();
        
        Parser Parser = new Parser();
        private int Stan = 0;
        public MainWindow()
        {
            InitializeComponent();
            
            Items.Items = Parser.Pars();
            ParseItems();
        }


        public void ParseItems()
        {
            foreach (ParserItem item in Items.Items)
            {
                listBox1.Items.Add(item.Name + " " + item.Description + " " + item.Region + " " + item.Area + " " +
                                   item.Furniture + " " + item.District + " " + item.Terrace + " " + item.Pool + " " +
                                   item.LotNumber + " " + item.Bedrooms + " " + item.Price + " ");
            }
        }

        #region Buttons

        private void Button0_OnClick(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            if (Stan == 0)
            {
                SortArea();
            }
            else
            {
                SortAreaDesc();
            }
            ParseItems();
        }
        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {

            listBox1.Items.Clear();
            if (Stan == 0)
            {
                SortBedrooms();
            }
            else
            {
                SortBedroomsDesc();
            }
            ParseItems();
        }
        private void Button2_OnClick(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            if (Stan == 0)
            {
                SortPrice();
            }
            else
            {
                SortPriceDesc();
            }
            ParseItems();
        }

        #endregion

        #region Sorts
        public void SortArea()
        {
            Stan = 1;
            Items.Items = Items.Items.OrderBy(i => i.Area).ToList();
        }
        public void SortAreaDesc()
        {
            Items.Items = Items.Items.OrderByDescending(i => i.Area).ToList();
            Stan = 0;
        }
        public void SortBedrooms()
        {
            Stan = 1;
            Items.Items = Items.Items.OrderBy(i => i.Bedrooms).ToList();
        }
        public void SortBedroomsDesc()
        {
            Items.Items = Items.Items.OrderByDescending(i => i.Bedrooms).ToList();
            Stan = 0;
        }
        public void SortPrice()
        {
            Stan = 1;
            Items.Items = Items.Items.OrderBy(i => i.Price).ToList();
        }
        public void SortPriceDesc()
        {
            Items.Items = Items.Items.OrderByDescending(i => i.Price).ToList();
            Stan = 0;

        }
        #endregion

        #region Menu Buttons

        private void MenuItem1_OnClick(object sender, RoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog())
            {
                listBox1.Items.Clear();
                fileService.GetModel(dialogService.FilePath);
                Items = fileService._Model;
                ParseItems();
            }
        }

        private void MenuItem2_OnClick(object sender, RoutedEventArgs e)
        {
            if (dialogService.SaveFileDialog())
            {
                fileService._Model = Items;
                fileService.CreateModel(dialogService.FilePath);
            }
        }

        #endregion
    }
}
