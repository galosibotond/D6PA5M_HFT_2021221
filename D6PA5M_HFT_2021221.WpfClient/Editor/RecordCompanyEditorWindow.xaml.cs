using System.Windows;

namespace D6PA5M_HFT_2021221.WpfClient
{
    /// <summary>
    /// Interaction logic for RecordCompanyEditorWindow.xaml
    /// </summary>
    public partial class RecordCompanyEditorWindow : Window
    {
        public RecordCompanyEditorWindow()
        {
            InitializeComponent();
        }

        private void albumEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            AlbumEditorWindow albumEditorWindow = new AlbumEditorWindow();
            albumEditorWindow.Show();
        }

        private void artistEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ArtistEditorWindow artistEditorWindow = new ArtistEditorWindow();
            artistEditorWindow.Show();
        }

        private void genreEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            GenreEditorWindow genreEditorWindow = new GenreEditorWindow();
            genreEditorWindow.Show();
        }
    }
}
