using System.Windows;

namespace D6PA5M_HFT_2021221.WpfClient
{
    /// <summary>
    /// Interaction logic for ArtistEditorWindow.xaml
    /// </summary>
    public partial class ArtistEditorWindow : Window
    {
        public ArtistEditorWindow()
        {
            InitializeComponent();
        }

        private void albumEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            AlbumEditorWindow albumEditorWindow = new AlbumEditorWindow();
            albumEditorWindow.Show();
        }
        private void genreEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            GenreEditorWindow genreEditorWindow = new GenreEditorWindow();
            genreEditorWindow.Show();
        }
        private void recordCompanyEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RecordCompanyEditorWindow recordCompanyEditorWindow = new RecordCompanyEditorWindow();
            recordCompanyEditorWindow.Show();
        }
    }
}
