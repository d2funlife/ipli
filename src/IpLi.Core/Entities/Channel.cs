using System;

namespace IpLi.Core.Entities
{
    public class Channel
    {
        public String Alias { get; set; }
        public String Title { get; set; }
        public String[] AlternativeTitles { get; set; }
        public String ImageUrl { get; set; }

        public String CurrentSourceUrl { get; set; }
        public Source[] Sources { get; set; }

        public Boolean LockSourceUrl { get; set; }

        public void Update(Channel target)
        {
            if(target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            Title = target.Title;
            AlternativeTitles = target.AlternativeTitles;
            ImageUrl = target.ImageUrl;
            CurrentSourceUrl = target.CurrentSourceUrl;
            LockSourceUrl = target.LockSourceUrl;
        }
    }
}