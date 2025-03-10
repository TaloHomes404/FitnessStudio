namespace FitnessStudio.MVVM.Model
{
    public class Supplement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LetterCode { get; set; }
        public string Dosage { get; set; }

        public Supplement(string name, string description, string letterCode, string dosage)
        {
            Name = name;
            Description = description;
            LetterCode = letterCode;
            Dosage = dosage;
        }
    }
}