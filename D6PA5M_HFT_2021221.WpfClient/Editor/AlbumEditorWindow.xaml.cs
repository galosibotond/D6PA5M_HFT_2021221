using System.Windows;

namespace D6PA5M_HFT_2021221.WpfClient
{
    /// <summary>
    /// Interaction logic for AlbumEditorWindow.xaml
    /// </summary>
    public partial class AlbumEditorWindow : Window
    {
        public AlbumEditorWindow()
        {
            InitializeComponent();
        }

        private void genreEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            GenreEditorWindow albumEditorWindow = new GenreEditorWindow();
            albumEditorWindow.Show();
        }

        private void artistEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ArtistEditorWindow artistEditorWindow = new ArtistEditorWindow();
            artistEditorWindow.Show();
        }

        private void recordCompanyEditorGotoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RecordCompanyEditorWindow recordCompanyEditorWindow = new RecordCompanyEditorWindow();
            recordCompanyEditorWindow.Show();
        }
    }
}
