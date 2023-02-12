using System;
using System.ComponentModel.DataAnnotations;

namespace Coursera.Web.ViewModels.Students
{
    public abstract class BaseSearchStudentInputModel
    {
        public string PINs { get; set; }

        [Required]
        [Range(0, 100)]
        public int MinCredit { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public OutputFormat OutputFormat { get; set; }

        [Required]
        [RegularExpression(@".+(?=)")]
        public string DirectoryPath { get; set; }

    }
}