using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy MotivationContent.xaml
    /// </summary>
    public partial class MotivationContent : UserControl
    {
        private List<Border> allCards = new List<Border>();
        private List<TextBlock> allQuoteTexts = new List<TextBlock>();
        private List<TextBlock> allAuthorTexts = new List<TextBlock>();
        private int currentIndex = 1; // Zaczynamy od indeksu 1 (Card2 jako aktywna)
        private int totalCards = 5;
        private string currentMood = "default";

        // Słowniki cytatów dla różnych nastrojów
        private Dictionary<string, List<(string Quote, string Author)>> moodQuotes = new Dictionary<string, List<(string Quote, string Author)>>();

        public MotivationContent()
        {
            InitializeComponent();

            // Inicjalizacja list kart i kropek wskaźnikowych
            allCards = new List<Border> { Card1, Card2, Card3, Card4, Card5 };
            allQuoteTexts = new List<TextBlock> { QuoteText1, QuoteText2, QuoteText3, QuoteText4, QuoteText5 };
            allAuthorTexts = new List<TextBlock> { AuthorText1, AuthorText2, AuthorText3, AuthorText4, AuthorText5 };

            // Inicjalizacja cytatów dla poszczególnych nastrojów
            InitializeMoodQuotes();

            // Ustawienie początkowego stanu karuzeli
            UpdateCarousel();

            // Podpięcie zdarzeń zmiany stanu checkboxów
            foreach (var child in MoodPanel.Children)
            {
                if (child is CheckBox checkBox)
                {
                    checkBox.Unchecked += Mood_Unchecked;
                }
            }
        }

        private void InitializeMoodQuotes()
        {
            // Domyślne cytaty
            moodQuotes["default"] = new List<(string Quote, string Author)>
            {
                ("The only person you should try to be better than is the person you were yesterday.", "Anonymous"),
                ("The future belongs to those who believe in the beauty of their dreams.", "Eleanor Roosevelt"),
                ("Strength doesn't come from what you can do. It comes from overcoming the things you once thought you couldn't.", "Rikki Rogers"),
                ("Success is not final, failure is not fatal: It is the courage to continue that counts.", "Winston Churchill"),
                ("The only way to do great work is to love what you do. If you haven't found it yet, keep looking. Don't settle.", "Steve Jobs")
            };

            // Cytaty dla nastroju "angry"
            moodQuotes["angry"] = new List<(string Quote, string Author)>
            {
                ("For every minute you remain angry, you give up sixty seconds of peace of mind.", "Ralph Waldo Emerson"),
                ("Anger is an acid that can do more harm to the vessel in which it is stored than to anything on which it is poured.", "Mark Twain"),
                ("When angry, count to ten before you speak. If very angry, count to one hundred.", "Thomas Jefferson"),
                ("The greatest remedy for anger is delay.", "Seneca"),
                ("Speak when you are angry and you will make the best speech you will ever regret.", "Ambrose Bierce")
            };

            // Cytaty dla nastroju "tired"
            moodQuotes["tired"] = new List<(string Quote, string Author)>
            {
                ("Rest when you're weary. Refresh and renew yourself, your body, your mind, your spirit.", "Ralph Marston"),
                ("Sometimes the most productive thing you can do is rest.", "Anonymous"),
                ("Almost everything will work again if you unplug it for a few minutes, including you.", "Anne Lamott"),
                ("Your body hears everything your mind says. Stay positive.", "Naomi Judd"),
                ("Sleep is the best meditation.", "Dalai Lama")
            };

            // Cytaty dla nastroju "sad"
            moodQuotes["sad"] = new List<(string Quote, string Author)>
            {
                ("Sadness flies away on the wings of time.", "Jean de La Fontaine"),
                ("Even the darkest night will end and the sun will rise.", "Victor Hugo"),
                ("The greater your storm, the brighter your rainbow.", "Anonymous"),
                ("Life is not about waiting for the storm to pass but learning to dance in the rain.", "Vivian Greene"),
                ("Behind every cloud is another cloud.", "Judy Garland")
            };

            // Cytaty dla nastroju "depressed"
            moodQuotes["depressed"] = new List<(string Quote, string Author)>
            {
                ("You're not alone in this. Remember that.", "Anonymous"),
                ("The sun will rise and we will try again.", "Twenty One Pilots"),
                ("It's hard to beat a person who never gives up.", "Babe Ruth"),
                ("Even the darkest hour only has 60 minutes.", "Morris Mandel"),
                ("Just because today is a terrible day doesn't mean tomorrow won't be the best day of your life. You just have to get there.", "Anonymous")
            };

            // Cytaty dla nastroju "motivated"
            moodQuotes["motivated"] = new List<(string Quote, string Author)>
            {
                ("The hardest step is the decision to act, the rest is merely tenacity.", "Amelia Earhart"),
                ("Don't watch the clock; do what it does. Keep going.", "Sam Levenson"),
                ("Your only limit is you.", "Anonymous"),
                ("The successful warrior is the average person with laser-like focus.", "Bruce Lee"),
                ("Dream it. Wish it. Do it.", "Anonymous")
            };

            // Cytaty dla nastroju "happy"
            moodQuotes["happy"] = new List<(string Quote, string Author)>
            {
                ("Happiness is not something readymade. It comes from your own actions.", "Dalai Lama"),
                ("The most wasted of days is one without laughter.", "E.E. Cummings"),
                ("Happiness is when what you think, what you say, and what you do are in harmony.", "Mahatma Gandhi"),
                ("Count your age by friends, not years. Count your life by smiles, not tears.", "John Lennon"),
                ("The happiness of your life depends upon the quality of your thoughts.", "Marcus Aurelius")
            };

            // Cytaty dla nastroju "alone"
            moodQuotes["alone"] = new List<(string Quote, string Author)>
            {
                ("Sometimes, you need to be alone. Not to be lonely, but to enjoy your free time being yourself.", "Anonymous"),
                ("If you want to go fast, go alone. If you want to go far, go together.", "African Proverb"),
                ("Loneliness is not lack of company, loneliness is lack of purpose.", "Guillermo Maldonado"),
                ("The greatest thing in the world is to know how to belong to oneself.", "Michel de Montaigne"),
                ("Being alone has a power that very few people can handle.", "Steven Aitchison")
            };
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

        }

        private void Mood_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                // Odznaczamy wszystkie inne checkboxy
                foreach (var child in MoodPanel.Children)
                {
                    if (child is CheckBox cb && cb != checkBox)
                    {
                        cb.IsChecked = false;
                    }
                }

                // Pobieramy tag (nazwę nastroju)
                string mood = checkBox.Tag.ToString();
                currentMood = mood;

                // Aktualizujemy cytaty
                UpdateQuotes();
            }
        }

        private void Mood_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                // Sprawdzamy, czy wszystkie checkboxy są odznaczone
                bool allUnchecked = true;
                foreach (var child in MoodPanel.Children)
                {
                    if (child is CheckBox cb && cb.IsChecked == true)
                    {
                        allUnchecked = false;
                        break;
                    }
                }

                // Jeśli wszystkie są odznaczone, wracamy do domyślnych cytatów
                if (allUnchecked)
                {
                    currentMood = "default";
                    UpdateQuotes();
                }
            }
        }

        private void UpdateQuotes()
        {
            // Pobieramy odpowiednie cytaty dla wybranego nastroju
            var quotes = moodQuotes[currentMood];

            // Aktualizujemy treść cytatów w kartach
            for (int i = 0; i < allQuoteTexts.Count && i < quotes.Count; i++)
            {
                allQuoteTexts[i].Text = quotes[i].Quote;
                allAuthorTexts[i].Text = "— " + quotes[i].Author;
            }
        }
    }
}