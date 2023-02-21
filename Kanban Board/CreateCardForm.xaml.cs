using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kanban_Board
{
    /// <summary>
    /// Interaktionslogik für CreateCardForm.xaml
    /// </summary>
    public partial class CreateCardForm : Window
    {
        public CreateCardForm()
        {
          
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResultObject res = new ResultObject();
            Card card = new Card();
            CardRepo api = new CardRepo();

            
            res = Datahandler.PrepareCard(card);
            card = (Card) res.data;
          

            if (res.isSuccess)
            {
                

                if (string.IsNullOrEmpty(txtBoxTitle.Text) || string.IsNullOrEmpty(txtBoxDescription.Text) 
                    || string.IsNullOrEmpty(txtBoxModifier.Text) || string.IsNullOrEmpty(txtBoxActivity.Text))
                {
                    MessageBox.Show("Bitte Pflichtfeld anbgeben.");
                    return;
                }
                else
                {

                    card.title += txtBoxTitle.Text;
                    card.description += txtBoxDescription.Text;
                    card.modifier += txtBoxModifier.Text;
                    card.activity += txtBoxActivity.Text;
                    card.statusID = 1;
                    card.status = "To Do";

                    res = api.CreateCard(card);

                    if (res.isSuccess)
                    {
                        MessageBox.Show(res.message);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();

                    }
                }
            }
            
        }

        
    }
}
