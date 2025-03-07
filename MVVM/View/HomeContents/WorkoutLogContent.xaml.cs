﻿using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy WorkoutLogContent.xaml
    /// </summary>
    public partial class WorkoutLogContent : UserControl
    {
        public WorkoutLogContent()
        {
            InitializeComponent();
            DataContext = new WorkoutLogContentViewModel();
        }
    }
}
