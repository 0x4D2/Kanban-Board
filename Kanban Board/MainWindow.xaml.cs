using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;



namespace Kanban_Board
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>



    public partial class MainWindow : Window
    {
        Point LB1StartMousePos;
        Point LB2StartMousePos;
        Card preCard = new Card();


        public MainWindow()
        {
            InitializeComponent();

            CardRepo api = new CardRepo();
            ResultObject res = new ResultObject();

            res = api.GetCards();

            if (res.isSuccess)
            {

                foreach (var result in (List<Card>)res.data)

                    if (result.statusID == 1)
                    {
                        LB1.Items.Add(result);
                    }
                    else if (result.statusID == 2)
                    {
                        LB2.Items.Add(result);
                    }
                    else if (result.statusID == 3)
                    {
                        LB3.Items.Add(result);
                    }
                    else
                    {
                        LB4.Items.Add(result);
                    }

            }

        }
        private void LB1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {

                    Card selectedItem = (Card)LB1.SelectedItem;

                    LB1.Items.Remove(selectedItem);


                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);
                    ListBox hovedListBox = (ListBox)sender;

                    if (selectedItem == null)
                    {
                        hovedListBox.Items.Add(selectedItem);
                    }


                    dynamic Booth = LB1.SelectedItem as dynamic;
                    if (sender is ListBox && e.LeftButton == MouseButtonState.Pressed)
                    {
                        DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Move);
                    }
                }
                catch { }
            }
        }
        private void LB2_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)

            {
                try
                {

                    Card selectedItem = (Card)LB2.SelectedItem;

                    LB2.Items.Remove(selectedItem);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);
                    ListBox hovedListBox = (ListBox)sender;

                    if (selectedItem == null)
                    {
                        hovedListBox.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }
        private void LB3_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    Card selectedItem = (Card)LB3.SelectedItem;

                    LB3.Items.Remove(selectedItem);

                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);


                    ListBox hovedListBox = (ListBox)sender;

                    if (selectedItem == null)
                    {
                        DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Move);
                        hovedListBox.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }
        private void LB4_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    Card selectedItem = (Card)LB4.SelectedItem;

                    LB4.Items.Remove(selectedItem);

                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);


                    ListBox hovedListBox = (ListBox)sender;

                    if (selectedItem == null)
                    {
                        hovedListBox.Items.Add(selectedItem);

                    }
                }
                catch { }
            }
        }
        private void LB1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LB1StartMousePos = e.GetPosition(null);
        }
        private void LB2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LB2StartMousePos = e.GetPosition(null);
        }
        private void LB3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LB1StartMousePos = e.GetPosition(null);
        }
        private void LB4_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LB1StartMousePos = e.GetPosition(null);
        }
        private void LB3_Drop(object sender, DragEventArgs e)
        {
            ListBox hovedListBox = (ListBox)sender;
            ResultObject res = new ResultObject();
            CardRepo repo = new CardRepo();

            if (e.Data.GetData(DataFormats.FileDrop) is Card listItem)
            {
                Point point = LB3.TranslatePoint(e.GetPosition(LB3), LB3);
                int index = GetIndexFromPoint(LB3, point);
                if (index < 0)
                {
                    hovedListBox.Items.Add(listItem);
                    res = repo.UpdateCard(listItem, 3, false);
                }
                else
                {
                    LB3.Items.Insert(index, listItem);
                    res = repo.UpdateCard(listItem, 3, false);
                }
                RemoveItemDesign(LB3, LB3.Items.Count);
            }
        }
        private void LB4_Drop(object sender, DragEventArgs e)
        {
            ListBox hovedListBox = (ListBox)sender;
            ResultObject res = new ResultObject();
            CardRepo repo = new CardRepo();

            if (e.Data.GetData(DataFormats.FileDrop) is Card listItem)
            {
                Point point = LB4.TranslatePoint(e.GetPosition(LB4), LB4);
                int index = GetIndexFromPoint(LB4, point);
                if (index < 0)
                {
                    hovedListBox.Items.Add(listItem);
                    res = repo.UpdateCard(listItem, 4, false);
                }
                else
                {
                    LB4.Items.Insert(index, listItem);
                    res = repo.UpdateCard(listItem, 4, false);
                }
                RemoveItemDesign(LB4, LB4.Items.Count);
            }

        }
        private void LB2_Drop(object sender, DragEventArgs e)
        {

            ListBox hovedListBox = (ListBox)sender;
            ResultObject res = new ResultObject();
            CardRepo repo = new CardRepo();

            if (e.Data.GetData(DataFormats.FileDrop) is Card listItem)
            {
                Point point = LB2.TranslatePoint(e.GetPosition(LB2), LB2);
                int index = GetIndexFromPoint(LB2, point);

                if (index < 0)
                {
                    hovedListBox.Items.Add(listItem);
                    res = repo.UpdateCard(listItem, 2, false);
                }
                else
                {
                    LB2.Items.Insert(index, listItem);
                    res = repo.UpdateCard(listItem, 2, false);
                }
                RemoveItemDesign(LB2, LB2.Items.Count);

            }
        }
        private void LB1_Drop(object sender, DragEventArgs e)
        {
            ListBox hovedListBox = (ListBox)sender;
            ResultObject res = new ResultObject();
            CardRepo repo = new CardRepo();

            if (e.Data.GetData(DataFormats.FileDrop) is Card listItem)
            {
                Point point = LB1.TranslatePoint(e.GetPosition(LB1), LB1);
                int index = GetIndexFromPoint(LB1, point);
                if (index < 0)
                {
                  
                    hovedListBox.Items.Add(listItem);
                    res = repo.UpdateCard(listItem, 1, false);

                }
                else
                {
                    LB1.Items.Insert(index, listItem);
                    res = repo.UpdateCard(listItem, 1, false);
                }

                RemoveItemDesign(LB1, LB1.Items.Count);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            CreateCardForm form2 = new CreateCardForm();
            form2.Show();
            this.Close();

        }
        private void btnDoneListBoxItemRight_Click(object sender, RoutedEventArgs e)
        {
            if (LB4.SelectedIndex != -1)
            {
                object selectedItem = LB4.SelectedItem;
                LB4.Items.RemoveAt(LB4.SelectedIndex);

                CardRepo repo = new CardRepo();
                ResultObject res = new ResultObject();

                res = repo.UpdateCard((Card)selectedItem, 4, true);
                if (res.isSuccess)
                {
                    MessageBox.Show($"Card moved to Archive");
                }
                else
                {
                    MessageBox.Show(res.exceptionMessage);
                }

            }
        }
        private void LB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Card selectedObject = LB1.SelectedItem as Card;

            if (selectedObject != null)
            {
                txtBoxTitle.Text = selectedObject.title;
                txtBoxDescription.Text = selectedObject.description;
                txtBoxCreateTimestamp.Text = selectedObject.createTimestamp.ToString();
            }
        }
        private void LB2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Card selectedObject = LB2.SelectedItem as Card;

            if (selectedObject != null)
            {
                txtBoxTitle.Text = selectedObject.title;
                txtBoxDescription.Text = selectedObject.description;
                txtBoxCreateTimestamp.Text = selectedObject.createTimestamp.ToString();
            }
        }
        private void LB3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Card selectedObject = LB3.SelectedItem as Card;

            if (selectedObject != null)
            {
                txtBoxTitle.Text = selectedObject.title;
                txtBoxDescription.Text = selectedObject.description;
                txtBoxCreateTimestamp.Text = selectedObject.createTimestamp.ToString();
            }
        }
        private void LB4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Card selectedObject = LB4.SelectedItem as Card;

            if (selectedObject != null)
            {
                txtBoxTitle.Text = selectedObject.title;
                txtBoxDescription.Text = selectedObject.description;
                txtBoxCreateTimestamp.Text = selectedObject.createTimestamp.ToString();
            }
        }
        private int GetIndexFromPoint(ListBox listBox, Point point)
        {
            UIElement element = listBox.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = listBox.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == listBox) return -1;
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return listBox.Items.IndexOf(data);
                }
            }
            return -1;
        }

        private void LB1_DragOver(object sender, DragEventArgs e)
        {
            var listBox = sender as ListBox;
            var mousePos = e.GetPosition(LB1);
            var listBoxItem = listBox.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
            var frameworkElement = VisualTreeHelper.GetChild(listBoxItem, 0) as FrameworkElement;
            var itemHeight = frameworkElement.ActualHeight;
            var itemsCount = listBox.Items.Count;

            for (int i = 0; i < itemsCount; i++)
            {
                var itemContainer = listBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (itemContainer != null)
                {
                    var itemTop = itemContainer.TranslatePoint(new Point(0, 0), listBox).Y;
                    var itemBottom = itemTop + itemHeight;

                    if (mousePos.Y >= itemTop && mousePos.Y < itemBottom)
                    {
                        var margin = itemContainer.Margin;
                        margin.Bottom = itemHeight / 2;
                        margin.Top = itemHeight / 2;
                        itemContainer.Margin = margin;
                    }
                    else
                    {
                        itemContainer.Margin = new Thickness(0);
                    }
                }
            }
        }
  
        private void LB1_DragLeave(object sender, DragEventArgs e)
        {
            var listBox = sender as ListBox;
            var itemsCount = listBox.Items.Count;

            RemoveItemDesign(listBox, itemsCount);

        }
        private  static void RemoveItemDesign(ListBox listBox, int listBoxItemCount)
        {

            var itemsCount = listBoxItemCount;

            for (int i = 0; i < itemsCount; i++)
            {
                var itemContainer = listBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (itemContainer != null)
                {
                    itemContainer.Margin = new Thickness(0);
                }
            }

        }
       

        private void LB2_DragLeave(object sender, DragEventArgs e)
        {
            var listBox = sender as ListBox;
            var itemsCount = listBox.Items.Count;

            RemoveItemDesign(listBox, itemsCount);

        }

        private  void LB2_DragOver(object sender, DragEventArgs e)
        {
           

            var listBox = sender as ListBox;
            var itemsCount = listBox.Items.Count;

            if(itemsCount != 0)
            {
                var mousePos = e.GetPosition(LB2);
                var listBoxItem = listBox.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                var frameworkElement = VisualTreeHelper.GetChild(listBoxItem, 0) as FrameworkElement;
                var itemHeight = frameworkElement.ActualHeight;


                for (int i = 0; i < itemsCount; i++)
                {
                    var itemContainer = listBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (itemContainer != null)
                    {
                        var itemTop = itemContainer.TranslatePoint(new Point(0, 0), listBox).Y;
                        var itemBottom = itemTop + itemHeight;

                        if (mousePos.Y >= itemTop && mousePos.Y < itemBottom)
                        {
                            var margin = itemContainer.Margin;
                            margin.Bottom = itemHeight / 2;
                            margin.Top = itemHeight / 2;
                            itemContainer.Margin = margin;
                        }
                        else
                        {
                            itemContainer.Margin = new Thickness(0);
                        }
                    }
                }

            }
        }

        private void LB3_DragLeave(object sender, DragEventArgs e)
        {
            var listBox = sender as ListBox;
            var itemsCount = listBox.Items.Count;

            RemoveItemDesign(listBox, itemsCount);

        }

        private void LB3_DragOver(object sender, DragEventArgs e)
        {


            var listBox = sender as ListBox;
            var itemsCount = listBox.Items.Count;

            if (itemsCount != 0)
            {
                var mousePos = e.GetPosition(LB3);
                var listBoxItem = listBox.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                var frameworkElement = VisualTreeHelper.GetChild(listBoxItem, 0) as FrameworkElement;
                var itemHeight = frameworkElement.ActualHeight;


                for (int i = 0; i < itemsCount; i++)
                {
                    var itemContainer = listBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (itemContainer != null)
                    {
                        var itemTop = itemContainer.TranslatePoint(new Point(0, 0), listBox).Y;
                        var itemBottom = itemTop + itemHeight;

                        if (mousePos.Y >= itemTop && mousePos.Y < itemBottom)
                        {
                            var margin = itemContainer.Margin;
                            margin.Bottom = itemHeight / 2;
                            margin.Top = itemHeight / 2;
                            itemContainer.Margin = margin;
                        }
                        else
                        {
                            itemContainer.Margin = new Thickness(0);
                        }
                    }
                }

            }



        }

        private void LB4_DragLeave(object sender, DragEventArgs e)
        {
            var listBox = sender as ListBox;
            var itemsCount = listBox.Items.Count;

            RemoveItemDesign(listBox, itemsCount);

        }

        private void LB4_DragOver(object sender, DragEventArgs e)
        {


            var listBox = sender as ListBox;
            var itemsCount = listBox.Items.Count;

            if (itemsCount != 0)
            {
                var mousePos = e.GetPosition(LB4);
                var listBoxItem = listBox.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                var frameworkElement = VisualTreeHelper.GetChild(listBoxItem, 0) as FrameworkElement;
                var itemHeight = frameworkElement.ActualHeight;


                for (int i = 0; i < itemsCount; i++)
                {
                    var itemContainer = listBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (itemContainer != null)
                    {
                        var itemTop = itemContainer.TranslatePoint(new Point(0, 0), listBox).Y;
                        var itemBottom = itemTop + itemHeight;

                        if (mousePos.Y >= itemTop && mousePos.Y < itemBottom)
                        {
                            var margin = itemContainer.Margin;
                            margin.Bottom = itemHeight / 2;
                            margin.Top = itemHeight / 2;
                            itemContainer.Margin = margin;
                        }
                        else
                        {
                            itemContainer.Margin = new Thickness(0);
                        }
                    }
                }

            }


        }
    }
}



