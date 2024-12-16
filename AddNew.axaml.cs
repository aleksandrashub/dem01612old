using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demo1212shubenok.Models;
using Metsys.Bson;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;

namespace demo1212shubenok;

public partial class AddNew : Window
{
    public bool actCheck;
    public string path;
    public string filename;
    public string destpath;
    List<string> mans = Helper.context.Manufs.Select(x => x.Name).ToList();
    public Bitmap bitmap;
    public bool isnew;
    public List<Prod> currdops = new List<Prod>();
    public Prod prod = new Prod();
    public AddNew()
    {
        InitializeComponent();
        isnew = true;
        List<Prod> newdops = Helper.context.Prods.ToList();
        newdopList.ItemsSource = newdops;
        manCb.ItemsSource = mans;
        prod.IdPr = Helper.context.Prods.OrderBy(x => x.IdPr).Last().IdPr + 1;
        Helper.context.Prods.Add(prod);
            Helper.context.SaveChanges();

            

    }
    public AddNew(Prod pr)
    {
        InitializeComponent();
        isnew = false;
        prod.IdPr = pr.IdPr;
        prod.Name = pr.Name;
        prod.Descr = pr.Descr;
        prod.DopPrs  = pr.DopPrs;
        prod.IdManufNavigation = pr.IdManufNavigation;
        prod.IdManuf = pr.IdManuf;
        prod.Cost = pr.Cost;
        prod.IdActNavigation = pr.IdActNavigation;
        prod.IdAct = pr.IdAct;
        filename = pr.Image;
        nameTx.Text = pr.Name;
        costTx.Text = pr.Cost.ToString();
        descrTx.Text = pr.Descr;
        idTx.Text = pr.IdPr.ToString();
        manCb.ItemsSource = mans;
        manCb.SelectedItem = Helper.context.Manufs.Where(x => x.IdManuf == pr.IdManuf).First().Name;
        img.Source = pr.bitm;
        List<Prod> newdops = Helper.context.Prods.ToList();
        newdopList.ItemsSource = newdops;
        List<Prod> currdops = new List<Prod>(); ;
        foreach (int idpr in pr.DopPrs.Select(x => x.IdDop))
        {
            currdops.Add(Helper.context.Prods.Where(x => x.IdPr == idpr).First());
        }
        currdopList.ItemsSource= currdops;
        if (pr.IdAct == 1)
        {
            actCh.IsChecked = true;
        }
        else 
        {
            actCh.IsChecked = false;
        }

       
    }
    private void nazad_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow main = new MainWindow();
        main.Show();
        this.Close();
    }

    private async void img_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog op = new OpenFileDialog();

        var res = await op.ShowAsync(this);
        if (res == null) return;
        path = string.Join("", res);

        if (res != null)
        {
             bitmap = new Bitmap(path);
        }
        img.Source = bitmap;

        destpath = AppDomain.CurrentDomain.BaseDirectory + "Assets";
        filename = Path.GetFileName(path);
        File.Move(path, destpath);
    }

    private void Ok_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        bool check = true;
        if (isnew)
        {
            prod.IdPr = Helper.context.Prods.OrderBy(x => x.IdPr).Last().IdPr + 1;
        }

        if (nameTx.Text.Length > 0)
        {
            prod.Name = nameTx.Text;

        }
        else
        {
            check = false;
            var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Не заполнено поле наименования товара");
            ms.ShowAsync();
        }
        string costT = costTx.Text;
        float costCheck;
        if (float.TryParse(costT, out costCheck))
        {
            prod.Cost = costCheck;
        }
        else
        {
            check = false;
            var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Неверные данные в поле стоимости");
            ms.ShowAsync();
        }
        if (descrTx.Text.Length > 0)
        {
            prod.Descr = descrTx.Text;

        }
        else
        {
            check = false;
            var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Не заполнено поле описания товара");
            ms.ShowAsync();
        }
        if (manCb.SelectedIndex != -1)
        { 

            Manuf manuf = new Manuf();
            var man = manCb.SelectedItem as string;
            manuf = Helper.context.Manufs.Where(x => x.Name == man).FirstOrDefault();
            prod.IdManufNavigation = manuf;
            prod.IdManuf = manuf.IdManuf;

        }
        else
        {
            check = false;
            var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Не выбран производитель");
            ms.ShowAsync();
        }
        if (actCheck == true)
        {
            prod.IdActNavigation = Helper.context.Actives.Where(x => x.NameAct == "Да").First();
            prod.IdAct = Helper.context.Actives.Where(x => x.NameAct == "Да").First().IdAct;
        }
        else
        {
            prod.IdActNavigation = Helper.context.Actives.Where(x => x.NameAct == "Нет").First();
            prod.IdAct = Helper.context.Actives.Where(x => x.NameAct == "Нет").First().IdAct;
        }
        if (filename != null)
        { 
        prod.Image = filename;
        }
        if (check && isnew)
        { 
      
             Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().Name = prod.Name;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().Image = prod.Image;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().IdManufNavigation = prod.IdManufNavigation;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().IdManuf = prod.IdManuf;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().IdActNavigation = prod.IdActNavigation;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().IdAct = prod.IdAct;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().Cost = prod.Cost;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().Descr = prod.Descr;
            Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().DopPrs = prod.DopPrs;
            Helper.context.SaveChanges ();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }


    private void newDopListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        var item = newdopList.SelectedItem as Prod;
        if (item != prod)
        {
            DopPr doppr = new DopPr();
            doppr.IdPr = prod.IdPr;
            doppr.IdDop = Helper.context.Prods.Where(x => x.IdPr == item.IdPr).FirstOrDefault().IdPr;
            if (item != null)
            {
                prod.DopPrs.Add(doppr);
                currdopList.Items.Add(Helper.context.Prods.Where(x => x.IdPr == item.IdPr).FirstOrDefault());
            }




            /*

              public Bitmap bitm => Image != null && Image != "" ? new Bitmap($@"Assets\\{Image}") : null;
                public string color => IdAct == 2 ?
                    "Gray" : "White";
                public string activ => IdActNavigation.NameAct == "Нет" ?
                    "неактивен" : null;



             */

        }



    }

    private void CurrPrListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        var item = newdopList.SelectedItem as Prod;
        DopPr prodpr = new DopPr();
        prodpr.IdPr = prod.IdPr;
        prodpr.IdDop = Helper.context.Prods.Where(x => x.IdPr == item.IdPr).First().IdPr;
        prod.DopPrs.Remove(prodpr);
        Helper.context.Prods.Where(x => x.IdPr == prod.IdPr).First().DopPrs = prod.DopPrs;
        Helper.context.SaveChanges();
        currdops.Clear();
        foreach (int idpr in prod.DopPrs.Select(x => x.IdDop))
        {
            currdops.Add(Helper.context.Prods.Where(x => x.IdPr == idpr).First());
        }
        currdopList.ItemsSource= null;

        currdopList.ItemsSource = currdops.ToList();

    }

    private void CheckBox_IsCheckedChanged_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        if (actCh.IsChecked == true)
        {
            actCheck = true;

        }
        else
        {
            actCheck = false;
        }
       
    }
}