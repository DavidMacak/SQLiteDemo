using System.Windows;

namespace SQLiteDemo
{
    /// <summary>
    /// Custom made dialog window because i found after x google pages that i can use MessageBox.Show() instead
    /// </summary>
    public partial class SmallDialog : Window
    {
        public SmallDialog()
        {
            InitializeComponent();
        }
        public SmallDialog(string message)
        {
            InitializeComponent();
            DialogTextBlock.Text = message;
        }
        public SmallDialog(string message, string windowTitle)
        {
            InitializeComponent();
            DialogTextBlock.Text = message;
            this.Title = windowTitle;
        }
        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
