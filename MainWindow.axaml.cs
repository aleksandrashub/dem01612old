using Avalonia.Controls;
using demo1212shubenok.Models;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace demo1212shubenok
{
    public partial class MainWindow : Window
    {

        List<string> manufs = Helper.context.Manufs.Select(x => x.Name).ToList();
        List<Prod> prods = Helper.context.Prods.Include(x => x.IdManufNavigation).Include(x => x.IdActNavigation).ToList();
        public MainWindow()
        {
            InitializeComponent();
            List<string> manufs = new List<string>();
            manufs.Add("Все элементы");
            List<string> mans = Helper.context.Manufs.Select(x => x.Name).ToList();
            manufs.AddRange(mans);
            manufCb.ItemsSource = manufs;
            update();
           // OpenFileDialog op = new OpenFileDialog();
        }

        public void update()
        {
            
            var list = Helper.context.Prods.Include(x => x.IdManufNavigation).Include(x => x.IdActNavigation).ToList();

            if (manufCb.SelectedItem != null && manufCb.SelectedItem != "Все элементы")
            { 
                var man = manufCb.SelectedItem as string;
                Manuf manuf = Helper.context.Manufs.Where(x => x.Name == man).First();
                list = prods.Where(x => x.IdManuf == manuf.IdManuf).ToList();   
                
            }


            switch (costCb.SelectedIndex)
            {
                case 0:
                    list = list.OrderByDescending(x => x.Cost).ToList();
                    break;

                case 1:
                    list = list.OrderBy(x => x.Cost).ToList();    
                    break;
                default: break;
            }


            string searchText = textbox.Text ?? "";
            int count = searchText.Split(' ').Length;
            string[] values = new string[count];
            values = searchText.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in values) 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    list = list.Where(x => x.Name.Contains(value) || x.Descr.Contains(value)).ToList();
                }
                else
                { 
                }
            
            }
            listbox.ItemsSource = list;


        }

        private void TextBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            update();
        }

        private void sort_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            update();
        }

        private void filter_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            update();
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddNew add = new AddNew();
            add.Show();
            this.Close();


        }

        private void delButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int ind = (int)((sender as Button)!).Tag!;
            Prod prod=Helper.context.Prods.Where(x => x.IdPr == ind).First();
            List<DopPr> dopprs = Helper.context.DopPrs.Where(x => x.IdPr == ind).ToList();
            if (Helper.context.ProdazhaPrs.Where(x => x.IdPr == ind).Count() > 0)
            {
                var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Нельзя удалить данный товар, сначала удалите заказы с ним");
                ms.ShowAsync();
            }
            else
            {
                Helper.context.DopPrs.RemoveRange(dopprs);
                Helper.context.SaveChanges();
                Helper.context.Prods.Remove(prod);
                Helper.context.SaveChanges();
                update();

            }
            

        }
    }
}