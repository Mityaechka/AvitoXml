using AvitoXml.Wpf.Models;
using ExcelDataReader;
using LinqToExcel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AvitoXml.Wpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для XlsImportWindow.xaml
    /// </summary>
    public partial class XlsImportWindow : Window
    {
        public ObservableCollection<ImportParametr> ImportParametrs { get; set; } = new ObservableCollection<ImportParametr>();
        public ObservableCollection<AvitoParametr> AvitoParametrs { get; set; } = new ObservableCollection<AvitoParametr>() {
            new AvitoParametr("Id","Уникальный идентификатор объявления"),
            new AvitoParametr("DateBegin","Дата и время начала размещения объявления"),
            new AvitoParametr("DateEnd","ата и время, до которых объявление актуально"),
            new AvitoParametr("ListingFee","Вариант платного размещения"),
            new AvitoParametr("AdStatus","Платная услуга, которую нужно применить к объявлению"),
            new AvitoParametr("AvitoId","Номер объявления на Авито — целое число"),
            new AvitoParametr("AllowEmail","Возможность написать сообщение по объявлению через сайт"),
            new AvitoParametr("ManagerName","Имя менеджера"),
            new AvitoParametr("ContactPhone","Контактный телефон"),
            new AvitoParametr("Address","Полный адрес объекта"),
            new AvitoParametr("Category","Категория товара"),
            new AvitoParametr("GoodsType","Вид товара"),
            new AvitoParametr("AdType","Вид объявления"),
            new AvitoParametr("Title","Название объявления"),
            new AvitoParametr("Description","Текстовое описание объявления"),
            new AvitoParametr("Price","Цена"),
            new AvitoParametr("Condition","Состояние вещи"),
            new AvitoParametr("Images","Фотографии "),
            new AvitoParametr("VideoURL","Видео c YouTube "),
            new AvitoParametr("Subway","Ближайшая станция метро")
        };
        public ObservableCollection<XlsParametr> XlsParametrs { get; private set; }
        public string FileName;
        AdBlank AdBlank { get; set; }
        public XlsImportWindow(AdBlank adBlank)
        {
            InitializeComponent();
            AdBlank = adBlank;
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы Excell (*.xls)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                FileNameLabel.Content = FileName;
                GetXlsParametrs();
            }
        }
        void GetXlsParametrs()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IExcelDataReader reader;
                reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);


                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                var dataSet = reader.AsDataSet(conf);
                var table = dataSet.Tables[0];
                int index = 0;
                XlsParametrs = new ObservableCollection<XlsParametr>();
                foreach (var c in table.Columns)
                {
                    XlsParametrs.Add(new XlsParametr(index, c.ToString()));
                    index++;
                }
                Parametrs.Children.RemoveRange(0, Parametrs.Children.Count);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            DockPanel panel = new DockPanel() { HorizontalAlignment = HorizontalAlignment.Stretch };

            ComboBox comboBox1 = new ComboBox() { ItemsSource = AvitoParametrs, DisplayMemberPath = "AvitoRu", MinWidth = 150 };
            ComboBox comboBox2 = new ComboBox() { ItemsSource = XlsParametrs, DisplayMemberPath = "XlsHeader", MinWidth = 150 };
            Button btn = new Button() { Content = "Удалить", HorizontalAlignment = HorizontalAlignment.Right };
            btn.Click += new RoutedEventHandler(delegate (Object o, RoutedEventArgs a)
            {
                Parametrs.Children.Remove(panel);
            });
            DockPanel.SetDock(btn, Dock.Right);
            panel.Children.Add(comboBox1);
            panel.Children.Add(comboBox2);
            panel.Children.Add(btn);
            Parametrs.Children.Add(panel);
            Parametrs.UpdateLayout();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Ad> ads = new List<Ad>();
            using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IExcelDataReader reader;
                reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                var dataSet = reader.AsDataSet(conf);
                var table = dataSet.Tables[0];
                var parseParametrs = new List<ImportParametr>();
                foreach (DockPanel panel in Parametrs.Children)
                {
                    parseParametrs.Add(new ImportParametr()
                    {
                        AvitoParametr = (AvitoParametr)((ComboBox)panel.Children[0]).SelectedItem,
                        XlsParametr = (XlsParametr)((ComboBox)panel.Children[1]).SelectedItem
                    });
                }

                foreach (DataRow r in table.Rows)
                {
                    Ad ad = new Ad(AdBlank);
                    foreach (var parametr in parseParametrs)
                    {
                        //if (string.IsNullOrEmpty(Convert.ToString(r.ItemArray[parametr.XlsParametr.XlsIndex]).Trim()))
                        //    continue;
                        if (!ad.Parametrs.ContainsKey(parametr.AvitoParametr.AvitoXml))
                            ad.Parametrs.Add(parametr.AvitoParametr.AvitoXml, r.ItemArray[parametr.XlsParametr.XlsIndex]);
                        else
                            ad.Parametrs[parametr.AvitoParametr.AvitoXml] = r.ItemArray[parametr.XlsParametr.XlsIndex];
                    }
                    ads.Add(ad);
                }
                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, Ad.Xml(ads));
            }
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


/*
 Grid Grid = new Grid();
            
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Star) });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Star) });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
            ComboBox comboBox1 = new ComboBox() { ItemsSource = AvitoParametrs, DisplayMemberPath = "AvitoRu" };
            ComboBox comboBox2 = new ComboBox() { ItemsSource = XlsParametrs, DisplayMemberPath = "XlsHeader" };
            Button btn = new Button();
            btn.Click += new RoutedEventHandler(delegate (Object o, RoutedEventArgs a)
            {
                Parametrs.Children.Remove(Grid);
            });
            Grid.SetColumn(comboBox1, 0);
            Grid.SetColumn(comboBox2, 1);
            Grid.SetColumn(btn, 2);
            Grid.SetRow(comboBox1, 0);
            Grid.SetRow(comboBox2, 0);
            Grid.SetRow(btn, 0);
            var b = new Button();
            Grid.SetColumn(b, 0);
            Grid.SetRow(b, 0);
            Parametrs.Children.Add(Grid);
            Parametrs.UpdateLayout();
 */
