using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy MotivationContent.xaml
    /// </summary>
    public partial class MotivationContent : UserControl
    {
        private List<Border> allCards = new List<Border>();
        private List<Ellipse> allDots = new List<Ellipse>();
        private int currentIndex = 1; // Zaczynamy od indeksu 1 (Card2 jako aktywna)
        private int totalCards = 5;

        public MotivationContent()
        {
            InitializeComponent();

            // Inicjalizacja list kart i kropek wskaźnikowych
            allCards = new List<Border> { Card1, Card2, Card3, Card4, Card5 };
            allDots = new List<Ellipse> { Dot1, Dot2, Dot3, Dot4, Dot5 };

            // Ustawienie początkowego stanu karuzeli
            UpdateCarousel();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Przejście do następnej karty
            currentIndex = (currentIndex + 1) % totalCards;
            if (currentIndex == 0) currentIndex = totalCards; // Obejście dla dzielenia modulo aby indeksy zaczynały się od 1

            UpdateCarousel();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            // Przejście do poprzedniej karty
            currentIndex = (currentIndex - 1 <= 0) ? totalCards : currentIndex - 1;

            UpdateCarousel();
        }

        private void UpdateCarousel()
        {
            // Najpierw ukrywamy wszystkie karty
            foreach (var card in allCards)
            {
                card.Visibility = Visibility.Collapsed;
                card.Effect = TryFindResource("BackgroundBlur") as Effect;
                card.Height = 320;
                Panel.SetZIndex(card, 1);
            }

            // Określamy które karty powinny być widoczne (obecna, poprzednia i następna)
            int prevIndex = (currentIndex - 1 <= 0) ? totalCards : currentIndex - 1;
            int nextIndex = (currentIndex + 1 > totalCards) ? 1 : currentIndex + 1;

            // Ustawiamy widoczność kart
            allCards[prevIndex - 1].Visibility = Visibility.Visible;
            allCards[currentIndex - 1].Visibility = Visibility.Visible;
            allCards[nextIndex - 1].Visibility = Visibility.Visible;

            // Ustawiamy pozycje kart
            Grid.SetColumn(allCards[prevIndex - 1], 0);
            Grid.SetColumn(allCards[currentIndex - 1], 1);
            Grid.SetColumn(allCards[nextIndex - 1], 2);

            // Ustawiamy styl karty aktywnej
            allCards[currentIndex - 1].Effect = TryFindResource("CardShadow") as Effect;
            allCards[currentIndex - 1].Height = 350;
            Panel.SetZIndex(allCards[currentIndex - 1], 2);

            // Aktualizujemy wskaźniki kropek
            for (int i = 0; i < allDots.Count; i++)
            {
                allDots[i].Fill = (i == currentIndex - 1)
                    ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6B6B"))
                    : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDDDDD"));
            }
        }
    }
}