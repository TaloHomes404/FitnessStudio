using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    public partial class BreathingExercisesContent : UserControl
    {
        public BreathingExercisesContent()
        {
            InitializeComponent();
            DataContext = new BreathingExercisesContentViewModel();

            // Set the middle card as active by default
            SetActiveCard(MiddleCard);
        }


        //Click functionality to select card
        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border clickedCard)
            {
                SetActiveCard(clickedCard);
            }
        }

        private void SetActiveCard(Border activeCard)
        {
            // Reset all cards
            LeftCard.Effect = TryFindResource("BackgroundBlur") as Effect;
            MiddleCard.Effect = TryFindResource("BackgroundBlur") as Effect;
            RightCard.Effect = TryFindResource("BackgroundBlur") as Effect;

            Panel.SetZIndex(LeftCard, 1);
            Panel.SetZIndex(MiddleCard, 1);
            Panel.SetZIndex(RightCard, 1);

            // Set active card
            activeCard.Effect = TryFindResource("CardShadow") as Effect;
            Panel.SetZIndex(activeCard, 2);
        }
    }
}