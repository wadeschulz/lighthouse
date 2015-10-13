using System;
using System.ComponentModel.DataAnnotations;

namespace Lighthouse.Storage.Models
{
    public class AnnotationRule
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Query { get; set; }
        public string AnnotationType { get; set; }
        public string Annotation { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? ValidatedUtc { get; set; }
        public bool IsValidated { get; set; }
        public DateTime? ArchivedUtc { get; set; }
        public bool IsArchived { get; set; }

        [MaxLength(255)]
        public string Panel { get; set; }
    }
}
