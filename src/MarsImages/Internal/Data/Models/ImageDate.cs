using System;
using System.Collections.Generic;

namespace MarsImages.Internal.Data.Models
{
    public class ImageDate : IEquatable<ImageDate>
    {
        public ImageDate()
        {
            Photos = new List<Photo>();
        }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Completed { get; set; }
        public IReadOnlyList<Photo> Photos { get; set; }

        public override bool Equals(object obj) => (obj is ImageDate idt) && Equals(idt);

        public bool Equals(ImageDate other) => (Date) == (other.Date);
        public override int GetHashCode() => (Date).GetHashCode();
        public static bool operator == (ImageDate left, ImageDate right) => Equals(left, right);
        public static bool operator != (ImageDate left, ImageDate right) => !Equals(left, right);
    }
}