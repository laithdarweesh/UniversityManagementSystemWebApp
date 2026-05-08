using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Domain.Entities
{
    public class MainFees
    {
        public int MainFeesID { get; private set; }
        public string Title { get; private set; }
        public decimal Fees { get; private set; }
        private MainFees() { }
        private MainFees(int MainFeesID, string Title, decimal Fees)
        {
            _ValidateFeesTitle(Title);
            _ValidateFees(Fees);

            this.MainFeesID = MainFeesID;
            this.Title = Title;
            this.Fees = Fees;
        }
        public void SetFeesTitle(string NewFeesTitle)
        {
            _ValidateFeesTitle(NewFeesTitle);
            this.Title = NewFeesTitle;
        }
        public void SetFees(decimal NewFees)
        {
            _ValidateFees(NewFees);
            this.Fees = NewFees;
        }
        public static MainFees Add(string FeesTitle, decimal Fees)
        {
            return new MainFees(0, FeesTitle, Fees);
        }
        private void _ValidateFeesTitle(string NewFeesTitle)
        {
            if (string.IsNullOrWhiteSpace(NewFeesTitle))
                throw new ArgumentException("Fees title is required");
        }
        private void _ValidateFees(decimal NewFees)
        {
            if (NewFees < 0)
                throw new ArgumentException("Fees cannot be negative");

            if (NewFees == 0)
                throw new ArgumentException("Fees must be greater than zero");
        }
    }
}